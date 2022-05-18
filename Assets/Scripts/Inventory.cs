using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon currentWeapon;
    public Animator animator;
    public AnimatorOverrideController pistolAnim, shotgunAnim, minigunAnim, cannonAnim;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Weapon.knife;
    }

    // Update is called once per frame
    void Update()
    {
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
                animator.SetTrigger("Ready");
                break;
        case 2:
                animator.runtimeAnimatorController = pistolAnim;
                break;
        case 3:
                animator.runtimeAnimatorController = shotgunAnim;
                break;
        case 4:
                animator.runtimeAnimatorController = minigunAnim;
                break;
        case 5:
                animator.runtimeAnimatorController = cannonAnim;
                break;       
        }
    }
    public void OnSwitchWeapon()
    {
        if ((((int)currentWeapon) + 1) == 6)
        {
            currentWeapon = 0;
        }
        else
        {
            currentWeapon++;
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
