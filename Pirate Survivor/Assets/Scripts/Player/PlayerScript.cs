using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;


    Rigidbody2D rb;

    


    PlayerStats player;

    void Start()
    {


        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();

        lastMovedVector = new Vector2(1, 0f); // so weapon doesnt fire before movement 
    }

    void Update()
    {
        InputManagment();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagment()
    {
        if (GameManager.instance.isGameOver)
        {
            return; 
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;



        if (moveDir.x !=0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);  // last moved x
        }


        if (moveDir.y !=0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); //last moved y
        }



        if(moveDir.x != 0 && moveDir.y != 0) 
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // whilemoving 


        }





    }

    private void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        rb.velocity = new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed);

        // Only change the rotation if there's some movement to avoid division by zero
        if (moveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f; // Subtracting 90 degrees to make the sprite look upwards when pressing 'W'
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}


