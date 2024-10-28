using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_02 : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 currentPos;//���݂̃J�����ʒu
    Vector3 pastPos;//�ߋ��̃J�����ʒu

    Vector3 diff;//�ړ�����

    private float CameraHeight = 2;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̃v���C���[�̈ʒu�̎擾
        pastPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //------�J�����̈ړ�------

        //�v���C���[�̌��ݒn�̎擾
        currentPos = player.transform.position;

        diff = currentPos - pastPos;

        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 2.0f);//�J�������v���C���[�̈ړ�����������������

        pastPos = currentPos;


        //------�J�����̉�]------

        // �}�E�X�̈ړ��ʂ��擾
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(mx) > 0.0000001f)
        {
            // ��]���̓��[���h���W��Y��
            transform.RotateAround(player.transform.position, Vector3.up, mx);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(my) > 0.00000001f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, transform.right, -my);

            if ((CameraHeight - my) < 0 || (CameraHeight - my) > 6)
            {
                my = 0;
            }
            CameraHeight -= my / 16;
        }
    }
}
