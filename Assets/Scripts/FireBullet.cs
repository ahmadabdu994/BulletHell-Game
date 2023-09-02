using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    private bool isFiring = false;

    public GameObject bulletPool;
    public enum BulletType { unDirected, Directed, Random };
    public BulletType bulletType;
    public enum RotationType { noRotation, infiinateRotation, LoopingRootation };
    public RotationType rotationType;


    [SerializeField]
    private int bulletAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    [SerializeField]
    private float startRotation = 90f, endRotation = 270f;

    public float fireRate = 2f;
    public float rotationSpeed = 2f;

    public bool RotationtoS;
    public bool RotationtoE;

    public bool Player;

    private Vector2 bulletMoveDirection;

    public AudioSource src1;
    public AudioClip sfxSt;

    public void Awake()
    {
        if (rotationType == RotationType.infiinateRotation || rotationType == RotationType.noRotation)
        {
            //  startRotation.
        }
    }
    void Start()
    {
        if (Player == false)
        {
              new WaitForSeconds(4f);
            InvokeRepeating("Fire", 0f, fireRate);
        }


        RotationtoS = true;
        RotationtoE = false;


    }

    public void StartFiring()
    {
        if (!isFiring)
        {
            isFiring = true;
            InvokeRepeating("Fire", 0f, fireRate);
        }
    }

    public void StopFiring()
    {
        if (isFiring)
        {
            isFiring = false;
            CancelInvoke("Fire");
        }
    }

    public void Fire()
    {
        if(Player == true)
        {
            src1.clip = sfxSt;
            src1.Play();
        }
            
        for (int i = 0; i < bulletAmount + 1; i++)
        {
            Vector2 bulDir;

            if (bulletType == BulletType.unDirected)
            {
                // Calculate angle for undirected bullets
                float angle = Random.Range(startAngle, endAngle);
                float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
                bulDir = (new Vector2(bulDirX, bulDirY) - (Vector2)transform.position).normalized;
            }
            else if (bulletType == BulletType.Directed)
            {
                // Get the player gameobject
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                // Find the direction to the player
                Vector2 directionToPlayer = player.transform.position - transform.position;

                // Set the bullet's direction to the player
                bulDir = directionToPlayer.normalized;
            }
            else
            {
                // Calculate a random rotation within the specified range
                float randomRotation = Random.Range(startRotation, endRotation);
                Quaternion rotation = Quaternion.Euler(0f, 0f, randomRotation);

                // Calculate the direction based on the random rotation
                bulDir = rotation * Vector2.up;
            }

            GameObject bul = bulletPool.GetComponent<BulletPool>().GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);
        }
    }
    void Update()
    {
        if (rotationType == RotationType.infiinateRotation)
        {
            StartCoroutine(InfinateRotation());

        }

        if (rotationType == RotationType.LoopingRootation)
        {
            StartCoroutine(LoopingRotation());

        }

        if(Player == true && Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("Fire", 0f, fireRate);
        }

        if (Player == true && Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }

    }

    void OnDestroy()
    {
        // Cancel any ongoing invocations of the "Fire" method
        CancelInvoke("Fire");
    }

    IEnumerator InfinateRotation()
    {


        Vector3 eulerRotation = transform.rotation.eulerAngles;

        // Add the rotation speed to the current rotation
        eulerRotation.z += rotationSpeed * Time.deltaTime;

        // Set the new rotation
        transform.rotation = Quaternion.Euler(eulerRotation);

        yield return new WaitForSeconds(0f);
    }

    IEnumerator LoopingRotation()
    {
        Vector3 eulerRotation = transform.rotation.eulerAngles;

        if (RotationtoS == true)
        {
            // Add the rotation speed to the current rotation
            eulerRotation.z += rotationSpeed * Time.deltaTime;

            // Set the new rotation
            transform.rotation = Quaternion.Euler(eulerRotation);
            if (eulerRotation.z >= startRotation)
            {
                RotationtoE = true;
                RotationtoS = false;
            }

        }

        if (RotationtoE == true)
        {
            // Add the rotation speed to the current rotation
            eulerRotation.z -= rotationSpeed * Time.deltaTime;

            // Set the new rotation
            transform.rotation = Quaternion.Euler(eulerRotation);

            if (eulerRotation.z <= endRotation)
            {
                RotationtoE = false;
                RotationtoS = true;
            }
        }

        yield return new WaitForSeconds(0f);
    }
}