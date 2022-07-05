using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public ObjectPooler objPooler;
    public Camera MainCamera;
    public float speed = 1f;
    private Vector3 targetPos, thisPos, direction;
    public Quaternion rotation;

    private void OnEnable()
    {
    }
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
    // Start is called before the first frame update
    void Awake()
    {
        MainCamera = Camera.main;
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
        target = GameObject.Find("PlayerCapsule").transform;
        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.tag.Contains("Player") && other.tag.Contains("Enemy") || this.gameObject.tag.Contains("Enemy") && other.tag.Contains("Player")))
        {
            //Do damage
            Debug.Log("DO DAMAGE");
            //DisableProjectile();
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
