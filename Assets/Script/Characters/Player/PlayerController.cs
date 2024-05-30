using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirs))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 5;
    Vector2 moveInput;
    TouchingDirs touchingDirs;
    HealthSystem healthSystem;


    public float CurrentMoveSpeed { get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirs.IsOnWall)
                {
                    if (touchingDirs.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // Air Move
                        return airWalkSpeed;
                    }
                }
               
            }
            return 0; // idle speed = 0 or movement locked
            


        } }

    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;

    [SerializeField]
    private bool _isBlocking = false;

    public bool IsMoving { 
        get { return _isMoving; }
        private set { 
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value&&!IsBlocking);

        }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value && !IsBlocking);
        }
    }

    public bool IsBlocking
    {
        get { return _isBlocking; }
        private set
        {
            _isBlocking = value;
            animator.SetBool(AnimationStrings.block, value);
        }
    }

    public bool _isFacingRight = true;
    // control the player facing
    public bool IsFacingRight { 
        get { return _isFacingRight; } 
        private set {
            if (_isFacingRight != value) 
            {   // flip the local scale to make player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value; } }


    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirs = GetComponent<TouchingDirs>();
        healthSystem = GetComponent<HealthSystem>();    
      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool LockVelocity { get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }
    private void FixedUpdate()
    {
        if (!LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        
        animator.SetFloat(AnimationStrings.dy, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2> ();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDir(moveInput);
        }
        else
        {
            IsMoving = false;
        }

        

        
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        } else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO check if alive as well
        if (context.started && touchingDirs.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            // set y velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse); 
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
            animator.SetTrigger(AnimationStrings.blockk);
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsBlocking = ForceBar.instance.UseForce(10);
            //IsBlocking = true;
            animator.SetTrigger(AnimationStrings.blockk);
        }
        else if (context.canceled)
        {
            IsBlocking = false;
            ForceBar.instance.ReForce();
        }
            
          
       
    }

    private void SetFacingDir(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight= false;
        }
    }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    

    public void OnHit(int damage, Vector2 knockback)
    {
        LockVelocity = true;
        rb.velocity = new Vector2(knockback.x,rb.velocity.y+knockback.y);
      
        
    }
    public void addHealthBar()
    {
        healthSystem.addHealthBar();
    }

    
}
