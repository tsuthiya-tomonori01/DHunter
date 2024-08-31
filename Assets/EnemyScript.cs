using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody rb;

    //エネミーの体力
    [SerializeField] int EnemyHP = 120;

    //エネミーの攻撃力
    //[SerializeField] int Enemy_Attack = 30;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = 240;

        rb = GetComponent<Rigidbody>();
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

            if (EnemyHP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
