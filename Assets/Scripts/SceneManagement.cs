using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void SceneLoader(int sceneIndex)
    {
        SceneManager.LoadScene("Level" + sceneIndex.ToString());
    }
}
