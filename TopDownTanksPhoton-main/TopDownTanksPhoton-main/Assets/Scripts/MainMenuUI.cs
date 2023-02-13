using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace PUN_Tanks
{

    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI room;
        [SerializeField] GameObject lobbyPanel;
        [SerializeField] Button CreateRoomBtn;
        [SerializeField] Button JoinRoomBtn;
        [SerializeField] Button startGameBtn;
        [SerializeField] Button readyBtn;
        [SerializeField] TextMeshProUGUI readyBtnText;
        [SerializeField] TMP_Dropdown dd_playerColor;
        List<NetworkPlayer> networkPlayers = new List<NetworkPlayer>();

        public bool isReady;
        bool readyBardo;
        private void Start()
        {
            NetworkManager.Instance.onConnectedToServer += onConnectedToServer;
            NetworkManager.Instance.onRoomCreated += onRoomCreated;
            //startGameBtn.interactable = false;
            readyBtn.interactable = false;
        }

        private void onRoomCreated()
        {

            readyBtn.interactable = true;

        }



        private void onConnectedToServer()
        {
            lobbyPanel.SetActive(true);
        }

        public void OnNameUpdated(string pName)
        {
            PhotonNetwork.NickName = pName;
            
        }
        public void CreateRoom()
        {
            if (!string.IsNullOrEmpty(room.text))
            {
                NetworkManager.Instance.CreateRoom(room.text);
            }
            JoinRoomBtn.interactable = false;
            CreateRoomBtn.interactable = false;

        }
        public void JoinRoom()
        {
            if (!string.IsNullOrEmpty(room.text))
            {
                NetworkManager.Instance.JoinRoom(room.text);
            }
            CreateRoomBtn.interactable = false;
            JoinRoomBtn.interactable = false;
            readyBtn.interactable = true;
        }

        public void StartGame()
        {
            NetworkManager.Instance.LoadGamePlayScene();
        }

        public void LogRoom()
        {
            NetworkManager.Instance.Log(PhotonNetwork.CountOfRooms.ToString());
        }

        private void OnPlayerConnected(NetworkPlayer player)
        {
            networkPlayers.Add(player);
            if(readyBardo)
            {
                player.playerReady = true;
            }
        }

        public void onColorUpdated(int colorIdx)
        {
            Color pColor = Color.red;

            switch (colorIdx)
            {
                case 0:
                    pColor = Color.red;
                    break;
                case 1:
                    pColor= Color.blue;
                    break;
                case 2:
                    pColor= Color.black;
                    break;
                
            }


            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();
            playerProps.Add("Color_r", pColor.r);
            playerProps.Add("Color_g", pColor.g);
            playerProps.Add("Color_b", pColor.b);
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                LogRoom();
            }

            if (isReady && PhotonNetwork.IsMasterClient)
            {
                startGameBtn.interactable = true;

            }
        }

        public void onReadyButton()
        {
            readyBtn.interactable = false;
            readyBtnText.text = "Waiting";
            readyBardo = true;
            isReady = HandlePlayersReady();
            
        }

        private bool HandlePlayersReady()
        {
            for (int i = 0; i < networkPlayers.Count; i++)
            {
                if(!networkPlayers[i].playerReady)
                {
                    return false;
                }
            }
            
            return true;

        }
    }
}