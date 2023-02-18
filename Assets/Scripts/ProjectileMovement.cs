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
            rb.velocity = -(transform.position - tempTarget.transform.position) * speed;
        }
    }
    private void OnEnable()
    {
        if (target != null) tempTarget = target.transform;
        if (gameObject.tag.Contains("Player"))
        {
            Vector3 direction = player.transform.forward;
            direction.y = MainCamera.transform.forward.y;
            rb.velocity = direction.normalized * speed;


            //rb.AddForce((player.transform.forward + MainCamera.transform.up) * speed);

            //Vector3 direction = player.transform.forward + new Vector3(0f, MainCamera.transform.rotation.eulerAngles.y, 0f);
            //rb.velocity = direction.normalized * speed;

            //rb.AddForce(transform.up * MainCamera.transform.rotation.x);
            //Debug.Log("camera rot x: " + MainCamera.transform.rotation.x);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
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
        this.gameObject.SetActive(false);
    }
}
