using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementPattern
    {
        Linear,
        Circular
    }

    public MovementPattern pattern = MovementPattern.Linear;
    public float speed = 5f;
    public float amplitude = 2f;
    public float frequency = 1f;

    private Vector3 startPosition;
    private float startTime;

    private void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        switch (pattern)
        {
            case MovementPattern.Linear:
                LinearMovement();
                break;
            case MovementPattern.Circular:
                CircularMovement();
                break;
        }
    }

    private void LinearMovement()
    {
        transform.position = startPosition + Vector3.right * (speed * Time.time);
    }

    private void CircularMovement()
    {
        float yOffset = Mathf.Sin((Time.time - startTime) * frequency) * amplitude;
        transform.position = startPosition + Vector3.right * (speed * (Time.time - startTime)) + Vector3.up * yOffset;
    }
}