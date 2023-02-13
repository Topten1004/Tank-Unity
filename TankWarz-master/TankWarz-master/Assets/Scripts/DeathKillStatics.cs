using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Realtime;
using Photon.Pun;

public class DeathKillStatics : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Text _deathText;
    public Text _maxScore;
    public Text _playerMax;
    public Text _winnerName;
    private void Update()
    {
        Display();
        PanelDisplay();
    }

    public void Display()
    {
        _deathText.text = PhotonNetwork.LocalPlayer.CustomProperties["Score"].ToString();
    }


    public void PanelDisplay()
    {

        int maxScore = 0;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if(int.Parse(player.CustomProperties["Score"].ToString()) >= maxScore)
            {
                maxScore = int.Parse(player.CustomProperties["Score"].ToString());
                _maxScore.text = maxScore.ToString();
                _playerMax.text = player.NickName;
                _winnerName.text = player.NickName;
            }
            Debug.Log(player.NickName + " Score : " + player.CustomProperties["Score"].ToString());
            

        }
    }
}
