using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (this.gameObject.tag.Contains("Enemy"))
            {
                anim.Play("Death");
                Invoke(nameof(DestroyEnemy), 0.3f);
            }
        }
    }
    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
    }
}
