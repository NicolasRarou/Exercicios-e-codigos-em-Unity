using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_trap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] arrows;
    private float coolDownTimer;

    private void Attack()
    {
        coolDownTimer = 0;


        arrows[findArrow()].transform.position = FirePoint.position;
        arrows[findArrow()].GetComponent<EnemyProjectile>().Activeprojectile();
    }
    private int findArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (coolDownTimer >= attackCoolDown)
        {
            Attack();
        }
    }
}
