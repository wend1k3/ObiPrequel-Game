using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DetectionZone : MonoBehaviour
{

    public UnityEvent noCollidersRemain;
    public List<Collider2D> DetectedColliders = new List<Collider2D>();
    Collider2D col;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    /*keep track of things enter/exit the detection zone*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
        DetectedColliders.Add(collision);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectedColliders.Remove(collision);
        if (DetectedColliders.Count <=0) noCollidersRemain.Invoke();
    }

    
}
