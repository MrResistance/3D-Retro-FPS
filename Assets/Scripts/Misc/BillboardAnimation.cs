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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = target.position - transform.position;
        direction.y = 0;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
