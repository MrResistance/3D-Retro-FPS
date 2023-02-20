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
                objPooler.SpawnFromPool("Shotgun Projectile", firePos.transform.position, Quaternion.identity);
                break;
            case Inventory.Weapon.minigun:
                break;
            case Inventory.Weapon.cannon:
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
