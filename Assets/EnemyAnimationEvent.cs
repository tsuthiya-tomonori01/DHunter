using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
    }

    public void StateEnd()
    {
        enemyScript.SetState(EnemyScript.EnemyState.Freeze);
    }
}
