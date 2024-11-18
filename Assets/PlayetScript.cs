using UnityEngine;
using Input = UnityEngine.Input;

public class PlayetScript : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject Camera;

    public Animator animator;

    float playerSpeed = 6.0f;

    private bool PlayerIsDead = false;

    private int PlayerHP = 240;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion.Euler(0, 0, 0);
        PlayerHP = 240;
        PlayerIsDead = false;
    }

    void playerMove(float MouseX)
    {
        if (PlayerIsDead == true)
        {
            return;
        }

        if (Mathf.Abs(MouseX) > 0.00000001f)
        {
            transform.RotateAround(transform.position, Vector3.up, MouseX);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsDead == true)
        {
            return;
        }

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
            Quaternion.Euler(0, -90, 0);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * playerSpeed * Time.deltaTime;

            animator.SetBool("Mode_B", true);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * playerSpeed * Time.deltaTime;
            Quaternion.Euler(0, 90, 0);
        }

        else
        {
            animator.SetBool("Mode", false);

            animator.SetBool("Mode_B", false);

            Quaternion.Euler(0, 0, 0);
        }

        PlayerAttack();
        PlayerDead();
    }

    void FixedUpdate()
    {
        ////‰ñ”ð
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    animator.SetBool("Avoidance", true);
        //}
        //else
        //{
        //    animator.SetBool("Avoidance", false);
        //}
    }

    void PlayerAttack()
    {
        if (PlayerIsDead == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Mode_Attack", true);
        }
        else
        {
            animator.SetBool("Mode_Attack", false);
        }
    }

    void PlayerDead()
    {
        if (PlayerHP <= 0)
        {
            PlayerIsDead = true;
        }
    }

    public bool GetPlayerIsDead()
    {
        return PlayerIsDead;
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
