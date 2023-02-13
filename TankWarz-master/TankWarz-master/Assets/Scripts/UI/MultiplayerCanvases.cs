using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCanvases : MonoBehaviour
{
    [SerializeField]
    private RoomsCanvas _roomsCanvas;
    public RoomsCanvas RoomsCanvas { get { return _roomsCanvas;  } }

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrenRoomCanvas {  get { return _currentRoomCanvas;  } }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        RoomsCanvas.FirstInitialize(this);
        CurrenRoomCanvas.FirstInitialize(this);
    }
}
