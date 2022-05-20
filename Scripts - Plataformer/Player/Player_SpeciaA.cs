using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SpeciaA : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform BlastPoint;
    [SerializeField] private GameObject[] blasts;



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
        if (Input.GetButtonDown("Fire2") && coolDownTimer > attackCoolDown && playerMovement.canAttack())
            Special_Attack();
            coolDownTimer += Time.deltaTime;
        
    }

    private void Special_Attack()
    {
        anim.SetTrigger("Attack");
        coolDownTimer = 0;

        blasts[Findblast()].transform.position = BlastPoint.position;
        blasts[Findblast()].GetComponent<Blast_projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int Findblast()
    {
        for (int i = 0; i < blasts.Length; i++)
        {
            if (!blasts[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
