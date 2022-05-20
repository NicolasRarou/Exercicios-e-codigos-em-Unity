using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject [] fireballs;

    private Animator anim;
    private player_moviment playerMovement;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<player_moviment>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && coolDownTimer > attackCoolDown && playerMovement.canAttack())
            Attack();
            coolDownTimer += Time.deltaTime;
        
    }

   private void Attack()
    {
        anim.SetTrigger("Attack");
        coolDownTimer = 0;

        fireballs[findFireball()].transform.position = FirePoint.position;
        fireballs[findFireball()].GetComponent<projectile> ().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

}
