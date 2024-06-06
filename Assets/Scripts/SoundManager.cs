using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [SerializeField] private AudioSource EffectSource;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip teleportSound;  
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    

    public AudioClip HitSound { get => hitSound; set => hitSound = value; }
    public AudioClip CoinSound { get => coinSound; set => coinSound = value; }
    public AudioClip TeleportSound { get => teleportSound; set => teleportSound = value; }
    public AudioClip JumpSound { get => jumpSound; set => jumpSound = value; }
    public AudioClip WinSound { get => winSound; set => winSound = value; }
    public AudioClip LoseSound { get => loseSound; set => loseSound = value; }

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

    
    public void PlayEffectSound(AudioClip clipToPlay)
    {
        //if (EffectSource.isPlaying) return;

        EffectSource.PlayOneShot(clipToPlay);
    }


}
