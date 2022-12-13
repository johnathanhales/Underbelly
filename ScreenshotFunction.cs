using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotFunction : MonoBehaviour
{
    public int count = 0;
    public string stringPrefix = "";
    public Camera camera;
    public AudioSource audio;
    public AudioClip audioFile;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeScreenShot();
        }
    }

    void TakeScreenShot()
    {
        string dateStr = System.DateTime.Now.ToFileTime().ToString();

        ShutterSound();
        name = "C:\\Users\\Exobarf\\Desktop\\ScreenCaptureTS\\HDScreenCaptureTS" + dateStr + ".png";
        ScreenCapture.CaptureScreenshot(name, 4);
        name = "C:\\Users\\Exobarf\\Desktop\\ScreenCaptureTS\\ScreenCaptureTS" + dateStr + ".png";
        ScreenCapture.CaptureScreenshot(name);
        Debug.Log("ScreenCapture Taken, " + name);
        count++;
    }
    void ShutterSound()
    {
        audio.PlayOneShot(audioFile);
        while (!audio.isPlaying)
        {
            audio.volume = 1.0f;
        }
    }
}
