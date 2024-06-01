using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stim : MonoBehaviour
{

    [SerializeField]
    private int healthAmount = 20;

    public Vector3 spinRotateSpeed = new Vector3(0, 180, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();  
        if (damagable)
        {
            damagable.Heal(healthAmount);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotateSpeed * Time.deltaTime;
    }
}
