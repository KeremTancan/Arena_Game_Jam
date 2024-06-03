using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [SerializeField] private AudioSource EffectSource;

    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip teleportSound;   

    public AudioClip JumpSoundEffect { get => jumpSoundEffect; set => jumpSoundEffect = value; }
    public AudioClip CoinSound { get => coinSound; set => coinSound = value; }
    public AudioClip TeleportSound { get => teleportSound; set => teleportSound = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void RunSound()
    //{
      //  RunSource.Play();
   // }
   // public void StopRunSound()
   // {
    //    RunSource.Stop();
   // }
    public void PlayEffectSound(AudioClip clipToPlay)
    {
        //if (EffectSource.isPlaying) return;

        EffectSource.PlayOneShot(clipToPlay);
    }


}
