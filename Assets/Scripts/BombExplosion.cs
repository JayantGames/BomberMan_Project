using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
   private float countdown = 2f;

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            MapDestroyer.Instance.Explode(transform.position);
            Destroy(gameObject); 
        }                                                 
    }
}
