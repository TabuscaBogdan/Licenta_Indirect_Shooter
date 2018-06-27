using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetManager : NetworkLobbyManager {
    public Button hostButton;
    public Button joinButton;
    public string playerName;
    public string ip;
    public GameObject Prelobby;
    public GameObject lobby;
    GameObject JoinedPlayers;
    Transform Player;

    private void Start()
    {
        
        hostButton.onClick.AddListener(Hosting);
        joinButton.onClick.AddListener(Joining);
        singleton.networkPort = 8783;
    }


    public void Hosting()
    {
        Debug.Log("Hosting Has Started");
        Debug.Log(playerName+" is now host");
        base.StartHost();
        Prelobby.SetActive(false);
        lobby.SetActive(true);
    }
	public void Joining()
    {
        Debug.Log("Joining has Started");
        //playerName = GameObject.Find("NameField").transform.GetChild(2).GetComponent<Text>().text;
        ip= GameObject.Find("IpField").transform.GetChild(2).GetComponent<Text>().text;
        singleton.networkAddress = ip;
        StartClient();
        lobby.SetActive(true);
        Prelobby.SetActive(false);
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Someone Has connected");
        base.OnClientConnect(conn);
    }
    
    public override void OnLobbyServerPlayersReady()
    {
        Debug.Log("On server Ready Unparent Started");
        JoinedPlayers = GameObject.Find("JoinedPlayers");
        int n = JoinedPlayers.transform.childCount;
        try {
            for (int i = 0; i < n; i++)
            {
                Debug.Log(JoinedPlayers.transform.GetChild(i));
                Player = JoinedPlayers.transform.GetChild(i);
                Player.parent = null;
                Player.GetComponent<LobbyPlayerStatus>().ready = true;
                DontDestroyOnLoad(Player);
                i--;
                n--;
            }
        }
        catch
        {
            Debug.Log("Done Already");
        }
        
        Invoke("base.OnLobbyServerPlayersReady",2);
        base.OnLobbyServerPlayersReady();
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //var cc = lobbyPlayer.GetComponent<ColorControl>();
        string display_name=lobbyPlayer.GetComponentInChildren<Text>().text;
        Debug.Log("display_name:"+display_name);
        Debug.Log(gamePlayer);
        Debug.Log(gamePlayer.transform.GetChild(1));
        Debug.Log(gamePlayer.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1));

        gamePlayer.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = display_name;
        //var player = gamePlayer.GetComponent<Player>();
        //player.myColor = cc.myColor;
        return true;
    }


}
