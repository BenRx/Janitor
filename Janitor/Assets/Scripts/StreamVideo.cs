using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage bg;
    private VideoPlayer videoPlayer;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo() {
        WaitForSeconds wf = new WaitForSeconds(1);
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared) {
            yield return wf;
            break;
        }
        bg.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }
}
