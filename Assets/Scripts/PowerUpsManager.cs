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

    public void activatePowerUp(PowerUps powerUp, CharacterController.PLAYER player)
    {
        powerUpActive = true;
        currPowerUp = powerUp;
        powerUpCountdown = 10f;

        if (player == CharacterController.PLAYER.PLAYER_1)
        {
            GameManager.Instance.player1Powerup = true;
            GameManager.Instance.player2Powerup = false;

            GameObject player1Radial = GameManager.Instance.inGameSettingsPanel.GetComponent<InGameUISettings>().player1RPB;
            GameObject player2Radial = GameManager.Instance.inGameSettingsPanel.GetComponent<InGameUISettings>().player2RPB;
            player1Radial.SetActive(true);
            player2Radial.SetActive(false);
            player1Radial.GetComponent<RadialImageProgression>().CT = player1Radial.GetComponent<RadialImageProgression>().countdownTime;
            StartCoroutine(player1Radial.GetComponent<RadialImageProgression>().countdownAnimation());
        }
        else if (player == CharacterController.PLAYER.PLAYER_2)
        {
            GameManager.Instance.player1Powerup = false;
            GameManager.Instance.player2Powerup = true;

            GameObject player1Radial = GameManager.Instance.inGameSettingsPanel.GetComponent<InGameUISettings>().player1RPB;
            GameObject player2Radial = GameManager.Instance.inGameSettingsPanel.GetComponent<InGameUISettings>().player2RPB;
            player1Radial.SetActive(false);
            player2Radial.SetActive(true);
            player2Radial.GetComponent<RadialImageProgression>().CT = player2Radial.GetComponent<RadialImageProgression>().countdownTime;
            StartCoroutine(player2Radial.GetComponent<RadialImageProgression>().countdownAnimation());
        }
    }

    private void Update()
    {
        if (powerUpActive)
        {
            powerUpCountdown -= Time.deltaTime;
        }

        if (powerUpCountdown <= 0)
        {
            powerUpActive = false;
        }

        if (!powerUpActive)
        {
            currPowerUp = PowerUps.NONE;
            GameManager.Instance.player1Powerup = false;
            GameManager.Instance.player2Powerup = false;
        }
    }
}
