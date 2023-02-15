using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Inventory playerInventory;
    public Weapon thisWeapon;
    public bool isWeapon = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            if (isWeapon)
            {
                playerInventory.availableWeapons.Add((Inventory.Weapon)thisWeapon);
                playerInventory.WeaponAnimationUpdate();
                gameObject.SetActive(false);
            }
        }
    }
    public enum Weapon
    {
        unarmed,
        knife,
        pistol,
        shotgun,
        minigun,
        cannon
    }
}
