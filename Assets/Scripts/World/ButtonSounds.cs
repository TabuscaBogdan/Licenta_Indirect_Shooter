using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSounds : MonoBehaviour {

    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;

        button.onClick.AddListener(() => PlayClickSound());
	}
    void PlayClickSound()
    {
        source.clip = clickSound;
        source.PlayOneShot(clickSound);
    }
    public void PlayHoverSound()
    {
        source.clip = hoverSound;
        source.PlayOneShot(hoverSound);
    }

}
