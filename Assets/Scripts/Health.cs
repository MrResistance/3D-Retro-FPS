using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public Animator anim, health_UI_anim, face_UI_anim;
    private SFX_Manager sfxManager;
    private GameManager gameManager;
    private void Awake()
    {
        sfxManager= GetComponent<SFX_Manager>();
        if (this.gameObject.tag.Contains("Player"))
        {
            health_UI_anim = GameObject.Find("HealthUI").GetComponent<Animator>();
            health_UI_anim.SetFloat("PlayerHealth", health);
            face_UI_anim = GameObject.Find("Face").GetComponent<Animator>();
            face_UI_anim.SetFloat("PlayerHealth", health);
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else if (this.gameObject.tag.Contains("Enemy"))
        {
            anim = GetComponentInChildren<Animator>();
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
            health_UI_anim.SetFloat("PlayerHealth", health);
            face_UI_anim.SetFloat("PlayerHealth", health);
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
