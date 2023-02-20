using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Inventory inv;
    private ObjectPooler objPooler;
    public Transform firePos;
    public SphereCollider meleeRange;
    public LayerMask enemyLayer;
    public float meleeAttackRadius = 1f, meleeAttackDamage = 10f;

    private void Awake()
    {
        inv = GameObject.Find("PlayerCapsule").GetComponent<Inventory>();
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
    }
    public void ShootProjectile()
    {
        switch (inv.currentWeapon)
        {
            case Inventory.Weapon.unarmed:
                break;
            case Inventory.Weapon.knife:
                break;
            case Inventory.Weapon.pistol:
                objPooler.SpawnFromPool("Pistol Projectile", firePos.transform.position, Quaternion.identity);
                break;
            case Inventory.Weapon.shotgun:
                int pelletCount = Random.Range(6,11);
                for (int i = 0; i < pelletCount; i++)
                {
                    float randomX = Random.Range(-0.5f, 0.5f);
                    float randomY = Random.Range(-0.5f, 0.5f);
                    objPooler.SpawnFromPool("Shotgun Projectile", new Vector3(firePos.transform.position.x + randomX, firePos.transform.position.y + randomY, firePos.transform.position.z), Quaternion.identity);

                }
                break;
            case Inventory.Weapon.minigun:
                break;
            case Inventory.Weapon.cannon:
                objPooler.SpawnFromPool("Rocket Projectile", firePos.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void MeleeAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(firePos.transform.position, meleeAttackRadius, enemyLayer);
        foreach (Collider collider in colliders)
        {
            Health enemy = collider.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(meleeAttackDamage);
            }
        }
    }
}
