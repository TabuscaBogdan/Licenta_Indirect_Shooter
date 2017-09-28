using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterSound : MonoBehaviour {
    public AudioSource[] sounds;
    // Use this for initialization
    void Start () {
        sounds = GetComponents<AudioSource>();
        sounds[0].Play();
        Destroy(sounds[0].gameObject, sounds[0].clip.length);
    }
}
