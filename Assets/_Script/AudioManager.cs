using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public Sound[] sounds;

    private bool isMusicOn;
    private bool isSoundOn;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}

    void Start()
    {
        Play(GameStrings.themeSongSound);
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void MusicController(bool isOn)
    {
        Sound s = Array.Find(sounds, item => item.name == GameStrings.themeSongSound);
        // Music is turned off
        if (isOn == false)
        {
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.enabled = false;
        }
        else s.source.enabled = true;
    }

    public void SoundController(bool isOn)
    {

        foreach (Sound sound in sounds)
        {
            if (isOn == false)
            {
                if (sound.name != GameStrings.themeSongSound) sound.source.enabled = false;
            }
            else if (isOn == true)
            {
                if (sound.name != GameStrings.themeSongSound) sound.source.enabled = true;
            }
        }
    }
}
