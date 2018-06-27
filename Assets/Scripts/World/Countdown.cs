using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    public int minutes = 10;
    public int seconds = 0;
    // Use this for initialization
    private float TimeLeft;
    private Text timer;

    void Start () {
        TimeLeft = minutes * 60;
        timer = GameObject.Find("PlayerUI").transform.GetChild(0).GetComponent<Text>();
	}

	void Update () {
        TimeLeft = TimeLeft - Time.deltaTime;
        minutes= Mathf.FloorToInt(TimeLeft / 60f);
        seconds = Mathf.FloorToInt(TimeLeft % 60f);

        if(TimeLeft>0)
        {
            timer.text = minutes + ":" + seconds;
        }
        else
        {
            timer.text = "Time's Up!";
        }

    }
}
