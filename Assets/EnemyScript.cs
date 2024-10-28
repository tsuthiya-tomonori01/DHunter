using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //�G�l�~�[�̗̑�
    public int EnemyHP = 240;

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
    public Transform target;
    // �I�u�W�F�N�g�̈ړ����x���i�[����ϐ�
    public float moveSpeed;
    // �I�u�W�F�N�g����~����^�[�Q�b�g�I�u�W�F�N�g�Ƃ̋������i�[����ϐ�
    public float stopDistance;
    // �I�u�W�F�N�g���^�[�Q�b�g�Ɍ������Ĉړ����J�n���鋗�����i�[����ϐ�
    public float moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LockOn()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �ϐ� targetPos ���쐬���ă^�[�Q�b�g�I�u�W�F�N�g�̍��W���i�[
        Vector3 targetPos = target.position;
        // �������g��Y���W��ϐ� target ��Y���W�Ɋi�[
        //�i�^�[�Q�b�g�I�u�W�F�N�g��X�AZ���W�̂ݎQ�Ɓj
        targetPos.y = transform.position.y;
        // �I�u�W�F�N�g��ϐ� targetPos �̍��W�����Ɍ�������
        transform.LookAt(targetPos);

        // �ϐ� distance ���쐬���ăI�u�W�F�N�g�̈ʒu�ƃ^�[�Q�b�g�I�u�W�F�N�g�̋������i�[
        float distance = Vector3.Distance(transform.position, target.position);
        // �I�u�W�F�N�g�ƃ^�[�Q�b�g�I�u�W�F�N�g�̋�������
        // �ϐ� distance�i�^�[�Q�b�g�I�u�W�F�N�g�ƃI�u�W�F�N�g�̋����j���ϐ� moveDistance �̒l��菬�������
        // ����ɕϐ� distance ���ϐ� stopDistance �̒l�����傫���ꍇ
        if (distance < moveDistance && distance > stopDistance)
        {
            // �ϐ� moveSpeed ����Z�������x�ŃI�u�W�F�N�g��O�����Ɉړ�����
            transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
            animator.SetBool("chase", true);
        }
        else
        {
            animator.SetBool("chase", false);
        }
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
