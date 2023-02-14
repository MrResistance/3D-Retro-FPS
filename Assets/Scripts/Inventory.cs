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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WeaponAnimationUpdate()
    {
        if (availableWeapons.Count > 1)
        {
            //Debug.Log("More than 1 weapon available");
            //currentWeapon = availableWeapons[(int)currentWeapon++];
            for (int i = 0; i < availableWeapons.Count; i++)
            {
                //if (((int)availableWeapons[i]) == (int)currentWeapon)
                //{
                //    Debug.Log((((int)availableWeapons[i]) == (int)currentWeapon));
                //}
            }
            foreach (Weapon weapon in availableWeapons)
            {
                if (availableWeapons[((int)weapon)] == currentWeapon)
                {
                    Debug.Log("available weapon " + availableWeapons[(int)weapon] + " current weapon " + currentWeapon);
                }
            }
        }
        //switch (((int)currentWeapon))
        //{
        //case 0:
        //        animator.Play("Unarmed");
        //        break;
        //case 1:
        //        animator.runtimeAnimatorController = knifeAnim;
        //        animator.SetTrigger("Ready");
        //        currentWeapon++;
        //        break;
        //case 2:
        //        animator.runtimeAnimatorController = pistolAnim;
        //        animator.SetTrigger("Ready");
        //        currentWeapon++;
        //        break;
        //case 3:
        //        animator.runtimeAnimatorController = shotgunAnim;
        //        animator.SetTrigger("Ready");
        //        currentWeapon++;
        //        break;
        //case 4:
        //        animator.runtimeAnimatorController = minigunAnim;
        //        animator.SetTrigger("Ready");
        //        currentWeapon++;
        //        break;
        //case 5:
        //        animator.runtimeAnimatorController = cannonAnim;
        //        animator.SetTrigger("Ready");
        //        currentWeapon++;
        //        break;       
        //}
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
            if ((((int)currentWeapon) + 1) == 6)
            {
                currentWeapon = 0;
            }
            else
            {
                WeaponAnimationUpdate();
                //currentWeapon++;
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
