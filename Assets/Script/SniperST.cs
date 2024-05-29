using UnityEngine;

public class SniperST : StormTrooper
{

    [SerializeField]
    public float stayTime = 10f;

    [SerializeField]
    public float aimTime = 2f;

    [SerializeField]
    private float aimTimer;

    [SerializeField]
    private float stayTimer;

    public LaserPtr _laserPtr;
   
    private void Awake()
    {
        walkSpeed = 1.5f;
        
   
    }
    public override void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    public override void FixedUpdate()
    {


        touchingDirs.wallCheckDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;



        
        if (touchingDirs.IsGrounded && touchingDirs.IsOnWall)
        {

            FlipDir();
        }

        if (CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionAsVector2.x, rb.velocity.y);
        }
        else
        {
            // Default slow towards 0 on x velocity
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkSpeedLerpStopRate), rb.velocity.y);
        }
    }

    private void aim()
    {
        _laserPtr.render();
        if (aimTimer >= aimTime) shoot();
    }

    public override void Update()
    {
        if (_laserPtr == null) Debug.LogError("err");
        if (cliffZone.DetectedColliders.Count == 0)
        {
            if (stayTimer>=stayTime)
            {
                //_laserPtr.setEnable();
                animator.SetBool(AnimationStrings.isAim, false);
                FlipDir();
                stayTimer = 0;

            }
            else 
            {
                animator.SetBool(AnimationStrings.isAim, true);
                aim();
                aimTimer += Time.deltaTime;
                stayTimer += Time.deltaTime;

            }
            Debug.LogError("stay time: "+stayTimer);
        }

    }
}