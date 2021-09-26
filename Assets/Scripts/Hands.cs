using System;
using System.Linq;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] private float maxSetHeight = 5f;
    [SerializeField] private GameObject cursor;
    [SerializeField] private float handsHeight = 1.2f;

    private int ballMask;
    private Vector3 handsPosition;
    private float handsRadius = 0.5f;
    private bool setBallPressed;
    private AnimatorManager animator;

    private void Start()
    {
        ballMask = LayerMask.GetMask("Ball");
        animator = GetComponent<AnimatorManager>();
    }

    void Update()
    {
        handsPosition = new Vector3(transform.position.x, transform.position.y + handsHeight, transform.position.z);
        setBallPressed = Input.GetButton("Fire1");
        
        PrepareSetAnimation();
    }

	private void PrepareSetAnimation()
	{
        if (setBallPressed)
        {
            animator.PrepareSetAnimation(true);
        }
        else
        {
            animator.PrepareSetAnimation(false);
        }
    }

	private void FixedUpdate()
    {
        var overlap = Physics.OverlapSphere(handsPosition, handsRadius, ballMask);
        if (setBallPressed && overlap.Length > 0)
        {
            var ball = overlap.First().GetComponent<Rigidbody>();
            ball.velocity = CalculateLaunchVelocity(ball.transform.position, cursor.transform.position, maxSetHeight);
            setBallPressed = false;

            animator.PlaySetBallAnimation();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + handsHeight, transform.position.z), handsRadius);
    }

    private Vector3 CalculateLaunchVelocity(Vector3 source, Vector3 target, float maxheight)
    {
        float displacementY = target.y - source.y;
        Vector3 displacementXZ = new Vector3(target.x - source.x, 0, target.z - source.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * maxheight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * maxheight / Physics.gravity.y) + Mathf.Sqrt(2 * (displacementY - maxheight) / Physics.gravity.y));

        return velocityXZ + velocityY;
    }
}
