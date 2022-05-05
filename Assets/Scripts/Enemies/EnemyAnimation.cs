using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // The target marker.
    public Transform target;
    public Vector3 targetDirection;
    private Animator anim;
    // Angular speed in radians per sec.
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine which direction to rotate towards
        targetDirection = target.position - transform.position;
        targetDirection.y = 0;
        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        anim.SetFloat("PlayerX", targetDirection.x);
        anim.SetFloat("PlayerZ", targetDirection.z);
    }
}
