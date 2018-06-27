using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreMaster : NetworkBehaviour {

    
    public List<PlayerNumbers> currentScores;
    public GameObject[] players;
    PlayerNumbers playerScore;
    private void Awake()
    {
        currentScores = new List<PlayerNumbers>();
        PlayerNumbers ai = new PlayerNumbers("AI", 0, 0, 0);
        currentScores.Add(ai);
    }

    void Start () {
        
        /*
        //------get initial players
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            Status player_stats = player.GetComponent<Status>();
            playerScore = new PlayerNumbers(player_stats.name, player_stats.lives, player_stats.team, 0);
            currentScores.Add(playerScore);
        }
        */

	}
    public void ShowDebugScoreboard()
    {
        Debug.Log("Current Scoreboard");
        foreach (PlayerNumbers score in currentScores)
        {
            Debug.Log(score.name + "  Lives:" + score.lives + " Team:" + score.team + " Kills:" + score.kills);
        }
    }
    public void AddPlayer(Status player_stats)
    {
        Debug.Log("--->" + player_stats);
        Debug.Log(player_stats.name + "<");
        Debug.Log(player_stats.lives + "<");
        playerScore = new PlayerNumbers(player_stats.name, player_stats.lives, player_stats.team, 0);
        //lookout for similar names
        Debug.Log(playerScore);
        currentScores.Add(playerScore);


        ShowDebugScoreboard();
    }
    //called when your player is killed
    public void PlayerKilled(string name)
    {
        int index = currentScores.FindIndex(ply => ply.name == name);
        currentScores[index].lives--;
        ShowDebugScoreboard();
    }
    //called by OnHit when you kill others
    public void PlayerTakedown(string killer,string victim,bool set=false)
    {
        Debug.Log("Intra in takedown");
        Debug.Log(killer + " vict " + victim);
        ShowDebugScoreboard();
        int index = currentScores.FindIndex(ply => ply.name == killer);
        Debug.Log(index);
        if (victim == "AI")
        { currentScores[index].kills++; }
        else
        { currentScores[index].kills += 10; }
        /*if(isServer)
        {
            Debug.Log("Update Your Scores!");
            RpcTakedown(killer, victim);
        }*/
        //CmdUpdateYourScores(killer, victim);
        ShowDebugScoreboard();
    }
    [Command]
    public void CmdUpdateYourScores(string killer,string victim)
    {
        RpcTakedown(killer, victim);
    }
    [ClientRpc]
    public void RpcTakedown(string killer,string victim)
    {
        if (isLocalPlayer)
        {
            Debug.Log("Server Told me To Update Scores:" + killer + " " + victim);
            PlayerTakedown(killer, victim, true);
        }
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
