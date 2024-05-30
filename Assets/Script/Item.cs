using UnityEngine;

public abstract class Item: MonoBehaviour
{
    protected GameObject obi;
    protected Rigidbody2D rb;

    public abstract void Start();
    public abstract void Update();
    public abstract void OnTriggerEnter2D(Collider2D other);
}
