using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectionPanel;

    private void Start()
    {
        ActivateMainMenu();
    }

    public void ActivateMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
    }

    public void ActivateLevelSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    private void Update()
    {
        if (mainMenuPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ActivateLevelSelection();
            }
        }
    }
}
