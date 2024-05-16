using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StsBullet : MonoBehaviour
{
    private GameObject obi;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public int attackDamage = 2;
    public Vector2 knockback = Vector2.zero;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        obi = GameObject.FindGameObjectWithTag("Player");

        Vector2 dir = obi.transform.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;

        float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {

            Destroy(gameObject);
        }
    }
  
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            flag = true; 
            Damagable damagable = other.GetComponent<Damagable>();
            PlayerController pc = obi.GetComponent<PlayerController>();
            if (!pc.IsBlocking)
            {
                bool gotHit = damagable.Hit(attackDamage, knockback);
                Destroy(gameObject);
            }
            else
            {

                //rb.velocity = (-obi.transform.right) * force;
                //Vector2 deflectionDirection = other.transform.right;
                Vector2 deflectDirection = CalculateDeflectionDirection(20);

                // Apply deflection velocity
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = deflectDirection * force;
            }
                
        }
        else if (other.gameObject.CompareTag("Enemy"))

        {
            if (flag)
            {
                Damagable damagable = other.GetComponent<Damagable>();

                damagable.Health -= 2;
                Destroy(gameObject);
            }
            
            //bool gotHit = damagable.Hit(attackDamage, knockback);

        
            
        }
        
        
       
        else if (other.gameObject.CompareTag("Tile"))
        {
            Destroy(gameObject);
        }


    }
    private Vector2 CalculateDeflectionDirection(float maxRandomAngle)
    {
        // Get the bullet's velocity vector
        Vector2 bulletVelocity = GetComponent<Rigidbody2D>().velocity.normalized;

        // Calculate the opposite direction of the bullet's velocity for reflection
        Vector2 oppositeDirection = -bulletVelocity;

        // Generate a random angle offset within the specified range
        float randomOffset = Random.Range(-maxRandomAngle, maxRandomAngle);

        // Apply the random offset to the reflection direction
        Vector2 deflectionDirection = Quaternion.Euler(0, 0, randomOffset) * oppositeDirection;

        return deflectionDirection;
    }







}

