using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PrefabTile player1;

    private void Awake()
    {
        Instance = this;
    }
  
}
