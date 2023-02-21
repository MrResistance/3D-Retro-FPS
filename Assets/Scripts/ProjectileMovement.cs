using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Transform target, tempTarget, player;
    public Rigidbody rb;
    public ObjectPooler objPooler;
    public Camera MainCamera;
    public float speed = 1f;
    private AudioSource aS;
    public AudioClip fireSound, contactSound;

    private void FixedUpdate()
    {
        if (this.gameObject.tag.Contains("Enemy"))
        {
            rb.velocity = -(transform.position - tempTarget.transform.position) * speed;
        }
    }
    private void OnEnable()
    {
        if (target != null) tempTarget = target.transform;
        if (gameObject.tag.Contains("Player"))
        {
            aS.PlayOneShot(fireSound);
            Vector3 direction = Camera.main.transform.forward;
            rb.velocity = direction.normalized * speed;
        }
    }
    void Awake()
    {
        aS = GetComponentInChildren<AudioSource>();
        player = GameObject.Find("PlayerCapsule").transform;
        MainCamera = Camera.main;
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
        if (this.gameObject.tag.Contains("Enemy"))
        {
            target = GameObject.Find("PlayerCameraRoot").GetComponent<Transform>();
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
        transform.position = new Vector3 (0, 0, 0);
        transform.GetChild(0).transform.position = Vector3.zero;
        transform.GetChild(0).transform.rotation = quaternion.identity;
        if (!gameObject.name.Contains("Cannon"))
        { 
            GetComponentInChildren<BillboardAnimation>().direction = Vector3.zero;
            GetComponentInChildren<BillboardAnimation>().rotation = quaternion.identity;
        }
        this.gameObject.SetActive(false);
    }
}
