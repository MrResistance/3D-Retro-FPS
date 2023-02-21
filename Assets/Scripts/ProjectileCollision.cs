using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public ProjectileMovement pm;
    public float damage = 5f, radius = 20f;
    private Health health;
    public LayerMask playerLayer, enemyLayer;
    private void Awake()
    {
        pm = GetComponentInParent<ProjectileMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //RPG projectile contact is different to the others, it requires an explosion so we:
        //Play the explosion animation, stop the projectile from moving,
        //scale up the explosion effect, calculate AOE (Area of effect) damage
        if (transform.parent.name.Contains("Cannon"))
        {
            GetComponent<Animator>().Play("rocketExplode");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.localScale = new Vector3(10, 10, 10);
            AreaOfEffectAttack();
        }
        //If we've hit something that has health, do damage to it then disable the projectile
        else if (collision.collider.tag.Contains("Enemy") || collision.collider.tag.Contains("Player"))
        {
            health = collision.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            DisableProjectile();
        }
        else //if we've hit something else (walls, floors etc) just disable the projectile (potential for decals here)
        {
            DisableProjectile();
        }
    }

    private void AreaOfEffectAttack()
    {
        //Check if there are enemies in the area, damage them if so
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
        else if (gameObject.tag.Contains("Enemy"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayer);
            foreach (Collider collider in colliders)
            {
                Health player = collider.GetComponent<Health>();
                if (player != null)
                {
                    player.TakeDamage(damage);
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
