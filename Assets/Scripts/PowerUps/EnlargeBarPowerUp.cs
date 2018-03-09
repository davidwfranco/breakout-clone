using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeBarPowerUp : BasePowerUpController {
    protected override void ExecPowerUp(Collider2D other)
    {
        other.gameObject.SendMessage("Enlarge",0.2f);
    }
}