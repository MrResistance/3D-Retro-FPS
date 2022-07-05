using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target, tempTarget;
    public Rigidbody rb;
    public ObjectPooler objPooler;
    public Camera MainCamera;
    public float speed = 1f;
    private void FixedUpdate()
    {
        if (this.gameObject.tag.Contains("Enemy"))
        {
            rb.velocity = -(new Vector3 (transform.position.x, 1, transform.position.z) - new Vector3 (tempTarget.transform.position.x, 1, tempTarget.transform.position.z)) * speed;
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }
    }
    private void OnEnable()
    {
        tempTarget = target.transform;
    }
    // Start is called before the first frame update
    void Awake()
    {
        MainCamera = Camera.main;
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
        if (this.gameObject.tag.Contains("Enemy"))
        {
            target = GameObject.Find("PlayerCapsule").transform;
        }
        else
        {
            target = null;
        }
        rb = GetComponentInChildren<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.tag.Contains("Player") && other.tag.Contains("Enemy") || this.gameObject.tag.Contains("Enemy") && other.tag.Contains("Player")))
        {
            //Do damage
            Debug.Log("DO DAMAGE");
        }
            Debug.Log("I HIT: " + other.name);
            DisableProjectile();
    }
    private void DisableProjectile()
    {
        rb.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}
