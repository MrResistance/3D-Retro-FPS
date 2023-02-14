using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon currentWeapon;
    public List<Weapon> availableWeapons;
    public Animator animator;
    public AnimatorOverrideController knifeAnim, pistolAnim, shotgunAnim, minigunAnim, cannonAnim;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    private PlayerAttack playerAttack;
    // Start is called before the first frame update
    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        knifeAnim = animator.runtimeAnimatorController as AnimatorOverrideController;
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
        if (currentWeapon != Weapon.unarmed && currentWeapon != Weapon.knife)
        {
            animator.SetTrigger("Attack");
            playerAttack.ShootProjectile();
        }
    }
    public void OnSwitchWeapon()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //if ((((int)currentWeapon) + 1) == 6)
            //{
            //    currentWeapon = 0;
            //}
            //else
            //{
            //    WeaponAnimationUpdate();
            //    //currentWeapon++;
            //}
            int currentIndex = availableWeapons.IndexOf(currentWeapon);
            int nextIndex = (currentIndex + 1) % availableWeapons.Count;
            currentWeapon = availableWeapons[nextIndex];
            WeaponAnimationUpdate();
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
