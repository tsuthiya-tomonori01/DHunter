using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SensorScript : MonoBehaviour
{
    [SerializeField]
    private SphereCollider searchArea = default;
    [SerializeField]
    private float searchAngle = 45f;
    private LayerMask obstacleLayer = default;
    private EnemyScript enemyMove = default;

    // Start is called before the first frame update
    private void Start()
    {
        enemyMove = transform.parent.GetComponent<EnemyScript>();
    }

    private void OnTriggerStay(Collider target)
    {
        if (target.tag == "Player")
        {
            var playerDirection = target.transform.position - transform.position;

            var angle = Vector3.Angle(transform.forward, playerDirection);

            if (angle <= searchAngle)
            {
                if (!Physics.Linecast(transform.position + Vector3.up, target.transform.position + Vector3.up, obstacleLayer))
                {
                    if (Vector3.Distance(target.transform.position, transform.position) <= searchArea.radius * 0.5f
                        && Vector3.Distance(target.transform.position, transform.position) >= searchArea.radius * 0.05f)
                    {
                        enemyMove.SetState(EnemyScript.EnemyState.Attack01);
                    }
                    else if (Vector3.Distance(target.transform.position, transform.position) <= searchArea.radius
                        && Vector3.Distance(target.transform.position, transform.position) >= searchArea.radius * 0.5f
                        && enemyMove.state == EnemyScript.EnemyState.Idle)
                    {
                        enemyMove.SetState(EnemyScript.EnemyState.Move, target.transform); // センサーに入ったプレイヤーをターゲットに設定して、追跡状態に移行する。
                    }
                }    
            }
            else if (angle > searchAngle)
            {
                enemyMove.SetState(EnemyScript.EnemyState.Idle);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
