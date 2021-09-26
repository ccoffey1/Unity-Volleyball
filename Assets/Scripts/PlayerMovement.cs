using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpheight;

    private Vector3 velocity;
    private CharacterController controller;
    private AnimatorManager animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<AnimatorManager>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
		}

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (isGrounded)
		{
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
			{
                Jump();
			}
        }

        AnimateMovement(vertical, horizontal);

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime; // calc gravity
        controller.Move(velocity * Time.deltaTime); // apply gravity
    }

    private void OnDrawGizmosSelected()
    {
        // draw our ground check sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckDistance);
    }

    private void AnimateMovement(float vertical, float horizontal)
	{
        animator.UpdateAnimatorValues(vertical, horizontal);
    }

    private void Idle()
	{
	}

    private void Walk()
	{
        moveSpeed = walkSpeed;
	}

    private void Run()
	{
        moveSpeed = runSpeed;
	}

    private void Jump()
	{
        velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
	}
}
