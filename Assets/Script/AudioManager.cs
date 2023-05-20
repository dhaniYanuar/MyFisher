using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFisher
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                return instance;
            }
        }
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }
        #endregion

        public AudioSource AmbienceAudioSource;
        public AudioSource BGMAudioSource;
        public AudioSource SFXAudioSource;
        [SerializeField]
        public List<SFXElement> SFXes;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayAmbience(AudioClip clip)
        {
            
        }

        public void PlayBGM(AudioClip clip)
        {

        }

        public void PlaySFX(SFXENUM Id_sfx)
        {
            if (!SFXes.Exists(x=> x.Id_Name == Id_sfx))
            {
                return;
            }
            SFXAudioSource.PlayOneShot(SFXes.Find(x=> x.Id_Name == Id_sfx).clip);
        }


        public enum SFXENUM {HIT, LOSE, SWIPE}
        [Serializable]
        public struct SFXElement
        {
            public SFXENUM Id_Name;
            public AudioClip clip;
        }
    }
}