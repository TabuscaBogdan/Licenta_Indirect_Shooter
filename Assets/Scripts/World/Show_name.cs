using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_name : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
	}
}
