using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [SerializeField]
    public int attackDamage = 1;
    public Vector2 knockback = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Damagable damagable = other.GetComponent<Damagable>();
        Animator ani = other.GetComponent<Animator>();
        ani.SetBool(AnimationStrings.isHit, true);
        
        bool gotHit = damagable.Hit(attackDamage, knockback);
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Animator ani = other.GetComponent<Animator>();
        ani.SetBool(AnimationStrings.isHit, false);
    }
}
