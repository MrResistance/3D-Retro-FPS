using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // The target marker.
    public Transform target;
    public Vector3 targetDirection;
    public bool MirrorLeft = true;
    private Animator anim;
    private SpriteRenderer sr;
    public Camera MainCamera;
    public int directions = 8;
    float minMirrorAngle = 0;
    float maxMirrorAngle = 0;
    // Angular speed in radians per sec.
    public float speed = 1.0f;
    private void Awake()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
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
        //// Determine which direction to rotate towards
        //targetDirection = target.position - transform.position;
        //targetDirection.y = 0;
        //// The step size is equal to speed times frame time.
        //float singleStep = speed * Time.deltaTime;

        //// Rotate the forward vector towards the target direction by one step
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        //// Draw a ray pointing at our target in
        //Debug.DrawRay(transform.position, newDirection, Color.red);

        //// Calculate a rotation a step closer to the target and applies rotation to this object
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //anim.SetFloat("PlayerX", targetDirection.x);
        //anim.SetFloat("PlayerZ", -targetDirection.z);

        Vector3 viewDirection = -new Vector3(MainCamera.transform.forward.x, 0, MainCamera.transform.forward.z);
        transform.LookAt(transform.position + viewDirection);
        if (this.gameObject.tag == "Enemy")
        {
            anim.SetFloat("ViewAngle", transform.localEulerAngles.y);
        }
        if (MirrorLeft)
        {
            sr.flipX = !(transform.localEulerAngles.y >= minMirrorAngle && transform.localEulerAngles.y <= maxMirrorAngle);
        }
    }
}
