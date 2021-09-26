using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject ballIndicator;

    private Rigidbody rigidBody;
    private LayerMask groundLayer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
        ballIndicator = Instantiate(ballIndicator);
        ballIndicator.SetActive(false);
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, maxDistance: 100, groundLayer))
        {
            ballIndicator.SetActive(true);
            ballIndicator.transform.position = hitInfo.point;
            ballIndicator.transform.localScale = new Vector3(
                (Mathf.Min(ballIndicator.transform.localScale.x - hitInfo.distance, 1f) * 0.2f), 
                ballIndicator.transform.localScale.y,
                (Mathf.Min(ballIndicator.transform.localScale.z - hitInfo.distance, 1f) * 0.2f));
        }
        else
        {
            ballIndicator.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // destroy when we pass below the floor
        if (rigidBody.position.y < 0)
        {
            Destroy(this.gameObject);
            Destroy(ballIndicator);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            Destroy(ballIndicator);
        }
    }
}
