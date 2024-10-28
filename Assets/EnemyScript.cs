using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //エネミーの体力
    public int EnemyHP = 240;

    public int enemyDeathCount = 0;

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

    public Rigidbody rb;
    public Animator animator;
    public EnemyState state; //キャラの状態
    public Transform target;
    // オブジェクトの移動速度を格納する変数
    public float moveSpeed;
    // オブジェクトが停止するターゲットオブジェクトとの距離を格納する変数
    public float stopDistance;
    // オブジェクトがターゲットに向かって移動を開始する距離を格納する変数
    public float moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LockOn()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 変数 targetPos を作成してターゲットオブジェクトの座標を格納
        Vector3 targetPos = target.position;
        // 自分自身のY座標を変数 target のY座標に格納
        //（ターゲットオブジェクトのX、Z座標のみ参照）
        targetPos.y = transform.position.y;
        // オブジェクトを変数 targetPos の座標方向に向かせる
        transform.LookAt(targetPos);

        // 変数 distance を作成してオブジェクトの位置とターゲットオブジェクトの距離を格納
        float distance = Vector3.Distance(transform.position, target.position);
        // オブジェクトとターゲットオブジェクトの距離判定
        // 変数 distance（ターゲットオブジェクトとオブジェクトの距離）が変数 moveDistance の値より小さければ
        // さらに変数 distance が変数 stopDistance の値よりも大きい場合
        if (distance < moveDistance && distance > stopDistance)
        {
            // 変数 moveSpeed を乗算した速度でオブジェクトを前方向に移動する
            transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
            animator.SetBool("chase", true);
        }
        else
        {
            animator.SetBool("chase", false);
        }
    }

    //　敵キャラの状態を設定するためのメソッド 
    public void SetState(EnemyState enemyState, Transform targetObject = null)
    {
        
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

            if (EnemyHP <= 0)
            {
                animator.SetBool("Death", true);
                Destroy(this.gameObject,3);
                DeathCount();
            }
            else
            {
                animator.SetBool("Death", false);
            }
        }
    }

}
