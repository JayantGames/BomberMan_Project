using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpListener : MonoBehaviour
{
    public PowerUpsManager.PowerUps powerUpType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            PowerUpsManager.Instance.activatePowerUp(powerUpType);
            Destroy(gameObject);
        }
    }
}
