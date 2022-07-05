using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Inventory inv;
    private ObjectPooler objPooler;

    private void Awake()
    {
        inv = GetComponent<Inventory>();
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
    }
    public void ShootProjectile()
    {
        if (inv.currentWeapon == Inventory.Weapon.pistol)
        {
            objPooler.SpawnFromPool("Player Projectile", new Vector3(transform.position.x + 1f, 1, transform.position.z), Quaternion.identity);
        }
    }
}
