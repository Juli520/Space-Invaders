using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;
    
    private string _roomName = string.Empty;

    // TODO: Destroy object in Start
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);        
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) 
            return;
        
        DisconnectToServer();
        ConnectToServer();
    }
    
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void DisconnectToServer()
    {
        PhotonNetwork.Disconnect();
    }
    
    public void JoinRoom()
    {
        if (_roomName == string.Empty || _roomName == "")
            return;
        
        PhotonNetwork.JoinRoom(_roomName);
    }
    
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom()
    {
        if (_roomName == string.Empty || _roomName == "")
            return;
        
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;

        PhotonNetwork.CreateRoom(_roomName, options, TypedLobby.Default);
    }

    public void ChangeRoomName(string serverName)
    {
        _roomName = serverName;
    }

    public void ChangeNickName(string nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = nickName;
    }
}
