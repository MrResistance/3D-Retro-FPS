using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public ProjectileMovement pm;
    public float damage = 5f;
    private Health health;
    private void Awake()
    {
        pm = GetComponentInParent<ProjectileMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Contains("Enemy") || collision.collider.tag.Contains("Player"))
        {
            //Do damage
            Debug.Log("I HIT " + collision.gameObject.name + " Which has the tag: " + collision.gameObject.tag);
            health = collision.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        pm.DisableProjectile();
    }
}
