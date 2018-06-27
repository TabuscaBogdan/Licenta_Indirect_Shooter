using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using UnityEngine.UI;
public class NetworkPlayerInfoHooks : LobbyHook {
    /*
    [Command]
    public void cmdSetName(string name)
    {

    }*/


    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //var cc = lobbyPlayer.GetComponent<ColorControl>();
        Debug.Log(lobbyPlayer);
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();

        Color color = lobby.playerColor;
        string display_name = lobby.playerName;

        Debug.Log("display_name:" + display_name);
        Debug.Log("Color is:" + color);
        Debug.Log(gamePlayer);
        Debug.Log(gamePlayer.transform.GetChild(1));
        Debug.Log(gamePlayer.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1));

        gamePlayer.GetComponent<Status>().name = display_name;
        gamePlayer.GetComponent<Status>().team = (int)(color.r*2 + color.g*3+color.b*5);
        gamePlayer.GetComponent<Status>().teamColor = color;

        gamePlayer.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = display_name;
        gamePlayer.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().color = color;

    }
    
}
