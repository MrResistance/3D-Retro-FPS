using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private Animator anim, healthUIanim, faceUIanim;
    private SFX_Manager sfxManager;
    private GameManager gameManager;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        sfxManager= GetComponent<SFX_Manager>();
        if (this.gameObject.tag.Contains("Player"))
        {
            healthUIanim = GameObject.Find("HealthUI").GetComponent<Animator>();
            healthUIanim.SetFloat("PlayerHealth", health);
            faceUIanim = GameObject.Find("Face").GetComponent<Animator>();
            faceUIanim.SetFloat("PlayerHealth", health);
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                anim.SetTrigger("Death");
                sfxManager.PlayDeathSound();
                Invoke(nameof(DestroyEntity), 0.3f);
            }
        }
        if (this.gameObject.tag.Contains("Player"))
        {
            healthUIanim.SetFloat("PlayerHealth", health);
            faceUIanim.SetFloat("PlayerHealth", health);
            if (health <= 0)
            {
                gameManager.gameOver();
                sfxManager.PlayDeathSound();
            }
        }
    }
    private void DestroyEntity()
    {
        gameObject.SetActive(false);
    }
}
