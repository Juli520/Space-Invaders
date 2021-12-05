using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;
    public string level;
    public string menu;
    public GameObject error;
    private string _roomName = string.Empty;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            PhotonNetwork.Destroy(gameObject);
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
        if (_roomName == string.Empty || _roomName == "" ||
            PhotonNetwork.LocalPlayer.NickName == "" ||
            PhotonNetwork.LocalPlayer.NickName == string.Empty)
            return;

        PhotonNetwork.JoinRoom(_roomName);
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom()
    {
        if (_roomName == string.Empty || _roomName == "" ||
            PhotonNetwork.LocalPlayer.NickName == "" ||
            PhotonNetwork.LocalPlayer.NickName == string.Empty)
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
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

    public string GetRoomName()
    {
        return _roomName;
    }

    public Photon.Realtime.Player[] GetPlayerList()
    {
        return PhotonNetwork.PlayerList;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CheckPlayerCount()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
            photonView.RPC("StartGame", RpcTarget.All);
        else
            error.SetActive(true);
    }
    
    [PunRPC]
    public void StartGame()
    {
        if (level == string.Empty)
            return;

        PhotonNetwork.LoadLevel(level);
    }

    public override void OnConnected()
    {
    }

    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(menu);
    }
}
