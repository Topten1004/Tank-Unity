using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class CreateARoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    private MultiplayerCanvases _MultiplayerCanvases;

    public void FirstInitialize(MultiplayerCanvases _canvases)
    {
        _MultiplayerCanvases = _canvases;
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions _options = new RoomOptions();
        _options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, _options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully");
        _MultiplayerCanvases.CurrenRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed:" + message);
    }


}
