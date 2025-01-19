using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyControlScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Freeze
    };

    //�p�����[�^�֐��̒�`
    public EnemyState state; //�L�����̏��
    private Transform targetTransform; //�^�[�Q�b�g�̏��
    private NavMeshAgent navMeshAgent; //NavMeshAgent�R���|�[�l���g
    public Animator animator; //Animator�R���|�[�l���g
    [SerializeField]
    private Vector3 destination; //�ړI�n�̈ʒu�����i�[���邽�߂̃p�����[�^

    // Start is called before the first frame update
    void Start()
    {
        //�L������NavMeshAgent�R���|�[�l���g��navMeshAgent���֘A�t����
        navMeshAgent = GetComponent<NavMeshAgent>();

        //�L�������f����Animator�R���|�[�l���g��animator���֘A�t����
        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        SetState(EnemyState.Idle); //������Ԃ�Idle��Ԃɐݒ肷��
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[��ړI�n�ɂ��ĒǐՂ���
        if (state == EnemyState.Chase)
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
            //�@�G�̌������v���C���[�̕����ɏ����Âς���
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            //�@�Z�o���������̊p�x��G�̊p�x�ɐݒ�
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    //�@�G�L�����̏�Ԃ�ݒ肷�邽�߂̃��\�b�h 
    public void SetState(EnemyState tempState, Transform targetObject = null)
    {
        state = tempState;

        if (tempState == EnemyState.Idle)
        {
            navMeshAgent.isStopped = true; //�L�����̈ړ����~�߂�
            animator.SetBool("Walk", false); //�A�j���[�V�����R���g���[���[�̃t���O�ؑցiChase��Idle��������Freeze��Idle�j
        }
        else if (tempState == EnemyState.Chase)
        {
            targetTransform = targetObject; //�^�[�Q�b�g�̏����X�V
            navMeshAgent.SetDestination(targetTransform.position); //�ړI�n���^�[�Q�b�g�̈ʒu�ɐݒ�
            navMeshAgent.isStopped = false; //�L�����𓮂���悤�ɂ���
            animator.SetBool("Walk", true); //�A�j���[�V�����R���g���[���[�̃t���O�ؑցiIdle��Chase�j
        }
        else if (tempState == EnemyState.Attack)
        {
            navMeshAgent.isStopped = true; //�L�����̈ړ����~�߂�
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);

        }
        else if (tempState == EnemyState.Freeze)
        {
            Invoke("ResetState", 2.0f);
        }
    }

    //�@�G�L�����̏�Ԃ��擾���邽�߂̃��\�b�h
    public EnemyState GetState()
    {
        return state;
    }

    //�@�^�C�����C���ŏ�Ԃ�Freeze��Ԃɐݒ肷�邽�߂̃��\�b�h
    public void FreezeState()
    {
        SetState(EnemyState.Freeze);
    }

    //�@�ړI�n��ݒ肷��
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //�@�ړI�n���擾����
    public Vector3 GetDestination()
    {
        return destination;
    }
}
