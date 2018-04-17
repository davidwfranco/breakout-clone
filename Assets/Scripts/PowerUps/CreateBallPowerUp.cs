using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBallPowerUp : BasePowerUpController {
    protected override void ExecPowerUp(Collider2D other)
    {
        GameController.instance.CreateNewBall();
    }
}