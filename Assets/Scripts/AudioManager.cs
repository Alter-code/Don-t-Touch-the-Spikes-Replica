using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public static bool canPlay = true;


    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        foreach (Sound _sound in sounds)
        {
            _sound.source = gameObject.AddComponent<AudioSource>();
            _sound.source.clip = _sound.clip;
            _sound.source.loop = _sound.loop;

            _sound.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string sound)
    {   if(canPlay == false)
        {
            return;
        }
        Sound _sound = Array.Find(sounds, item => item.name == sound);
        if(_sound == null)
        {
            Debug.LogWarning($"Sound: {name} not found");
            return;
        }

        _sound.source.volume = _sound.volume * (1f + UnityEngine.Random.Range (-_sound.volumeVariance / 2f, _sound.volumeVariance / 2f));
        _sound.source.pitch = _sound.pitch * (1f + UnityEngine.Random.Range (-_sound.pitchVariance / 2f, _sound.pitchVariance / 2f));

        _sound.source.Play();
    }
}
