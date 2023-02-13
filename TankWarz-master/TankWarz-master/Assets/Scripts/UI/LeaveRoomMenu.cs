using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoomMenu : MonoBehaviour
{
    private MultiplayerCanvases _canvases;

    public void FirstInitialize(MultiplayerCanvases canvases)
    {
        _canvases = canvases;
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _canvases.CurrenRoomCanvas.Hide();
    }

}
