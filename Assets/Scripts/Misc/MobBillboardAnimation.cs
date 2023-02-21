using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBillboardAnimation : MonoBehaviour
{
    // The target marker.
    public Transform target;
    public Vector3 targetDirection;
    public Animator anim;
    public bool MirrorLeft = true;
    private SpriteRenderer sr;
    public Camera MainCamera;
    public int directions = 8;
    float minMirrorAngle = 0, maxMirrorAngle = 0;
    // Angular speed in radians per sec.
    public float speed = 1.0f;
    private void Awake()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
        target = GameObject.Find("PlayerCapsule").transform;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (directions <= 0)
        {
            directions = 1;
        }
        minMirrorAngle = (360 / directions) / 2;
        maxMirrorAngle = 180 - minMirrorAngle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDirection = -new Vector3(MainCamera.transform.forward.x, 0, MainCamera.transform.forward.z);
        transform.LookAt(transform.position + viewDirection);
        if (this.gameObject.tag == "Enemy" || transform.parent.name.Contains("Cannon"))
        {
            anim.SetFloat("ViewAngle", transform.localEulerAngles.y);
        }
        if (MirrorLeft)
        {
            sr.flipX = !(transform.localEulerAngles.y >= minMirrorAngle && transform.localEulerAngles.y <= maxMirrorAngle);
        }
    }

    public void EnemyShoot()
    {
        GetComponentInParent<EnemyAI>().FireProjectile();
    }
}
