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
            case Inventory.Weapon.pistol:
                objPooler.SpawnFromPool("Player Pistol Projectile", firePos.transform.position, Quaternion.identity);
                break;
            case Inventory.Weapon.shotgun:
                int pelletCount = Random.Range(6,11);
                for (int i = 0; i < pelletCount; i++)
                {
                    objPooler.SpawnFromPool("Player Shotgun Projectile", firePos.transform.position, Quaternion.identity);
                }
                break;
            case Inventory.Weapon.minigun:
                objPooler.SpawnFromPool("Player Minigun Projectile", firePos.transform.position, Quaternion.identity);
                break;
            case Inventory.Weapon.cannon:
                objPooler.SpawnFromPool("Player Cannon Projectile", firePos.transform.position, Quaternion.Euler(0, inv.transform.rotation.eulerAngles.y, 0));
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
