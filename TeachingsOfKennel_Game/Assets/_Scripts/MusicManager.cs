using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicClips;

    private void Update(){
        if (!musicSource.isPlaying){
            musicSource.clip = musicClips[Random.Range(0,musicClips.Length-1)];
            musicSource.Play();
        }
    }
}
