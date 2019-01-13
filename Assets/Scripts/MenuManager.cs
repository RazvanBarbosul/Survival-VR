using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {


    public Image playImg;
    public Image upgradesImg;
    public Image quitImg;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void PlayFadeEffect()
    {
        StartCoroutine(FadeImage(true, playImg));
    }

    public void PlayCancelFadeEffect()
    {
        StartCoroutine(FadeImage(false,playImg));
    }

    public void UpgradesFadeEffect()
    {
        StartCoroutine(FadeImage(true, upgradesImg));
    }

    public void UpgradesCancelFadeEffect()
    {
        StartCoroutine(FadeImage(false, upgradesImg));
    }

    public void QuitFadeEffect()
    {
        StartCoroutine(FadeImage(true, quitImg));
    }

    public void QuitCancelFadeEffect()
    {
        StartCoroutine(FadeImage(false, quitImg));
    }

    IEnumerator FadeImage(bool fadeAway, Image img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0.2f; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0.2f; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1);
        Debug.Log("Pressed");
    }
}
