using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class TouchingDirs : MonoBehaviour
{
    public ContactFilter2D touchingFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.05f;
    public float ceilingDistance = 0.05f;
    public Vector2 wallCheckDirection = Vector2.zero;
    

    private Animator animator;
    private CapsuleCollider2D col;

    [SerializeField]
    private bool _isOnWall;
    private int cnt=0;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

   

    [SerializeField]
    private bool _isOnCeiling;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeil, value);
        }
    }

    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded
    {
        get { return _isGrounded; }
        set
        {
            // If the value is different than what it was before, interrupt ground or air states
            if (_isGrounded = true && value != true)
                animator.SetTrigger(AnimationStrings.ground_interrupt);
            else if (_isGrounded = false && value != false)
                animator.SetTrigger(AnimationStrings.air_interrupt);

            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGround, value);
        }
    }

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[10];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();

        // Check that collider offset is 0,0
        if (col.offset.x != 0)
        {
            Debug.LogWarning("Recommended x offset of 0 for TouchingDirections collider on game object " + gameObject.name + ". Adjust the transform instead");
        }
      
    }
    public int colCast()
    {
        return col.Cast(wallCheckDirection, touchingFilter, wallHits, wallDistance);
    }
    public void FixedUpdate()
    {
        int groundHitCount = col.Cast(Vector2.down, touchingFilter, groundHits, groundDistance);
        IsGrounded = groundHitCount > 0;

   
        int wallHitCount = col.Cast(wallCheckDirection, touchingFilter, wallHits, wallDistance);
        
        IsOnWall = wallHitCount > 0 && !IsBulletHit(wallHits, wallHitCount);
        if (IsOnWall)

        {

            Debug.LogWarning("cnt: " + cnt);

            for (int i = 0; i < wallHitCount; i++)
            {
                RaycastHit2D hit = wallHits[i];

                // Access the collider that caused the hit
                Collider2D hitCollider = hit.collider;

                // Now you can use 'hitCollider' to inspect properties of the collider
                Debug.LogWarning("Hit collider: " + hitCollider.gameObject.name);
            }
            cnt++;
        }
        
      
      

        int ceilingHitCount = col.Cast(Vector2.up, touchingFilter, ceilingHits, ceilingDistance);
        IsOnCeiling = ceilingHitCount > 0;
    
        
        // Wall collisions sometimes show up as ceiling collisions if BoxCollider (I guess corners could show up as either or both)
        IsOnCeiling = col.Cast(Vector2.up, touchingFilter, ceilingHits, ceilingDistance) > 0;
    }

    private bool IsBulletHit(RaycastHit2D[] hits, int hitCount)
    {
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }


}

