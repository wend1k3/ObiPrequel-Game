﻿using UnityEngine;

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
}