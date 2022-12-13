using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsLibraryScript : MonoBehaviour
{
    public AudioClip money, explosion, tick;
    public AudioSource audio;
   
    public void PlaySound(AudioClip aClip)
    {
        audio.PlayOneShot(aClip);
    }
    public void PlayMoney()
    {
        PlaySound(money);
    }
    public void PlayExplosion()
    {
        PlaySound(explosion);
    }

    public void PlayTick()
    {
        PlaySound(tick);
    }
}
