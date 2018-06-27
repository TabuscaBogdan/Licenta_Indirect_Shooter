using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoHold : MonoBehaviour {

	public string PlayerName;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setPlayerName()
    {
        PlayerName= GameObject.Find("NameField").transform.GetChild(2).GetComponent<Text>().text;
    }

}
