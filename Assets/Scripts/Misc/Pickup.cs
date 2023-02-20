using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Inventory playerInventory;
    public Health playerHealth;
    public Weapon thisWeapon;
    public bool isWeapon = true;
    public Item thisItem;
    public bool isItem = false;
    public float healthRestoreAmount = 25f;

    private void Awake()
    {
        playerInventory = GameObject.Find("PlayerCapsule").GetComponent<Inventory>();
    }
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
            if (isItem)
            {
                switch (thisItem)
                {
                    case Item.health:
                        playerHealth = GameObject.Find("Capsule").GetComponent<Health>();
                        playerHealth.health += healthRestoreAmount;
                        break;
                    case Item.superHealth:
                        playerHealth = GameObject.Find("Capsule").GetComponent<Health>();
                        playerHealth.health += healthRestoreAmount * 2;
                        break;
                    case Item.ammo:
                        break;
                    default:
                        break;
                }
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
    public enum Item
    {
        health,
        superHealth,
        ammo
    }
}
