using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public float CameraDistance;
    public float CameraHeight;
    public float CameraSpeed;

    // Start is called before the first frame update
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(MouseX) > 0.000001f)
        {
            MouseX = MouseX * 3;

            transform.RotateAround(Player.transform.position, Vector3.up, MouseX);
        }

        if (Mathf.Abs(MouseY) > 0.0000001f)
        {
            if ((CameraHeight - MouseY) < 1 || (CameraHeight - MouseY) > 6)
            {
                MouseY = 0;
            }
            CameraHeight -= MouseY / 12;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerCenter = Player.transform.position + Vector3.up * CameraHeight;

        Vector3 targetPosition = playerCenter - Player.transform.forward * CameraDistance;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, CameraSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(Player.transform);
    }
}
