using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    private AudioSource aS;
    public List <AudioClip> hurt, death, fireWeapon;
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }
    public void PlayFireWeaponSound()
    {
        if (fireWeapon.Count > 0)
        aS.PlayOneShot(fireWeapon[Random.Range(0, fireWeapon.Count)]);
    }
    public void PlayHurtSound()
    {
        if (hurt.Count > 0)
        aS.PlayOneShot(hurt[Random.Range(0, hurt.Count)]);
    }
    public void PlayDeathSound()
    {
        if (death.Count > 0)    
        aS.PlayOneShot(death[Random.Range(0, death.Count)]);
    }
}
