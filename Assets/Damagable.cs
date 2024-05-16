using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    HealthSystem healthSystem;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth  = value;    
        }
    }

    [SerializeField]
    private int _health = 100;


    public int Health
    {
        get
        {
            return _health;
        }
        set { 
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    private float invincibilityTimer = 0.25f; //player cannot be hit again within a short period of time

     

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive,value);
            Debug.Log("IsAlive set"+value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        healthSystem = GetComponent<HealthSystem>();

    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible=true;
            //notify other subscribed componnents that the damageable was hit
            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit?.Invoke(damage, knockback);
            healthSystem.updateHealth(Health);
            return true;
        }
        return false;
        
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit>invincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    

    }

}
