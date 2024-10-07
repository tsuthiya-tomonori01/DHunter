using UnityEngine;
using Input = UnityEngine.Input;

public class PlayetScript : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject Camera;

    public Animator animator;

    float JumpSpeed = 80.0f;

    float playerSpeed = 6.0f;

    public bool JumpFlag = false;

    //int PlayerHP = 240;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion.Euler(0, 0, 0);
        JumpFlag = false;
    }

    void playerMove(float MouseX)
    {
        if (Mathf.Abs(MouseX) > 0.00001f)
        {
            MouseX = MouseX * 4;

            transform.RotateAround(transform.position, Vector3.up, MouseX);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        playerMove(MouseX);

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Mode", true);

            transform.position += transform.forward * playerSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * playerSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * playerSpeed * Time.deltaTime;

            animator.SetBool("Mode_B", true);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * playerSpeed * Time.deltaTime;
        }

        else
        {
            animator.SetBool("Mode", false);

            animator.SetBool("Mode_B", false);

            Quaternion.Euler(0, 0, 0);
        }

        PlayerAttack();

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpFlag = true;

            if (JumpFlag == true)
            {
                transform.position += transform.up * JumpSpeed * Time.deltaTime;
                JumpFlag = false;
            }
        }
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
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "CombatTrigger_02")
        {
            Debug.Log("Hit");
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "CombatTrigger_03")
        {
            Debug.Log("Hit");
            Destroy(other.gameObject);
        }
    }
}
