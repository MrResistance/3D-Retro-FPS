using System.Collections;
using System.Collections.Generic;
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
    private void OnEnable()
    {
        if (target != null) tempTarget = target.transform;
        if (gameObject.tag.Contains("Player"))
        {
            aS.PlayOneShot(fireSound);
            Vector3 direction = Camera.main.transform.forward;
            if (gameObject.name.Contains("Minigun"))
            {
                float angleX = Random.Range(-2f, 2f);
                float angleY = Random.Range(-2f, 2f);
                float angleZ = Random.Range(-2f, 2f);
                Quaternion rotation = Quaternion.Euler(angleX, angleY, angleZ);
                direction = rotation * direction;
            }
            else if (gameObject.name.Contains("Shotgun"))
            {
                float angleX = Random.Range(-5f, 5f);
                float angleY = Random.Range(-5f, 5f);
                float angleZ = Random.Range(-5f, 5f);
                Quaternion rotation = Quaternion.Euler(angleX, angleY, angleZ);
                direction = rotation * direction;
            }
            rb.velocity = direction.normalized * speed;
        }
        else if (this.gameObject.tag.Contains("Enemy"))
        {
            Vector3 direction = -(transform.position - tempTarget.transform.position);
            if (gameObject.name.Contains("Shotgun"))
            {
                float angleX = Random.Range(-5f, 5f);
                float angleY = Random.Range(-5f, 5f);
                float angleZ = Random.Range(-5f, 5f);
                Quaternion rotation = Quaternion.Euler(angleX, angleY, angleZ);
                direction = rotation * direction;
            }
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
        rb.isKinematic = false;
        transform.position = new Vector3 (0, 0, 0);
        transform.GetChild(0).transform.position = Vector3.zero;
        transform.GetChild(0).transform.rotation = Quaternion.identity;
        if (!gameObject.name.Contains("RPG"))
        { 
            GetComponentInChildren<BillboardAnimation>().direction = Vector3.zero;
            GetComponentInChildren<BillboardAnimation>().rotation = Quaternion.identity;
        }
        this.gameObject.SetActive(false);
    }
}
