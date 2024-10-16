using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody rb;

    public Animator animeror;

    //エネミーの体力
    [SerializeField] int EnemyHP = 120;

    //エネミーの攻撃力
    [SerializeField] int Enemy_Attack = 30;

    public enum EnemyState
    {
        Idle, //待機
        Wite, //一時停止
        Move, //追跡 
        Attack01, //攻撃１
        Attack02, //攻撃２
        Freeze,   //移行
        Death     //死亡
    };

    EnemyState enemyState = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //　敵キャラの状態を設定するためのメソッド 
    public void SetState(EnemyState enemyState, Transform targetObject = null)
    {
        
    }

   

    //　敵キャラの状態を取得するためのメソッド
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Sword")
        {
            EnemyHP -= 20;

            if (EnemyHP <= 0)
            {
                animeror.SetBool("Death", true);
                Destroy(this.gameObject,3);
            }
            else
            {
                animeror.SetBool("Death", false);
            }
        }
    }

}
