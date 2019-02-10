using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialImageProgression : MonoBehaviour
{
    public Transform loadingBar;
    public Transform textIndicator;  
    [SerializeField]
    private float currentAmount;
    [SerializeField]
    public float countdownTime;
    public float CT;
    public bool startCountdown;

    public void Update()
    {
        if (loadingBar.GetComponent<Image>().fillAmount == 0)
        {
            countdownTime = 10f;
            textIndicator.GetComponent<Text>().text = countdownTime.ToString();
            loadingBar.GetComponent<Image>().fillAmount = 1;
            gameObject.SetActive(false);
        }
    }

    public IEnumerator countdownAnimation()
    {
        yield return new WaitForSeconds(1f);

        if (countdownTime > 0)
        {
            countdownTime--;
        }
        textIndicator.GetComponent<Text>().text = countdownTime.ToString();

        fillLoadingBar();
        StartCoroutine(countdownAnimation());
    }

    public void fillLoadingBar()
    {
        float fill = (float)countdownTime / CT;
        loadingBar.GetComponent<Image>().fillAmount = fill;
    }         
}
