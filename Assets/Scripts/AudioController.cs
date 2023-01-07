using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource music;
    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void MuteMusicToggle(bool muted)
    {
        if (muted)
        {
            music.volume = 0;
        }
        else
        {
            music.volume = 1;
        }
    }
}
