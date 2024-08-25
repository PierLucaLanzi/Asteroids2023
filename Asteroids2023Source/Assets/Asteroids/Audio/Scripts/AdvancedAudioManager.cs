using System;
using System.Collections;
using UnityEngine;
// using Asteroids.Scripts.Managers.ScriptableObjects;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

    public class AdvancedAudioManager : Singleton<AudioManager>
    {

        [Space(20)]
        [Header("Audio Sources")]
        [SerializeField] private AudioSource backgroundMusicAudioSource;

        [SerializeField] private AudioSource fireAudioSource;
        
        [SerializeField] private AudioSource sfxAudioSource;
        
        [SerializeField] private AudioSource extraShipAudioSource;

        [SerializeField] private AudioSource explosionAudioSource;

        [Space(20)]
        [Header("Sound Bank")]
        [SerializeField] private AsteroidSoundBank soundBank;

        [Space(20)]
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer mixer;

        // coroutines
        private Coroutine backgroundMusicCoroutine;
        private Coroutine extraShipCoroutine;
        
        // background music 
        private AudioClip beatAudioClip;
        private float beatInterval = 0f;
        private bool dramaticBackgroundMusic = false;
        
        public void PlaySpaceshipFire()
        {
            fireAudioSource.PlayOneShot(soundBank.FireAudioClip);
        }

        public void PlaySpaceshipThrust()
        {
            sfxAudioSource.PlayOneShot(soundBank.ThrustAudioClip);
        }

        public void PlayExtraSpaceship()
        {
            if (extraShipCoroutine!=null)
                StopCoroutine(extraShipCoroutine);
            extraShipCoroutine = StartCoroutine("PlayExtraShipCoroutine");
        }

        public void PlaySpaceshipExplosion()
        {
            explosionAudioSource.PlayOneShot(soundBank.ExplosionAudioClip);
        }
        
        public void StartBackgroundMusic()
        {
            if (backgroundMusicCoroutine != null)
                return;
            else
            {
                backgroundMusicCoroutine = StartCoroutine("PlayBackgroundMusicRoutine");    
            }
        }

        public void StopBackgroundMusic()
        {
            if (backgroundMusicCoroutine != null)
                StopCoroutine(backgroundMusicCoroutine);
        }
        
        public void ShiftToDramaticBackgroundMusic()
        {
            if (backgroundMusicCoroutine != null)
                dramaticBackgroundMusic = true;
        }

        public void ResetBackgroundMusic()
        {
            beatAudioClip = soundBank.QuietBeatAudioClip;
            beatInterval = soundBank.QuietBeatInterval;
            dramaticBackgroundMusic = false;
        }
        
        private IEnumerator PlayBackgroundMusicRoutine()
        {
            ResetBackgroundMusic();
            
            while (true)
            {
                backgroundMusicAudioSource.PlayOneShot(beatAudioClip);
                yield return new WaitForSeconds(beatInterval);

                if (dramaticBackgroundMusic)
                {
                    beatAudioClip = soundBank.DramaticBeatAudioClip;
                    beatInterval = soundBank.DramaticBeatInterval;
                    
                } 
                
            }
        }

        private IEnumerator PlayExtraShipCoroutine()
        {
            for (int i = 0; i < soundBank.NoExtraShipBeeps; i++)
            {
                extraShipAudioSource.PlayOneShot(soundBank.ExtraShipAudioClip);
                yield return new WaitForSeconds(soundBank.ExtraShipInterval);
            }

            yield return null;
        }

        public void PlayAsteroidExplosion(AsteroidSize size)
        {
            float pitch = explosionAudioSource.pitch;

            explosionAudioSource.pitch = Random.Range(pitch - 0.05f, pitch + 0.05f);
            
            switch (size)
            {
                case AsteroidSize.Large:
                    explosionAudioSource.PlayOneShot(soundBank.LargeAsteroidExplosion);
                    break;
                case AsteroidSize.Medium:
                    explosionAudioSource.PlayOneShot(soundBank.MediumAsteroidExplosion);
                    break;
                case AsteroidSize.Small:
                    explosionAudioSource.PlayOneShot(soundBank.SmallAsteroidExplosion);
                    break;
            }

            explosionAudioSource.pitch = pitch;
        }
        
        // these functions are needed if the game has a menu that can modify the master volume
        public void SetMixerMasterVolume(float volume)
        {
                float mixerVolume = AudioManager.SliderToDB(volume);
                mixer.SetFloat ("MasterVolume", mixerVolume);
        }
    
        public static float SliderToDB(float volume, float maxDB=-10, float minDB=-80)
        {
            float dbRange = maxDB - minDB;
            return minDB + volume * dbRange;
        }
    }
// }
