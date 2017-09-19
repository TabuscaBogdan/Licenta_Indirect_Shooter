using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour {

    public AudioSource[] sounds;
    

    private Rigidbody RdBody;
    // Use this for initialization
    void Start () {
        RdBody = GetComponent<Rigidbody>();
        sounds = gameObject.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
   
    }

    // for closisions
    private void OnCollisionEnter(Collision collision)
    {
        /*problema
         * obiectul dispare si 
         * sunetul nu mai are timp sa ruleze
         * 
         * */
        //sounds[0].transform.parent = null;
        //sounds[0].Play();
        //Destroy(sounds[0].gameObject, sounds[0].clip.length);
        Destroy(gameObject); //remove the projectile


    }
}
