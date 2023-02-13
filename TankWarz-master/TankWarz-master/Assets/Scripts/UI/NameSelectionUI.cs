using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NameSelectionUI : MonoBehaviour
{
    [SerializeField]
    private InputField _playerNametextField;

    public void OnClick_Done()
    {
        MasterManager.PlayerSettings.SetNickName(_playerNametextField.text);
        PhotonNetwork.NickName = MasterManager.PlayerSettings.NickName;
        Debug.Log("Player nickname: " + PhotonNetwork.NickName);
    }
}
