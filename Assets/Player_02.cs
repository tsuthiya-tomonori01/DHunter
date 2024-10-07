using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_02 : MonoBehaviour
{
    [SerializeField]
    float moveSpeedIn;//�v���C���[�̈ړ����x�����


    Rigidbody playerRb;//�v���C���[��Rigidbody

    Vector3 moveSpeed;//�v���C���[�̈ړ����x

    Vector3 currentPos;//�v���C���[�̌��݂̈ʒu
    Vector3 pastPos;//�v���C���[�̉ߋ��̈ʒu

    Vector3 delta;//�v���C���[�̈ړ���

    Quaternion playerRot;//�v���C���[�̐i�s�����������N�H�[�^�j�I��

    float currentAngularVelocity;//���݂̉�]�e���x

    [SerializeField]
    float maxAngularVelocity = Mathf.Infinity;//�ő�̉�]�p���x[deg/s]

    [SerializeField]
    float smoothTime = 0.1f;//�i�s�����ɂ����邨���悻�̎���[s]

    float diffAngle;//���݂̌����Ɛi�s�����̊p�x

    float rotAngle;//���݂̉�]����p�x

    Quaternion nextRot;//�ǂ񂭂炢��]���邩

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        pastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //------�v���C���[�̈ړ�------

        //�J�����ɑ΂��đO���擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //�J�����ɑ΂��ĉE���擾
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        //moveVelocity��0�ŏ���������
        moveSpeed = Vector3.zero;

        //�ړ�����
        if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Mode", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveSpeed = -moveSpeedIn * cameraRight;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Mode_B", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveSpeed = moveSpeedIn * cameraRight;
        }

        else
        {
            animator.SetBool("Mode", false);

            animator.SetBool("Mode_B", false);

            Quaternion.Euler(0, 0, 0);
        }

        //Attack�Ăяo��
        PlayerAttack();

        //Move���\�b�h�ŁA�͉����Ă��炤
        Move();

        //����������
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            //playerRb.velocity = Vector3.zero;
            // playerRb.angularVelocity = Vector3.zero;
        }

        //------�v���C���[�̉�]------

        //���݂̈ʒu
        currentPos = transform.position;

        //�ړ��ʌv�Z
        delta = currentPos - pastPos;
        delta.y = 0;

        //�ߋ��̈ʒu�̍X�V
        pastPos = currentPos;

        if (delta == Vector3.zero)
            return;

        playerRot = Quaternion.LookRotation(delta, Vector3.up);

        diffAngle = Vector3.Angle(transform.forward, delta);

        //Vector3.SmoothDamp��Vector3�^�̒l�����X�ɕω�������
        //Vector3.SmoothDamp (���ݒn, �ړI�n, ref ���݂̑��x, �J�ڎ���, �ō����x);
        rotAngle = Mathf.SmoothDampAngle(0, diffAngle, ref currentAngularVelocity, smoothTime, maxAngularVelocity);

        nextRot = Quaternion.RotateTowards(transform.rotation, playerRot, rotAngle);

        transform.rotation = nextRot;
    }

    /// <summary>
    /// �ړ������ɗ͂�������
    /// </summary>
    private void Move()
    {
        //playerRb.AddForce(moveSpeed, ForceMode.Force);

        playerRb.velocity = moveSpeed;
    }

    void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Mode_Attack", true);
        }
        else
        {
            animator.SetBool("Mode_Attack", false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "CombatTrigger_01")
        {
            Debug.Log("Hit");

        }

        else if (other.gameObject.tag == "CombatTrigger_02")
        {
            Debug.Log("Hit");

        }

        else if (other.gameObject.tag == "CombatTrigger_03")
        {
            Debug.Log("Hit");

        }
    }
}
