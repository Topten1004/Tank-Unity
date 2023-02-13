using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _playerListPrefab;
    [SerializeField]
    private Text _readyUpText;
    private List<PlayerListing> _listing = new List<PlayerListing>();
    private MultiplayerCanvases _canvases;
    private bool _ready = false;


    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
        SetReadyUp(false);
    }
    
    public override void OnDisable()
    {
        base.OnDisable();
        for(int i = 0; i < _listing.Count; i++)
        {
            Destroy(_listing[i].gameObject);
        }
        _listing.Clear();
    }

    public void FirstInitialize(MultiplayerCanvases canvases)
    {
        _canvases = canvases;
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach(KeyValuePair<int, Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerinfo.Value);
        }
        
    }
    
    private void SetReadyUp(bool state)
    {

        _ready = state;
        if (_ready)
            _readyUpText.text = "Nope";
        else
            _readyUpText.text = "Ready";
    }

    private void AddPlayerListing(Player player)
    {
        int index = _listing.FindIndex(x => x.Player == player);
        if(index != -1)
        {
            _listing[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = (PlayerListing)Instantiate(_playerListPrefab, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listing.Add(listing);
            }
        }
       
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _canvases.CurrenRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        int index = _listing.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listing[index].gameObject);
            _listing.RemoveAt(index);
        }
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i< _listing.Count; i++)
            {
                if(_listing[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_listing[i].Ready)
                        return;
                }
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(2);

        }
        
    }


    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);

        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {

        int index = _listing.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listing[index].Ready = ready;
         
        }
    }
}
