using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private float moveH;
    private float moveV;

    public float moveSpeed;


    private Vector2 moveDirection;
    void FixedUpdate()
    {
        Movement();
        ProcessInputs();

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }

    // Update is called once per frame
   
    private void ProcessInputs()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveH, moveV).normalized;
    }
    private void Movement()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
