using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Vector3 spawnLocation;

    private Rigidbody spawnedBall;

    // Start is called before the first frame update
    void Update()
    {
        if (spawnedBall == null)
        {
            spawnedBall = Instantiate(ball, spawnLocation, Quaternion.identity).GetComponent<Rigidbody>();
            spawnedBall.velocity = new Vector3(0, 20f, -5f);
        }
    }
}
