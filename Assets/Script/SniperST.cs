using UnityEngine;

public class SniperST : StormTrooper
{

    [SerializeField]
    public float stayTime = 5f;

    [SerializeField]
    public float aimTime = 2f;
    private void Awake()
    {
        walkSpeed = 1.5f;
   
    }
    public override void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}