using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/SoundBank")]
public class AsteroidSoundBank : ScriptableObject
{
    [Space(20)]
    [Header("Spaceship Audio Clips")]
    [SerializeField] private AudioClip fireAudioClip;
    [SerializeField] private AudioClip thrustAudioClip;
    [SerializeField] private AudioClip explosionAudioClip;

    [Space(5)]
    [SerializeField] private AudioClip extraShipAudioClip;
    [SerializeField] private int noExtraShipBeeps = 7;
    [SerializeField] private float extraShipInterval;
        
    [Space(5)]
    [Header("Asteroid Explosions")]
    [SerializeField] private AudioClip smallAsteroidExplosion;
    [SerializeField] private AudioClip mediumAsteroidExplosion;
    [SerializeField] private AudioClip largeAsteroidExplosion;
        
    [Space(5)]
    [Header("Background Music")]
    [SerializeField] private AudioClip quietBeatAudioClip;
    [SerializeField] private float quietBeatInterval = 1.0f;
    [SerializeField] private AudioClip dramaticBeatAudioClip;
    [SerializeField] private float dramaticBeatInterval = 0.5f;

    // properties to access values
    public AudioClip FireAudioClip => fireAudioClip;
    public AudioClip ThrustAudioClip => thrustAudioClip;
    public AudioClip ExplosionAudioClip => explosionAudioClip;

    public AudioClip ExtraShipAudioClip => extraShipAudioClip;
    public int NoExtraShipBeeps => noExtraShipBeeps;
    public float ExtraShipInterval => extraShipInterval;
        
    public AudioClip SmallAsteroidExplosion => smallAsteroidExplosion;
    public AudioClip MediumAsteroidExplosion => mediumAsteroidExplosion;
    public AudioClip LargeAsteroidExplosion => largeAsteroidExplosion;
        
    public AudioClip QuietBeatAudioClip => quietBeatAudioClip;
    public float QuietBeatInterval => quietBeatInterval;
    public AudioClip DramaticBeatAudioClip => dramaticBeatAudioClip;
    public float DramaticBeatInterval => dramaticBeatInterval;
}
