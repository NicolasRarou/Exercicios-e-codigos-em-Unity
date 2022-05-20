using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float starthingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iframes")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private float numberOFFflashes;

    private SpriteRenderer spriteRend;
    private void Awake()
    {
        currentHealth = starthingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, starthingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
           if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<player_moviment>().enabled = false;
                dead = true;
            }
        }
    }

    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, starthingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7,8, true);

        for (int i = 0; i < numberOFFflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numberOFFflashes * 2));
            spriteRend.color = Color.white;
           yield return new WaitForSeconds(iframesDuration / (numberOFFflashes * 2));

        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
