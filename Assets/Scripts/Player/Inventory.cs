using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Weapon currentWeapon;
    public List<Weapon> availableWeapons;
    public Animator animator;
    public AnimatorController knifeAnim;
    public AnimatorOverrideController pistolAnim, shotgunAnim, minigunAnim, cannonAnim;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    private PlayerAttack playerAttack;
    // Start is called before the first frame update
    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
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
        switch (((int)currentWeapon))
        {
            case 0:
                animator.Play("Unarmed");
                break;
            case 1:
                animator.runtimeAnimatorController = knifeAnim;
                animator.SetTrigger("Ready");
                break;
            case 2:
                animator.runtimeAnimatorController = pistolAnim;
                animator.SetTrigger("Ready");
                break;
            case 3:
                animator.runtimeAnimatorController = shotgunAnim;
                animator.SetTrigger("Ready");
                break;
            case 4:
                animator.runtimeAnimatorController = minigunAnim;
                animator.SetTrigger("Ready");
                break;
            case 5:
                animator.runtimeAnimatorController = cannonAnim;
                animator.SetTrigger("Ready");
                break;
        }
    }
    public void OnShoot()
    {
        if (currentWeapon != Weapon.unarmed)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void OnSwitchWeapon()
    {
        if (Time.time > nextFire && availableWeapons.Count > 0)
        {
            nextFire = Time.time + fireRate;
            int currentIndex = availableWeapons.IndexOf(currentWeapon);
            int nextIndex = 0;
            if (Mouse.current.scroll.ReadValue().normalized.y == 1)
            {
                nextIndex = (currentIndex + 1) % availableWeapons.Count;
            }
            else
            {
                nextIndex = (currentIndex - 1) % availableWeapons.Count;
            }
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
        cannon
    }
}
