using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public ObjectPooler objPooler;
    public float speed = 1f;
    private Vector3 firePos;

    private void OnEnable()
    {
        firePos = new Vector3(target.position.x, 1, target.position.z);
        //rb.AddForce((firePos - new Vector3 (transform.position.x, 1, transform.position.z)) * Time.deltaTime * speed, ForceMode.Impulse);
        
        //rb.AddForce((firePos - new Vector3(transform.position.x, 1, transform.position.z)), ForceMode.Impulse);
        
        //rb.AddForce(transform.forward * speed, ForceMode.Impulse);

    }
    // Start is called before the first frame update
    void Awake()
    {
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
        target = GameObject.Find("PlayerCapsule").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 1, transform.position.z), firePos, speed);
        //transform.Translate(target.position * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.tag.Contains("Player") && other.tag.Contains("Enemy") || this.gameObject.tag.Contains("Enemy") && other.tag.Contains("Player")))
        {
            //Do damage
            Debug.Log("DO DAMAGE");
            DisableProjectile();
        }
        else
        {
            Debug.Log("I HIT: " + other.name);
            DisableProjectile();
        }
    }

    private void DisableProjectile()
    {
        rb.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}