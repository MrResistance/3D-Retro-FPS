using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private Animator anim;
    private SFX_Manager sfxManager;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        sfxManager= GetComponent<SFX_Manager>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        sfxManager.PlayHurtSound();
        if (this.gameObject.tag.Contains("Enemy"))
        {
            anim.Play("HitFront");
            if (health <= 0)
            {
                anim.Play("Death");
                sfxManager.PlayDeathSound();
                Invoke(nameof(DestroyEntity), 0.3f);
            }
        }
    }
    private void DestroyEntity()
    {
        gameObject.SetActive(false);
    }
}
