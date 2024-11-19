using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{

    [SerializeField]
    private EnemyScript enemyScript;
    [SerializeField]
    private SphereCollider searchArea;
    [SerializeField]
    private float searchAngle = 130f;

    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //　主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //　サーチする角度内だったら発見
            if (angle <= searchAngle)
            {
                enemyScript.SetState(EnemyScript.EnemyState.Move, other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyScript.SetState(EnemyScript.EnemyState.Wite);
        }
    }

#if UNITY_EDITOR
    //　サーチする角度表示
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }
#endif
}
