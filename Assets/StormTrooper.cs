
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirs),typeof(Damagable))]
public abstract class StormTrooper : MonoBehaviour
{
    public float walkSpeed;
    public float walkSpeedLerpStopRate = 0.2f;

    public DetectionZone attackZone;
    public DetectionZone cliffZone;
    private GameObject obi;
    public GameObject bullet;
    public Transform bulletPos;
    protected float timer;

    protected Rigidbody2D rb;
    protected TouchingDirs touchingDirs;
    protected Animator animator;
    Damagable damageable;
    public enum WalkableDir { Right, Left };

    private WalkableDir _walkDir;
    protected Vector2 walkDirectionAsVector2;

    // walk direction vector
    private Vector2 walkDirVec = Vector2.right;

    public WalkableDir WalkDir
    {
        get { return _walkDir; }
        set
        {

            _walkDir = value;
            if (value == WalkableDir.Left)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                walkDirectionAsVector2 = Vector2.left;
            }
            else if (value == WalkableDir.Right)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                walkDirectionAsVector2 = Vector2.right;
            }
            touchingDirs.wallCheckDirection = walkDirectionAsVector2;




        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        protected set
        {
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    public abstract void Update();

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool canShoot
    {
        get
        {
            return animator.GetBool(AnimationStrings.hasTarget);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }



    public abstract void FixedUpdate();
 

    protected void FlipDir()
    {
        //Debug.LogWarning("wall: " + touchingDirs.colCast() + "  " + animator.GetBool(AnimationStrings.isOnWall));
        if (WalkDir == WalkableDir.Right)
        {
            WalkDir = WalkableDir.Left;
        }
        else if (WalkDir == WalkableDir.Left)
        {
            WalkDir = WalkableDir.Right;
        }
        else
        {
            Debug.LogError("Current walkable dir is not set to legal values");
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        obi = GameObject.FindGameObjectWithTag("Player");
        touchingDirs = GetComponent<TouchingDirs>();
        animator = GetComponent<Animator>();
        touchingDirs.wallDistance = walkSpeed * 1.5f * Time.fixedDeltaTime;
        WalkDir = Random.Range(0, 1) == 1 ? WalkableDir.Left : WalkableDir.Right;
        damageable = GetComponent<Damagable>();
    }

    public abstract void shoot();

    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        //LockVelocity = true;
        LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }




}

