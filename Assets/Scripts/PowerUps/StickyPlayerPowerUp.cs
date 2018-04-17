using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlayerPowerUp : BasePowerUpController {

    private GameObject[] balls;

    protected override void ExecPowerUp(Collider2D other)
    {   
        balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].gameObject.SendMessage("StickToPlayer");    
        }        
        
    }
}