using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public Animator animator;

    private GameObject player;

    EnemyScript enemyScript;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            //プレイヤーがセンサー内にいたら攻撃する
            animator.SetBool("Attack", true);
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        //プレイヤーがセンサー外にいたら攻撃せず、追跡する
        if (player)
        {
            animator.SetBool("Attack", false);
        }
    }
}
