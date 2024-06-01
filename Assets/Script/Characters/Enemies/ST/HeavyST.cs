using UnityEngine;

public class HeavyST: StormTrooper
{
    private void Awake()
    {
        walkSpeed = 1.7f;
       
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

    public override void Update()
    {
        HasTarget = attackZone.DetectedColliders.Count > 0;
        if (canShoot)
        {
            timer += Time.deltaTime;
            if (timer > 0.5) //0.5
            {
                timer = 0;
                shoot();
            }

        }
    }

    public void OnCliffDetected()
    {
        if (touchingDirs.IsGrounded) FlipDir();
    }
}