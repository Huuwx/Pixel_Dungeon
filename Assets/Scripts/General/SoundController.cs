using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;

    public static SoundController Instance { get => instance; }

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip playerWalk;
    public AudioClip playerAttack;
    public AudioClip playerGetDamaged;
    public AudioClip orcWalk;
    public AudioClip orcAttack;
    public AudioClip orcGetDamaged;
    public AudioClip skeletonWalk;
    public AudioClip skeletonAttack;
    public AudioClip skeletonGetDamaged;
    public AudioClip Click;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
}
