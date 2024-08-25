using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : Singleton<AudioManager>
    {
        

        [Space(20)]
        [Header("Audio Sources")]
        [SerializeField] private AudioSource backgroundMusicAudioSource;

        [SerializeField] private AudioSource fireAudioSource;
        
        [SerializeField] private AudioSource sfxAudioSource;
        
        [SerializeField] private AudioSource extraShipAudioSource;

        [SerializeField] private AudioSource explosionAudioSource;

        [SerializeField] private AudioSource saucerAudioSource;
        
        [Space(20)]
        [Header("Spaceship Audio Clips")]
        [SerializeField] private AudioClip fireAudioClip;
        [SerializeField] private AudioClip thrustAudioClip;
        [SerializeField] private AudioClip explosionAudioClip;

        [Space(5)]
        [SerializeField] private AudioClip extraShipAudioClip;
        [SerializeField] private int noExtraShipBeeps = 7;
        [SerializeField] private float extraShipInterval;
        private Coroutine extraShipCoroutine;
        
        [Space(5)]
        [Header("Asteroid Explosions")]
        [SerializeField] private AudioClip smallAsteroidExplosion;
        [SerializeField] private AudioClip mediumAsteroidExplosion;
        [SerializeField] private AudioClip largeAsteroidExplosion;

        [Space(5)]
        [Header("Saucers")]
        [SerializeField] private AudioClip smallSaucer;
        [SerializeField] private AudioClip bigSaucer;
        
        [Space(5)]
        [Header("Background Music")]
        [SerializeField] private AudioClip quietBeatAudioClip;
        [SerializeField] private float quietBeatInterval = 1.0f;
        [SerializeField] private AudioClip dramaticBeatAudioClip;
        [SerializeField] private float dramaticBeatInterval = 0.5f;

        [Space(20)]
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer mixer;
        
        private AudioClip beatAudioClip;
        private float beatInterval = 0f;
        private Coroutine backgroundMusicCoroutine;
        private bool dramaticBackgroundMusic = false;
        
        public void PlaySpaceshipFire()
        {
            fireAudioSource.PlayOneShot(fireAudioClip);
        }

        public void PlaySpaceshipThrust()
        {
            sfxAudioSource.PlayOneShot(thrustAudioClip);
        }

        public void PlayExtraSpaceship()
        {
            if (extraShipCoroutine!=null)
                StopCoroutine(extraShipCoroutine);
            extraShipCoroutine = StartCoroutine("PlayExtraShipCoroutine");
        }

        public void PlaySpaceshipExplosion()
        {
            explosionAudioSource.PlayOneShot(explosionAudioClip);
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
            beatAudioClip = quietBeatAudioClip;
            beatInterval = quietBeatInterval;
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
                    beatAudioClip = dramaticBeatAudioClip;
                    beatInterval = dramaticBeatInterval;
                    
                } 
                
            }
        }

        private IEnumerator PlayExtraShipCoroutine()
        {
            for (int i = 0; i < noExtraShipBeeps; i++)
            {
                extraShipAudioSource.PlayOneShot(extraShipAudioClip);
                yield return new WaitForSeconds(extraShipInterval);
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
                    explosionAudioSource.PlayOneShot(largeAsteroidExplosion);
                    break;
                case AsteroidSize.Medium:
                    explosionAudioSource.PlayOneShot(mediumAsteroidExplosion);
                    break;
                case AsteroidSize.Small:
                    explosionAudioSource.PlayOneShot(smallAsteroidExplosion);
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
