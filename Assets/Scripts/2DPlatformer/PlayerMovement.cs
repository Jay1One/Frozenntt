using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterMovement
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private Transform graphics;

    [SerializeField] private float JumpForce=5f;
    [SerializeField] private Transform helpers;
    //[SerializeField] private Animator animator;
    private Rigidbody2D rigidbody;
    private bool start = false;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Inputmanager.JumpAction += OnJump;
    }
    
   
    private void OnDestroy()
    {
        Inputmanager.JumpAction -= OnJump;
    }

    private void FixedUpdate()
    {
        if (IsFrizing)
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.x = 0f;
            rigidbody.velocity = velocity;
            return;
        }
        Vector2 direction=new Vector2(Inputmanager.HorizontalAxis,0f);
       

        if (!IsGrounded())
        {
            direction *= 0.5f;
        }
        
        Move(direction);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            start = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
        }
        if (IsGrounded() && start)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
            //animator.SetFloat("Speed",Mathf.Abs(rigidbody.velocity.x));
        }
        // else
        // {
        //     animator.SetFloat("Speed",0f);
        // }
        // if (Mathf.Abs(rigidbody.velocity.x) < 0.01f)
        // {
        //     return;
        // }
        if (!IsGrounded())
        {
            float xScale = rigidbody.velocity.x > 0 ? 1f : -1f;
            if (xScale < 0 && graphics.localScale.x < 0)
            {
                return;
            }

            if (xScale > 0 && graphics.localScale.x > 0)
            {
                return;
            }

            graphics.localScale = new Vector3(xScale, 1f, 1f);

            float xAngle = rigidbody.velocity.x > 0 ? 0f : 180f;
            helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);
        }
    }

    public override void Move(Vector2 direction)
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = direction.x * maxSpeed;
        rigidbody.velocity = velocity;
    }

    private bool IsGrounded()
    {
        Vector2 point = transform.position;
        if (gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.up)
        {
            print("");
            return false;
        }
        point.y -= 0.1f;//чтобы избежать рейкаста в самого себя
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, 0.2f);
        return hit.collider!=null;
    }
    public override void Stop(float time)
    {
        throw new System.NotImplementedException();
    }

    public override void Jump(float force)
    {
       // rigidbody.AddForce(new Vector2(0f,force),ForceMode2D.Impulse);
    }

    private void OnJump(float inputForce)
    {
        if (IsGrounded())
        {
            Jump(inputForce*JumpForce);
        }
    }
}
