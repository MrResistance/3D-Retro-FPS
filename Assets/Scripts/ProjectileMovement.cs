using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Transform target, tempTarget, player;
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
    }
    private void OnEnable()
    {
        if (target != null) tempTarget = target.transform;
        rb.AddForce(player.transform.forward * speed);
    }
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
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
    
    public void DisableProjectile()
    {
        rb.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}
