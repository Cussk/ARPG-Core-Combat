using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Sound
{
    public class SoundRandomizer : MonoBehaviour
    {
        [SerializeField] float lowerPitchRange = 0.8f;
        [SerializeField] float upperPitchRange = 1.2f;
        [SerializeField] AudioClip[] soundSet = null;
        AudioSource audioSource = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomizedSound()
        {
            // Randomize sound clip
            audioSource.clip = soundSet[Random.Range(0, soundSet.Length)];
            // Randomize pitch
            audioSource.pitch = Random.Range(lowerPitchRange, upperPitchRange);

            audioSource.Play();
        }
    }
}
