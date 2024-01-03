using System;
using UnityEngine;

public sealed class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] SoundClips;
    
    [SerializeField] private AudioSource SoundSource;
    
    public static AudioController Instance = null;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(string clipName, bool loop = false)
    {
        if (SoundSource.clip.name == clipName && SoundSource.isPlaying)
        {
            SoundSource.Stop();
        }
            
        SoundSource.clip = Array.Find(SoundClips, x => x.name == clipName);
        SoundSource.loop = loop;
        SoundSource.Play();
    }
}