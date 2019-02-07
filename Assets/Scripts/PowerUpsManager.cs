using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public static PowerUpsManager Instance;

    public float powerUpCountdown;
    public PowerUps currPowerUp;
    public bool powerUpActive;

    private void Awake()
    {
        Instance = this;
    }

    public enum PowerUps
    {
        NONE,
        LONG_BLAST,
        MORE_BOMBS,
        FAST_PACE,
        RC_BOMBS
    }

    private void Start()
    {
        currPowerUp = PowerUps.NONE;    
    }

    public PowerUps getCurrentPowerUp()
    {
        return currPowerUp;
    }

    public void activatePowerUp(PowerUps powerUp)
    {
        powerUpActive = true;
        currPowerUp = powerUp;
    }

    private void Update()
    {
        if (powerUpActive)
        {
            powerUpCountdown -= Time.deltaTime;
        }

        if(powerUpCountdown <=0)
        {
            powerUpActive = false;
        }
    }
}
