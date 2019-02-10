using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpListener : MonoBehaviour
{
    public PowerUpsManager.PowerUps powerUpType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            PowerUpsManager.Instance.activatePowerUp(powerUpType, CharacterController.PLAYER.PLAYER_1);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            PowerUpsManager.Instance.activatePowerUp(powerUpType,CharacterController.PLAYER.PLAYER_2);
            Destroy(gameObject);
        }
    }
}
