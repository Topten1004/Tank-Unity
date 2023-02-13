using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class CreateGameMenu : MonoBehaviourPunCallbacks
{

	private GameObject namePlayerClientField;
	public TextMeshProUGUI serverNumberTextField;
	public GameObject startServerText;
    
    void StartServer()
    {
    	// Считать имя игрока из введенного поля
        namePlayerClientField = GameObject.Find("PlayerInputField");
        InputField namePlayerClientFieldManipulator = namePlayerClientField.GetComponent<InputField>();
		string enteredNamePlayer = namePlayerClientFieldManipulator.text;
		// Установим имя игрока на сервер
		PhotonNetwork.NickName = enteredNamePlayer;
		Debug.Log("Player name is: " + PhotonNetwork.NickName);
		// Версия игры для невозможности подключения игроков с другой версией клиента
		PhotonNetwork.GameVersion = "1.0.0";
		// Синхронизирование в переключении сцен (во время конца игры)
		//PhotonNetwork.AutomaticallySyncScene = true;

		// Подкючение к мастер-серверу
		PhotonNetwork.ConnectUsingSettings();
    }

    public void TryToPlay()
    {
        StartServer();
        startServerText.SetActive(true);
        
    }

    // Вывод сообщения, что подключились к мастеру
    public override void OnConnectedToMaster(){
    	Debug.Log("Connected to master");
    	// Создание комнаты после подключения к мастеру
    	Debug.Log(serverNumberTextField);
    	serverNumberTextField.text = "KB2NBL";
    	PhotonNetwork.CreateRoom(serverNumberTextField.text, new Photon.Realtime.RoomOptions{ MaxPlayers = 2});
    }

    public override void OnPlayerEnteredRoom(Player player){
    	PhotonNetwork.AutomaticallySyncScene = true;
    	PhotonNetwork.LoadLevel("GameScene");
    } 


    public void BackToMainMenu(){
    	try{
        PhotonNetwork.Disconnect();
        }catch{}
        SceneManager.LoadScene(0);
    }
}