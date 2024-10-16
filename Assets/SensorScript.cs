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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
