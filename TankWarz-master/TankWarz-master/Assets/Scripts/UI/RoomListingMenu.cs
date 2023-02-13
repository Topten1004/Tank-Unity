using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomListing _roomListPrefab;


    private List<RoomListing> _listing = new List<RoomListing>();
    private MultiplayerCanvases _canvases;

    public void FirstInitialize(MultiplayerCanvases canvases)
    {
        _canvases = canvases;
    }

    public override void OnJoinedRoom()
    {
        _canvases.CurrenRoomCanvas.Show();
        _content.DestroyChildren();
        _listing.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            //Removed from rooms list.
            if (info.RemovedFromList)
            {
                int index = _listing.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(_listing[index].gameObject);
                    _listing.RemoveAt(index);
                }
            }
            //Added to rooms list.
            else{
                int index = _listing.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index == -1)
                {
                    RoomListing listing = (RoomListing)Instantiate(_roomListPrefab, _content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listing.Add(listing);
                    }
                }
                else
                {

                }
            }
           
        }
    }
}
