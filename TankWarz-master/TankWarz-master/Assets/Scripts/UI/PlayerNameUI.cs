using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField]
    private Text _playerName;

    private void Update()
    {
        _playerName.text = PhotonNetwork.LocalPlayer.NickName;
    }

}
