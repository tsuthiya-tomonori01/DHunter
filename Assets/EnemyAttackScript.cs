using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public Animator animator;

    private GameObject player;

    EnemyScript enemyScript;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            //�v���C���[���Z���T�[���ɂ�����U������
            animator.SetBool("Attack", true);
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        //�v���C���[���Z���T�[�O�ɂ�����U�������A�ǐՂ���
        if (player)
        {
            animator.SetBool("Attack", false);
        }
    }
}
