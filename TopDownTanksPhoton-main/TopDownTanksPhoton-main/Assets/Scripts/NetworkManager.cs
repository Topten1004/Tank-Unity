using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

namespace PUN_Tanks
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] TextMeshProUGUI textLog;
        static NetworkManager instance;
        public event Action onConnectedToServer;
        public event Action onRoomCreated;

        public static NetworkManager Instance => instance;

        private void Awake()
        {
            if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
            else { Destroy(gameObject); }

        }
        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            ConnectToNetwork();
        }

        void ConnectToNetwork()
        {
            PhotonNetwork.GameVersion = "v0.1";
            PhotonNetwork.ConnectUsingSettings();

        }
        public override void OnConnectedToMaster()
        {
            Log("Connected");

            onConnectedToServer?.Invoke();
        }
        public override void OnCreatedRoom()
        {
            Log($"Room {PhotonNetwork.CurrentRoom.Name} is created");
            onRoomCreated?.Invoke();
        }
        public override void OnJoinedRoom()
        {

            Log($"Room {PhotonNetwork.CurrentRoom.Name} is joined");
        }
        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions { MaxPlayers = 8 });

        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Log($"Join Room Failed {message}");
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Log($"{newPlayer.NickName} joined the room");
            
            
        }

        public void LoadGamePlayScene()
        {
            if(!PhotonNetwork.IsMasterClient)
            {
                Log("Only Master Client should load");
            }

            PhotonNetwork.LoadLevel("Gameplay");
        }
        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        public void Log(string msg)
        {
            textLog.text = $"{msg}\n{textLog.text}";
        }
        public void clearLog()
        {
            textLog.text = string.Empty;
        }
    }

}
