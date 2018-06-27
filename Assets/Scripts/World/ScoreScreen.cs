using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {
    [SerializeField]
    GameObject ScorePannel;

    ScoreMaster scm;
    public GameObject ScoreEntry;
    List<PlayerNumbers> currentScores;

    void Start() {
        scm = gameObject.GetComponent<ScoreMaster>();
    }
    void ShowScoreboard()
    {
        currentScores = scm.currentScores;

        foreach (PlayerNumbers score in currentScores)
        {
            GameObject PlayerEntry = Instantiate(ScoreEntry) as GameObject;
            PlayerEntry.transform.GetChild(1).GetComponent<Text>().text=score.name;
            PlayerEntry.transform.GetChild(2).GetComponent<Text>().text = score.lives.ToString();
            PlayerEntry.transform.GetChild(3).GetComponent<Text>().text = score.kills.ToString();
            PlayerEntry.transform.GetChild(4).GetComponent<Text>().text = score.team.ToString();
            PlayerEntry.transform.parent = ScorePannel.transform;

        }
        ScorePannel.SetActive(true);
    }
    void RemoveScoreboard()
    {
        int n = ScorePannel.transform.childCount;
        for(int i=n-1;i>0;i--)
        {
            Transform obj = ScorePannel.transform.GetChild(i);
            //obj.parent = null;
            Destroy(obj.gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ShowScoreboard();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            RemoveScoreboard();
            ScorePannel.SetActive(false);
        }
		
	}
}
