using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private int stPoint;

    [SerializeField]
    private Transform[] points;

    private int i;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[stPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length) i = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position,speed*Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y - 0.08f) collision.transform.SetParent(transform);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }

    }

}
