using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonClicked : MonoBehaviour
{
    public void PlaySoundClickButton (AudioClip sound)
    {
        AudioManager.instance.PlaySFX(sound);
    }
}
