using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using static UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    //�G�l�~�[�̗̑�
    [SerializeField] int EnemyHP = 120;

    //�G�l�~�[�̍U����
    [SerializeField] int Enemy_Attack = 30;

    public int enemyDeathCount = 0;

    public enum EnemyState
    {
        Idle, //�ҋ@
        Wite, //�ꎞ��~
        Move, //�ǐ� 
        Attack01, //�U���P
        Attack02, //�U���Q
        Freeze,   //�ڍs
        Death     //���S
    };

    public Rigidbody rb;
    public Animator animator;
    public EnemyState state; //�L�����̏��
    private Transform targetTransform;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�@�G�L�����̏�Ԃ�ݒ肷�邽�߂̃��\�b�h 
    public void SetState(EnemyState enemyState, Transform targetObject = null)
    {
        
    }

    //�f�X�J�E���g
    public void DeathCount()
    {
        enemyDeathCount += 1;
    }

    //�@�G�L�����̏�Ԃ��擾���邽�߂̃��\�b�h
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
