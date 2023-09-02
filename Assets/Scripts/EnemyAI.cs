using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public GameObject[] weaponPrefabs;
    private int currentWeaponIndex = 0;

    public int EnemieSc;
    public int eHealth;

    UIManager uIManager;

    public enum MovementPattern
    {
        Linear,
        Circular,
        Target
    }

    public enum MovementDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    public MovementDirection movementDirection = MovementDirection.Right;
    public MovementPattern pattern = MovementPattern.Linear;

    public float amplitude = 2f;
    public float frequency = 1f;
    public float speed = 5f;

    private Vector3 startPosition;
    private float startTime;
    private int targetPoint = 0;

    private void Start()
    {
        StartCoroutine(StartShootingAfterDelay());
        targetPoint = 0;

        startPosition = transform.position;
        startTime = Time.time;
    }

    private IEnumerator StartShootingAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed

        StartCoroutine(SelectWeapon());
    }

    private IEnumerator SelectWeapon()
    {
        while (true)
        {
            if (currentWeaponIndex > 0)
            {
                weaponPrefabs[currentWeaponIndex - 1].SetActive(false);
            }

            weaponPrefabs[currentWeaponIndex].SetActive(true);

            FireBullet firebull = weaponPrefabs[currentWeaponIndex].GetComponent<FireBullet>();
            firebull.StartFiring();

            yield return new WaitForSeconds(5);

            weaponPrefabs[currentWeaponIndex].SetActive(false);
            firebull.StopFiring();

            currentWeaponIndex = (currentWeaponIndex + 1) % weaponPrefabs.Length;
        }
    }

    private void Update()
    {
        switch (pattern)
        {
            case MovementPattern.Linear:
                LinearMovement();
                break;
            case MovementPattern.Circular:
                CircularMovement();
                break;
            case MovementPattern.Target:
                TargetMovement();
                break;
        }
    }

    private void LinearMovement()
    {
        Vector3 movement = GetMovementDirectionVector(movementDirection);
        transform.position = startPosition + movement * (speed * Time.time);
    }

    private void CircularMovement()
    {
        float yOffset = Mathf.Sin((Time.time - startTime) * frequency) * amplitude;
        Vector3 movement = GetMovementDirectionVector(movementDirection);
        transform.position = startPosition + movement * (speed * (Time.time - startTime)) + Vector3.up * yOffset;
    }

    private Vector3 GetMovementDirectionVector(MovementDirection direction)
    {
        switch (direction)
        {
            case MovementDirection.Right:
                return Vector3.right;
            case MovementDirection.Left:
                return Vector3.left;
            case MovementDirection.Up:
                return Vector3.up;
            case MovementDirection.Down:
                return Vector3.down;
            default:
                return Vector3.zero;
        }
    }

    private void TargetMovement()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTargetInt();
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        float elapsedTime = Time.time - startTime;
    }

    private void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }

    public void ReduceLive()
    {
        eHealth -= 1;
        if (eHealth <= 0)
        {
            uIManager.AddScore(EnemieSc);
        }
    }

    
}