using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateARoom _createRoomMenu;
    [SerializeField]
    private RoomListingMenu _roomListingMenu; 

    private MultiplayerCanvases _MultiplayerCanvases;

    public void FirstInitialize(MultiplayerCanvases _canvases)
    {
        _MultiplayerCanvases = _canvases;
        _createRoomMenu.FirstInitialize(_canvases);
        _roomListingMenu.FirstInitialize(_canvases);
    }
}
