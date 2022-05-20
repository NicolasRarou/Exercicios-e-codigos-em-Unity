using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Trap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float Damage;
    [SerializeField]private float activationDelay;
    [SerializeField] private float activationTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool triggered;
    private bool active;

    private void Awake()
    {
       anim = GetComponent<Animator>();
       spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(Damage);
            }
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRend.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activationTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
