using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public static float moveSpeed = 10;
    [SerializeField] private float jumpSpeed=10;
    [Range(0, .3f)]
    new private Rigidbody2D rigidbody;

    private float inputX;
    public static bool isJump=false;
    public static bool isGround = true;
    [SerializeField] private LayerMask layer;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        isGround = Physics2D.Raycast(transform.position, Vector2.down,0.2f);

        Debug.Log(isGround?"在地上":"上天了");

        Move();
        Jump();
        Flip();
    }

    private void Flip()
    {
        if (inputX == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputX == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space) )
        {
            isJump= true;
            rigidbody.velocity = new Vector2(0, jumpSpeed);
        }
    }

    private void Move()
    {
        rigidbody.velocity = new Vector2(inputX * moveSpeed, rigidbody.velocity.y);

    }
}
