using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Inventory inv;
    private ObjectPooler objPooler;
    public Transform firePos;

    private void Awake()
    {
        inv = GetComponent<Inventory>();
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
    }
    public void ShootProjectile()
    {
        if (inv.currentWeapon == Inventory.Weapon.pistol)
        {
            objPooler.SpawnFromPool("Player Projectile", firePos.transform.position, Quaternion.identity);
        }
    }
}
