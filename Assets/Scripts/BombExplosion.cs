using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private float countdown = 2f;
    public bool player1Bomb;
    public bool player2Bomb;

    private void Start()
    {
        if (PowerUpsManager.Instance.currPowerUp == PowerUpsManager.PowerUps.RC_BOMBS)
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
            MapDestroyer.Instance.Explode(transform.position);
            GameManager.Instance.bombDestroyed();
            GameManager.Instance.bombsSpawnList.Remove(gameObject);
            Destroy(gameObject);
        }

        if (GameManager.Instance.detonateBomb)
        {
            GameManager.Instance.detonateBomb = false;
            countdown = 0f;
        }
    }
}
