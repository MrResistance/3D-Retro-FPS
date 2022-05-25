using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public objectPooler objPooler;
    public float speed = 5f;
    private Vector3 firePos;

    private void OnEnable()
    {
        objPooler.AddObjectToPool(this.gameObject);
        firePos = new Vector3(target.position.x, 0, target.position.z);
    }
    // Start is called before the first frame update
    void Awake()
    {
        objPooler = GameObject.FindObjectOfType<objectPooler>();
        target = GameObject.Find("PlayerCapsule").transform;
        rb = GetComponent<Rigidbody>();
        rb.AddForce((firePos - transform.position) * Time.deltaTime * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.Translate(target.position * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.tag.Contains("player") && !other.tag.Contains("Enemy") || this.gameObject.tag.Contains("enemy") && !other.tag.Contains("player")) && other.tag != "Projectile")
        {
            this.gameObject.SetActive(false);
        }
    }
}
