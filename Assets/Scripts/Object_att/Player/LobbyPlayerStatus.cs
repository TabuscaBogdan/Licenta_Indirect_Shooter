using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayerStatus : NetworkLobbyPlayer {
    [SyncVar(hook ="NameChange")]
    public string Player_name="";
    [SyncVar]
    public int team;
    public GameObject JoinedPlayers;
    public GameObject Prelobby;
    public GameObject Lobby;
    int no_clients=0;
    [SyncVar(hook = "AllReady")]
    public bool ready = false;


    public void refreshNames()
    {
        
        Transform Player;
        int n = JoinedPlayers.transform.childCount;
        no_clients = n;
        Debug.Log("Number of current Clients:" + n);
        for (int i = 0; i < n; i++)
        {
            Debug.Log(JoinedPlayers.transform.GetChild(i));
            Player = JoinedPlayers.transform.GetChild(i);
            string client_name = Player.GetComponent<LobbyPlayerStatus>().Player_name;
            Debug.Log(client_name);
            Player.GetComponent<LobbyPlayerStatus>().RpcNameSet(client_name);

        }
    }
    //Your own Name
    public void setClientName()
    {
        try
        {
            Player_name = GameObject.Find("InformationHolder").GetComponent<InfoHold>().PlayerName;
        }
        catch
        {
            Debug.Log("NU VREA");
        }
    }


    private void Awake()
    {
        
        try
        {
            JoinedPlayers = GameObject.Find("JoinedPlayers");
        }
        catch
        {
            Debug.Log("Game has started");
        }
        //=======
        DontDestroyOnLoad(gameObject);
    }

    public override void OnClientEnterLobby()
    {
        
        
        base.OnClientEnterLobby();

        if (isServer)
        {
            Debug.Log("OnClientEnterLobby server" + Player_name);
            int n = JoinedPlayers.transform.childCount;
            Debug.Log("Number of current Clients:" + n);
        }
        if(isClient)
        {
            
            Debug.Log("OnClientEnterLobby client" + Player_name);
            gameObject.transform.GetChild(0).GetComponent<Text>().text = Player_name;
            Debug.Log(gameObject.transform.GetChild(0).GetComponent<Text>().text + " imd dupa");

        }
        try
        {
            gameObject.transform.SetParent(JoinedPlayers.transform);
        }
        catch
        {
            Debug.Log("changed scnene");
        }

    }
    //Hook
    public void NameChange(string newName)
    {
        Debug.Log("Player Name Hook Activated with name" + newName + " on " + Player_name);
        Player_name = newName;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = Player_name;

    }
    [Command]
    public void CmdNameChanged(string name)
    {
        Debug.Log("Command issued to server with name " + name);
        Player_name = name;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        //OVER HERE CHANGE VARIABLES!!!
        setClientName();
        CmdNameChanged(Player_name);

        //===============================
        if (isServer)
        {
            Debug.Log(Player_name + " On Start");
            
        }
    }
    public void AllReady(bool rdy)
    {
        ready = rdy;
        GameObject JoinedPlayers = GameObject.Find("JoinedPlayers");
        Transform Player;
        int n = JoinedPlayers.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            Debug.Log(JoinedPlayers.transform.GetChild(i));
            Player = JoinedPlayers.transform.GetChild(i);
            Player.parent = null;
            DontDestroyOnLoad(Player);
            i--;
            n--;
        }
    }
    public override void OnClientReady(bool readyState)
    {
        base.OnClientReady(readyState);
        gameObject.transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }
    [ClientRpc]
    void RpcNameSet(string name)
    {
        Player_name = name;
    }

}
