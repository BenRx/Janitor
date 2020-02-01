using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityManager : MonoBehaviour
{
    private Light lightToFade;
    public float BPM;
    public float fadeOutCoef;
    public float minLuminosity;
    public float maxLuminosity;

    private float deltaDuration;


    void Start()
    {
        lightToFade = GetComponent<Light>();
        float repeatValue = (60 / BPM); // Rythm var : /4 - croche | *4 - 4 Temps | O - Blanche

        StartCoroutine(fadeInAndOutRepeat(lightToFade, repeatValue, repeatValue));
    }

    IEnumerator fadeInAndOutRepeat(Light lightToFade, float duration, float waitTime)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true)
        {
            yield return fadeInAndOut(lightToFade, false, duration);
            yield return waitForXSec;
            yield return fadeInAndOut(lightToFade, true, duration);
        }
    }
    IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration)
    {
        float counter = 0f;
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        } 
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        while (counter < duration)
        {
            counter += fadeIn ? Time.deltaTime : Time.deltaTime / fadeOutCoef;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }
}
