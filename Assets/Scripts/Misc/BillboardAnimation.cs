using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardAnimation : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;
    public Quaternion rotation;
    public float damping = 360f;
    private void Awake()
    {
        target = GameObject.Find("PlayerCapsule").transform;
    }
    // Update is called once per frame
    void Update()
    {
        direction = target.position - transform.position;
        if (!transform.parent.tag.Equals("Player Projectile"))
        {
            direction.y = 0;
        }
        rotation = Quaternion.LookRotation(direction);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
