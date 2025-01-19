using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyControlScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Freeze
    };

    //パラメータ関数の定義
    public EnemyState state; //キャラの状態
    private Transform targetTransform; //ターゲットの情報
    private NavMeshAgent navMeshAgent; //NavMeshAgentコンポーネント
    public Animator animator; //Animatorコンポーネント
    [SerializeField]
    private Vector3 destination; //目的地の位置情報を格納するためのパラメータ

    // Start is called before the first frame update
    void Start()
    {
        //キャラのNavMeshAgentコンポーネントとnavMeshAgentを関連付ける
        navMeshAgent = GetComponent<NavMeshAgent>();

        //キャラモデルのAnimatorコンポーネントとanimatorを関連付ける
        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        SetState(EnemyState.Idle); //初期状態をIdle状態に設定する
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを目的地にして追跡する
        if (state == EnemyState.Chase)
        {
            if (targetTransform == null)
            {
                SetState(EnemyState.Idle);
            }
            else
            {
                SetDestination(targetTransform.position);
                navMeshAgent.SetDestination(GetDestination());
            }
            //　敵の向きをプレイヤーの方向に少しづつ変える
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            //　算出した方向の角度を敵の角度に設定
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    //　敵キャラの状態を設定するためのメソッド 
    public void SetState(EnemyState tempState, Transform targetObject = null)
    {
        state = tempState;

        if (tempState == EnemyState.Idle)
        {
            navMeshAgent.isStopped = true; //キャラの移動を止める
            animator.SetBool("Walk", false); //アニメーションコントローラーのフラグ切替（Chase⇒IdleもしくはFreeze⇒Idle）
        }
        else if (tempState == EnemyState.Chase)
        {
            targetTransform = targetObject; //ターゲットの情報を更新
            navMeshAgent.SetDestination(targetTransform.position); //目的地をターゲットの位置に設定
            navMeshAgent.isStopped = false; //キャラを動けるようにする
            animator.SetBool("Walk", true); //アニメーションコントローラーのフラグ切替（Idle⇒Chase）
        }
        else if (tempState == EnemyState.Attack)
        {
            navMeshAgent.isStopped = true; //キャラの移動を止める
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);

        }
        else if (tempState == EnemyState.Freeze)
        {
            Invoke("ResetState", 2.0f);
        }
    }

    //　敵キャラの状態を取得するためのメソッド
    public EnemyState GetState()
    {
        return state;
    }

    //　タイムラインで状態をFreeze状態に設定するためのメソッド
    public void FreezeState()
    {
        SetState(EnemyState.Freeze);
    }

    //　目的地を設定する
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //　目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
    }
}
