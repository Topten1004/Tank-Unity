using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private PlayerListingMenu _playerListingMenu;
    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;
    private MultiplayerCanvases _multiplayerCanvases;

    public LeaveRoomMenu LeaveRoomMenu { get { return _leaveRoomMenu; } }

    public void FirstInitialize(MultiplayerCanvases _canvases)
    {
        _multiplayerCanvases = _canvases;
        _playerListingMenu.FirstInitialize(_canvases);
        _leaveRoomMenu.FirstInitialize(_canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
