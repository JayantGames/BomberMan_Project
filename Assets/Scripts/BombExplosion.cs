using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
   private float countdown = 2f;

    private void Start()
    {
        if (PowerUpsManager.Instance.getCurrentPowerUp() == PowerUpsManager.PowerUps.RC_BOMBS)
        {
            countdown = 10f;
        }
        else
        {
            countdown = 2f;
        }
    }

    public void DetonateRCBomb()
    {
        countdown = 0f;
    }
    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
           // if (PowerUpsManager.Instance.getCurrentPowerUp() != PowerUpsManager.PowerUps.NONE)
           // {
           //     PowerUpsManager.Instance.currPowerUp = PowerUpsManager.PowerUps.NONE;
          //  }

            MapDestroyer.Instance.Explode(transform.position);
            GameManager.Instance.bombDestroyed();
            GameManager.Instance.bombsSpawnList.Remove(gameObject);
            Destroy(gameObject);
        }   
        
        if(GameManager.Instance.detonateBomb)
        {
            GameManager.Instance.detonateBomb = false;
            countdown = 0f;
        }
    }
}
