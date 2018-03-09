using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBarPowerUp : BasePowerUpController {
    protected override void ExecPowerUp(Collider2D other)
    {
        float newXSize = other.transform.localScale.x + (other.transform.localScale.x * 0.5f);
        other.transform.localScale = new Vector2(newXSize, other.transform.localScale.y);
    }
}