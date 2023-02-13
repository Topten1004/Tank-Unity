using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public Player Player { get; private set; }
    public bool Ready = false;
    public int ScoreC = 0;
    public static int Death = 0;

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _text.text = player.NickName;
        ScoreC = (int)player.CustomProperties["Score"];

    }



}
