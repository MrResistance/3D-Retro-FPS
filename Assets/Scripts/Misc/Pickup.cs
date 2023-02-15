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
                if (playerInventory.AddWeaponToAvailableWeapons((Inventory.Weapon)thisWeapon))
                {
                    gameObject.SetActive(false);
                }
                playerInventory.WeaponAnimationUpdate();
                
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
