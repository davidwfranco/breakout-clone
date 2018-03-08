using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlayerPowerUpNP : BasePowerUpController {

    private GameObject ball;

    protected override void ExecPowerUp(Collider2D other)
    {   
        ball = GameObject.FindGameObjectsWithTag("Ball")[0];
        ball.gameObject.SendMessage("StickToPlayer");
    }
}