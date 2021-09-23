using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string musicPref = "musicPref";
    private static readonly string sfxPref = "sfxPref";
    private float musicFloat, sfxFloat;
    public AudioSource[] musicAudio;
    public AudioSource[] sfxAudio;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(musicPref);
        sfxFloat = PlayerPrefs.GetFloat(sfxPref);

        for (int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicFloat;
        }
        for (int j = 0; j < sfxAudio.Length; j++)
        {
            sfxAudio[j].volume = sfxFloat;
        }
    }
}
