using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public enum EnemyState
    {
        Wite, //�ꎞ��~
        Move, //�ǐ� 
        Attack01, //�U���P
        Attack02, //�U���Q
        Freeze,   //�ڍs
        Death     //���S
    };

    public Transform targetTransform;

    public Rigidbody rb;

    public Animator animator;

    public Transform target;

    public EnemyState state;
    
    bool EnemyDeath = false;

    //�G�l�~�[�̗̑�
    public int EnemyHP = 1200;

    public float enemyMoveSpeed;

    public float EnemyAngulSpeed;

    public int enemyDeathCount = 0;

    private float FreezeTime = 5.0f;

    private float elapsedTime;

    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = 1200;
        enemyDeathCount = 0;
        SetState(EnemyState.Wite);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyDeath == true)
        {
            return;
        }

        if (state == EnemyState.Move)
        {
            if (targetTransform == null)
            {
                SetState(EnemyState.Wite);
            }
            else
            {
                //�^�[�Q�b�g�̍��W������
                SetDistination(targetTransform.position);
                
                //�G�̌������v���C���[�̕����ɕς��Ă���
                var dir = (GetDisnation() - transform.position).normalized;

                //��Ɍ����Ȃ��悤�ɂ���
                dir.y = 0;

                //�G�l�~�[�̊p�x���v���C���[�̕����ɋ߂Â���
                Quaternion setRotation = Quaternion.LookRotation(dir);

                //�G�l�~�[�̊p�x�����߂��p�x�ɑ������
                transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, EnemyAngulSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetTransform.position) > 1.3f)
                {
                    //�G�l�~�[���v���C���[�̕����ɐi�܂���
                    transform.position = transform.position + transform.forward * enemyMoveSpeed * Time.deltaTime;

                    animator.SetBool("Walk", true);
                }
            }

            if (state == EnemyState.Move)
            {
                //�G�l�~�[�ƃv���C���[�̋������P�ȓ���������AWalk����߂�����
                if (Vector3.Distance(transform.position, targetTransform.position) < 1.3f)
                {
                    animator.SetBool("Walk", false);
                    SetState(EnemyState.Attack01);
                }
            }
        }

        if (state == EnemyState.Attack01)
        {
            //�����A�U�����Ƀv���C���[���U���͈͊O�ɍs������A�U������߂�
            if (Vector3.Distance(transform.position, targetTransform.position) > 1.4f)
            {
                animator.SetBool("Attack", false);
                SetState(EnemyState.Move);
            }
        }

        if (state == EnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > FreezeTime)
            {
                SetState(EnemyState.Attack01);
            }
        }
    }

    public void SetState(EnemyState EState, Transform targetObject = null)
    {
        state = EState;

        if (EState == EnemyState.Wite)
        {
            state = EState;
            animator.SetBool("Walk", false);
        }
        else if (EState == EnemyState.Move)
        {
            targetTransform = targetObject;
            state = EState;
        }
        else if (EState == EnemyState.Attack01)
        {
            state = EState;
            animator.SetBool("Attack", true);
        }
        else if (EState == EnemyState.Freeze)
        {
            state = EState;
            animator.SetBool("Attack", false);
        }
        else if (EState == EnemyState.Death)
        {
            state = EState;
        }
    }
    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public EnemyState GetState()
    {
        return state;
    }

    public void SetDistination(Vector3 position)
    {
        destination = position;
    }

    public Vector3 GetDisnation()
    {
        return destination;
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

            if (EnemyHP < 0)
            {
                animator.SetBool("Death", true);
                Destroy(this.gameObject,2);
                EnemyDeath = true;
                DeathCount();
            }
            else
            {
                animator.SetBool("Death", false);
            }
        }
    }

}
