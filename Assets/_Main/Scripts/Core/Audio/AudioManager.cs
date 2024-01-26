using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private const string sfx_parent_name = "sfx";
    public static AudioManager instance { get; private set; }

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup voiceMixer;

    private Transform sfxRoot;

    private void Awake()
    {
        if (instance == null)
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        sfxRoot = new GameObject(sfx_parent_name).transform;
        sfxRoot.SetParent(transform);

    }
    public void PlaySoundEffect(string filepath)
    {

    }
}
