using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody rb;

    public Animator animator;

    //エネミーの体力
    [SerializeField] int EnemyHP = 120;

    //エネミーの攻撃力
    [SerializeField] int Enemy_Attack = 30;

    //エネミー攻撃フレーム
    int Enemy_Attack_Frame01 = 120;

    int Enemy_Attack_Frame02 = 240;

    enum EnemyState
    {
        Wite,
        Run,
        Attack01,
        Attack02,
        Death
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = 240;

        rb = GetComponent<Rigidbody>();

        EnemyState enemyState = EnemyState.Wite;

        switch (enemyState)
        {
            case EnemyState.Wite:
                EnemyWite();
                break;
            case EnemyState.Run:
                EnemyRun();
                break;
            case EnemyState.Attack01:
                EnemyAttack01();
                break;
            case EnemyState.Attack02:
                EnemyAttack02();
                break;
            case EnemyState.Death:
                EnemyDeath();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Sword")
        {
            EnemyHP -= 20;
        }
    }

    //エネミー待機
    void EnemyWite()
    {
        
    }

    //エネミー移動
    void EnemyRun()
    {
        
    }
    //エネミー攻撃０１
    void EnemyAttack01()
    {
        Enemy_Attack_Frame01--;
        if (Enemy_Attack_Frame01 <= 0)
        {
            animator.SetBool("Enemy_Attack01", true);
            Enemy_Attack_Frame01 = 2400;
        }
        else
        {
            animator.SetBool("Enemy_Attack01", false);
        }
    }
    //エネミー攻撃０２
    void EnemyAttack02()
    {
        Enemy_Attack_Frame02--;
        if (Enemy_Attack_Frame02 <= 0)
        {
            animator.SetBool("Enemy_Attack02", true);
            Enemy_Attack_Frame02 = 3600;
        }
        else
        {
            animator.SetBool("Enemy_Attack02", false);
        }
    }

    //エネミー死亡
    void EnemyDeath()
    {
        if (EnemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
