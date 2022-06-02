using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public ObjectPooler objPooler;
    public float speed = 20f;
    private Vector3 firePos;

    private void OnEnable()
    {
        firePos = new Vector3(target.position.x, 0, target.position.z);
        rb.AddForce((firePos - new Vector3 (transform.position.x, 0, transform.position.z)) * Time.deltaTime * speed, ForceMode.Impulse);

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
        
        //transform.Translate(target.position * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.tag.Contains("Player") && other.tag.Contains("Enemy") || this.gameObject.tag.Contains("Enemy") && other.tag.Contains("Player")) && other.tag != "Projectile")
        {
            //Do damage
            //Debug.Log("DO DAMAGE");
            this.gameObject.SetActive(false);
        }
        else
        {
            //Debug.Log("I HIT: " + other.name);
            this.gameObject.SetActive(false);
        }
    }
}
