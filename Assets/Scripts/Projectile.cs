using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public ObjectPooler objPooler;
    public float speed = 5f;
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
        if ((this.gameObject.tag.Contains("player") && other.tag.Contains("Enemy") || this.gameObject.tag.Contains("enemy") && other.tag.Contains("player")) && other.tag != "Projectile")
        {
            this.gameObject.SetActive(false);
        }
    }
}
