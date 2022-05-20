using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] protected float Damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}
