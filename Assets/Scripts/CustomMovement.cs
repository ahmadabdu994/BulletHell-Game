using UnityEngine;

public class CustomMovement : MonoBehaviour
{
    public float speed = 5.0f; // Default speed
    public Vector3 targetPosition;
    private bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Calculate the direction to the target position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Calculate the distance to move this frame based on speed
            float distanceThisFrame = speed * Time.deltaTime;

            // Move the object towards the target
            transform.Translate(direction * distanceThisFrame);

            // Check if we reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < distanceThisFrame)
            {
                isMoving = false;
            }
        }
    }
}
