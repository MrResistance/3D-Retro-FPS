using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Weapon currentWeapon;
    public List<Weapon> availableWeapons;
    public Animator animator;
    public RuntimeAnimatorController knifeAnim;
    public AnimatorOverrideController pistolAnim, shotgunAnim, minigunAnim, rpgAnim;
    public float weaponSwapSpeed = 0.5F;
    private float nextSwap = 0.0F;
    public List<GameObject> weaponIndicatorSprites;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GameObject.Find("Weapon").GetComponent<Animator>();
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    weaponIndicatorSprites[0] = GameObject.Find("Indicator_Weapon_Knife");
                    break;
                case 1:
                    weaponIndicatorSprites[1] = GameObject.Find("Indicator_Weapon_Pistol");
                    break;
                case 2:
                    weaponIndicatorSprites[2] = GameObject.Find("Indicator_Weapon_Shotgun");
                    break;
                case 3:
                    weaponIndicatorSprites[3] = GameObject.Find("Indicator_Weapon_Minigun");
                    break;
                case 4:
                    weaponIndicatorSprites[4] = GameObject.Find("Indicator_Weapon_RPG");
                    break;

            }
        }
    }
    void Start()
    {
        if (availableWeapons.Count > 0)
        {
            currentWeapon = availableWeapons[0];
        }
        else
        {
            currentWeapon = Weapon.unarmed;
        }
        WeaponAnimationUpdate();
    }
    public void WeaponAnimationUpdate()
    {
        ResetWeaponIndicatorSprites();
        switch (((int)currentWeapon))
        {
            case 0:
                animator.Play("Unarmed");
                break;
            case 1:
                animator.runtimeAnimatorController = knifeAnim;
                animator.SetTrigger("Ready");
                weaponIndicatorSprites[0].SetActive(true);
                break;
            case 2:
                animator.runtimeAnimatorController = pistolAnim;
                animator.SetTrigger("Ready");
                weaponIndicatorSprites[1].SetActive(true);
                break;
            case 3:
                animator.runtimeAnimatorController = shotgunAnim;
                animator.SetTrigger("Ready");
                weaponIndicatorSprites[2].SetActive(true);
                break;
            case 4:
                animator.runtimeAnimatorController = minigunAnim;
                animator.SetTrigger("Ready");
                weaponIndicatorSprites[3].SetActive(true);
                break;
            case 5:
                animator.runtimeAnimatorController = rpgAnim;
                animator.SetTrigger("Ready");
                weaponIndicatorSprites[4].SetActive(true);
                break;
        }
    }
    public void ResetWeaponIndicatorSprites()
    {
        foreach (GameObject sprite in weaponIndicatorSprites)
        {
            sprite.SetActive(false);
        }
    }
    public void OnShoot()
    {
        if (currentWeapon != Weapon.unarmed)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void OnShootHold()
    {
        if (currentWeapon == Weapon.minigun)
        {
            animator.SetBool("IsMinigun", true);
        }
    }
    public void OnStopShooting()
    {
        animator.SetBool("IsMinigun", false);
    }
    public void OnSwitchWeaponUp()
    {
        OnStopShooting();
        if (Time.time > nextSwap && availableWeapons.Count > 0)
        {
            nextSwap = Time.time + weaponSwapSpeed;
            int currentIndex = availableWeapons.IndexOf(currentWeapon);
            int nextIndex = 0;
            nextIndex = (currentIndex + 1) % availableWeapons.Count;
            // To ensure that the nextIndex field doesn't go outside the bounds of the array
            if (nextIndex < 0)
            {
                nextIndex = availableWeapons.Count - 1;
            }
            if (nextIndex > availableWeapons.Count)
            {
                nextIndex = 0;
            }
            currentWeapon = availableWeapons[nextIndex];
            WeaponAnimationUpdate();
        }
    }
    public void OnSwitchWeaponDown()
    {
        OnStopShooting();
        if (Time.time > nextSwap && availableWeapons.Count > 0)
        {
            nextSwap = Time.time + weaponSwapSpeed;
            int currentIndex = availableWeapons.IndexOf(currentWeapon);
            int nextIndex = 0;
            nextIndex = (currentIndex - 1) % availableWeapons.Count;
            // To ensure that the nextIndex field doesn't go outside the bounds of the array
            if (nextIndex < 0)
            {
                nextIndex = availableWeapons.Count - 1;
            }
            if (nextIndex > availableWeapons.Count)
            {
                nextIndex = 0;
            }
            currentWeapon = availableWeapons[nextIndex];
            WeaponAnimationUpdate();
        }
    }
    public bool AddWeaponToAvailableWeapons(Weapon weapon)
    {
        if (!availableWeapons.Contains(weapon))
        {
            availableWeapons.Add(weapon);
            return true;
        }
        else
        {
            return false;
        } 
    }
    public enum Weapon
    {
        unarmed,
        knife,
        pistol,
        shotgun,
        minigun,
        RPG
    }
}
