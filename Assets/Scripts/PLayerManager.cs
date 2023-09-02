using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    FireBullet firebullet;

    Vector2 moveDirection;
    Vector2 mousePosition;

   
    void Start()
    {
        
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        

        moveDirection = new Vector2(moveX, moveY).normalized;

       
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

       
    }
}
