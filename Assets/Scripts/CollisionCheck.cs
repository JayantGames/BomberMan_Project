using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player1":
                {
                    GameManager.Instance.player1Alive = false;
                    Instantiate(GameManager.Instance.deathPrefab, collision.transform.position, Quaternion.identity);
                    Destroy(collision.gameObject);
                    break;
                }
            case "Player2":
                {
                    GameManager.Instance.player2Alive = false;
                    Instantiate(GameManager.Instance.deathPrefab, collision.transform.position, Quaternion.identity);
                    Destroy(collision.gameObject);
                    break;
                }
            case "Bomb":
                {
                    MapDestroyer.Instance.Explode(collision.transform.position);
                    Destroy(collision.gameObject);
                    break;
                }
            case "PowerUp":
                {
                    Destroy(collision.gameObject);
                    break;
                }
        }
    }
}

