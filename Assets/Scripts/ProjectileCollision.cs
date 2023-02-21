using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public ProjectileMovement pm;
    public float damage = 5f, radius = 20f;
    private Health health;
    public LayerMask enemyLayer;
    private void Awake()
    {
        pm = GetComponentInParent<ProjectileMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.parent.name.Contains("Cannon"))
        {
            GetComponent<Animator>().Play("rocketExplode");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.localScale = new Vector3(10, 10, 10);
            AreaOfEffectAttack();
        }
        else if (collision.collider.tag.Contains("Enemy") || collision.collider.tag.Contains("Player"))
        {
            health = collision.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            DisableProjectile();
        }
        else
        {
            DisableProjectile();
        }
    }

    private void AreaOfEffectAttack()
    {
        if (gameObject.tag.Contains("Player"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);
            foreach (Collider collider in colliders)
            {
                Health enemy = collider.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
    private void DisableProjectile()
    {
        transform.localScale = new Vector3(1, 1, 1);
        pm.DisableProjectile();
    }
}
