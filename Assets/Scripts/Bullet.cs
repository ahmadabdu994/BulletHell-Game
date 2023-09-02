using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector2 moveDirection;
    public float moveSpeed = 5f;

    public bool isHeatSeek;
    public bool PlayerBullet;

    private EnemyAI enemyAI;
    private UIManager uimanager;

   
    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();

    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); 
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy();
        }

        if(PlayerBullet == false && collision.gameObject.tag == "Player's HitPoint")
        {
            if(uimanager != null)
            {
                Destroy();
                uimanager.ReduceLive();
                print("Hit");
            }
           
        }

        if (PlayerBullet == true && collision.gameObject.tag == "Enemy")
        {
            if (uimanager != null)
            {
                    Destroy(collision.gameObject);
                
                Destroy();
                uimanager.AddScore(1);
            }
        }
    }
}
