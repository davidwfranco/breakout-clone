using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedDownPowerUp : BasePowerUpController {

    private GameObject ball;
    public int ballSpeedDownPerc;

    protected override void ExecPowerUp(Collider2D other)
    {   
        ball = GameObject.FindGameObjectsWithTag("Ball")[0];
        ball.GetComponent<BallController>().ballSpeed *= (float)(ballSpeedDownPerc/100);
    }
}