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
            health = collision.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Debug.Log("DO DAMAGE");
        }
        Debug.Log("I HIT: " + collision.collider.name);
        pm.DisableProjectile();
    }
}
