using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private Animator anim, healthUIanim;
    private SFX_Manager sfxManager;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        sfxManager= GetComponent<SFX_Manager>();
        if (this.gameObject.tag.Contains("Player"))
        {
            healthUIanim = GameObject.Find("HealthUI").GetComponent<Animator>();
            healthUIanim.SetFloat("PlayerHealth", health);
        }
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
        if (this.gameObject.tag.Contains("Player"))
        {
            healthUIanim.SetFloat("PlayerHealth", health);
            if (health <= 0)
            {
                sfxManager.PlayDeathSound();
            }
        }
    }
    private void DestroyEntity()
    {
        gameObject.SetActive(false);
    }
}
