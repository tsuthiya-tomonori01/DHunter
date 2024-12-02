using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public enum EnemyState
    {
        Wite, //一時停止
        Move, //追跡 
        Attack01, //攻撃１
        Attack02, //攻撃２
        Freeze,   //移行
        Death     //死亡
    };

    public Transform targetTransform;

    public Rigidbody rb;

    public Animator animator;

    public Transform target;

    public EnemyState state;
    
    bool EnemyDeath = false;

    //エネミーの体力
    public int EnemyHP = 1200;

    public float enemyMoveSpeed;

    public float EnemyAngulSpeed;

    public int enemyDeathCount = 0;

    private float FreezeTime = 5.0f;

    private float elapsedTime;

    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = 1200;
        enemyDeathCount = 0;
        SetState(EnemyState.Wite);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyDeath == true)
        {
            return;
        }

        if (state == EnemyState.Move)
        {
            if (targetTransform == null)
            {
                SetState(EnemyState.Wite);
            }
            else
            {
                //ターゲットの座標を入れる
                SetDistination(targetTransform.position);
                
                //敵の向きをプレイヤーの方向に変えていく
                var dir = (GetDisnation() - transform.position).normalized;

                //上に向かないようにする
                dir.y = 0;

                //エネミーの角度をプレイヤーの方向に近づける
                Quaternion setRotation = Quaternion.LookRotation(dir);

                //エネミーの角度を決めた角度に代入する
                transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, EnemyAngulSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetTransform.position) > 1.3f)
                {
                    //エネミーをプレイヤーの方向に進ませる
                    transform.position = transform.position + transform.forward * enemyMoveSpeed * Time.deltaTime;

                    animator.SetBool("Walk", true);
                }
            }

            if (state == EnemyState.Move)
            {
                //エネミーとプレイヤーの距離が１以内だったら、Walkをやめさせる
                if (Vector3.Distance(transform.position, targetTransform.position) < 1.3f)
                {
                    animator.SetBool("Walk", false);
                    SetState(EnemyState.Attack01);
                }
            }
        }

        if (state == EnemyState.Attack01)
        {
            //もし、攻撃中にプレイヤーが攻撃範囲外に行ったら、攻撃をやめる
            if (Vector3.Distance(transform.position, targetTransform.position) > 1.4f)
            {
                animator.SetBool("Attack", false);
                SetState(EnemyState.Move);
            }
        }

        if (state == EnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > FreezeTime)
            {
                SetState(EnemyState.Attack01);
            }
        }
    }

    public void SetState(EnemyState EState, Transform targetObject = null)
    {
        state = EState;

        if (EState == EnemyState.Wite)
        {
            state = EState;
            animator.SetBool("Walk", false);
        }
        else if (EState == EnemyState.Move)
        {
            targetTransform = targetObject;
            state = EState;
        }
        else if (EState == EnemyState.Attack01)
        {
            state = EState;
            animator.SetBool("Attack", true);
        }
        else if (EState == EnemyState.Freeze)
        {
            state = EState;
            animator.SetBool("Attack", false);
        }
        else if (EState == EnemyState.Death)
        {
            state = EState;
        }
    }
    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }

    public void SetDistination(Vector3 position)
    {
        destination = position;
    }

    public Vector3 GetDisnation()
    {
        return destination;
    }

    //デスカウント
    public void DeathCount()
    {
        enemyDeathCount += 1;
    }

    //　敵キャラの状態を取得するためのメソッド
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Sword")
        {
            EnemyHP -= 20;

            if (EnemyHP < 0)
            {
                animator.SetBool("Death", true);
                Destroy(this.gameObject,2);
                EnemyDeath = true;
                DeathCount();
            }
            else
            {
                animator.SetBool("Death", false);
            }
        }
    }

}
