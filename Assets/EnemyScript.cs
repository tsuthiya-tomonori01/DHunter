using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody rb;

    //�G�l�~�[�̗̑�
    [SerializeField] int EnemyHP = 120;

    //�G�l�~�[�̍U����
    [SerializeField] int Enemy_Attack = 30;

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

    public EnemyState state;
    private Transform targetTransform;
    private NavMeshAgent navMeshAgent;
    public Animator animator;
    [SerializeField]
    private PlayableDirector timeline;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        SetState(EnemyState.Idle);                                                                                                                                                                                                                                               
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.Move)
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
            // �G�̌������v���C���[�̕����ɏ������ς���
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            //�Z�o���������̊p�x��G�̊p�x�ɐݒ�
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    //�@�G�L�����̏�Ԃ�ݒ肷�邽�߂̃��\�b�h 
    public void SetState(EnemyState enemyState, Transform targetObject = null)
    {
        state = enemyState;

        if (enemyState == EnemyState.Idle)
        {
            navMeshAgent.isStopped = true;
            //animator.setbool("", false)
        }

        else if (enemyState == EnemyState.Move)
        {
            targetTransform = targetObject;
            navMeshAgent.SetDestination(targetTransform.position);
            navMeshAgent.isStopped = false;
            //animator.setbool("", false)
        }

        else if (enemyState == EnemyState.Attack01)
        {
            navMeshAgent.isStopped = true;
            timeline.Play();
            //animator.setbool("", false)
        }

        else if (enemyState == EnemyState.Attack02)
        {
            navMeshAgent.isStopped = true;
            timeline.Play();
            //animator.setbool("", false)
        }

        else if (enemyState == EnemyState.Freeze)
        {
            Invoke("ResetState", 2.0f);
        }
    }

    public EnemyState GetState()
    {
        return state;
    }

    public void FreezeState()
    {
        SetState(EnemyState.Freeze);
    }

    private void ResetState()
    {
        SetState(EnemyState.Idle);
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    public Vector3 GetDestination()
    {
        return destination;
    }

    //�@�G�L�����̏�Ԃ��擾���邽�߂̃��\�b�h
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
