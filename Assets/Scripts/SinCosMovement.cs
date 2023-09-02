using UnityEngine;

public class SinCosMovement : MonoBehaviour
{
    public float amplitude = 2f;   // Amplitude of the sine wave
    public float frequency = 1f;   // Frequency of the sine wave
    public float speed = 2f;       // Speed of the GameObject's movement

    private float time = 0f;       // Time variable for sine and cosine functions

    private void Update()
    {
        // Increment time based on speed
        time += Time.deltaTime * speed;

        // Calculate new positions using sine and cosine functions
        float xPosition = amplitude * Mathf.Sin(frequency * time);
        float yPosition = amplitude * Mathf.Cos(frequency * time);

        // Set the GameObject's position
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
    }
}



