using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class JoinGameMenu : MonoBehaviourPunCallbacks
{

	private GameObject nameJoinPlayerClientField;
	string enteredNamePlayer;
	public GameObject serverNumberTextField;
	string nameServerText;
	public GameObject JoinOkServerText;
	public GameObject JoinErrorServerText;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TryToJoin()
    {
    	JoinOkServerText.SetActive(false);
    	JoinErrorServerText.SetActive(false);
    	try{
        PhotonNetwork.Disconnect();
        }catch{}

    	// Считать имя игрока из введенного поля
        nameJoinPlayerClientField = GameObject.Find("NameInputField");
        InputField namePlayerClientFieldManipulator = nameJoinPlayerClientField.GetComponent<InputField>();
		enteredNamePlayer = namePlayerClientFieldManipulator.text;
		Debug.Log("Player name is: " + enteredNamePlayer);
		
		// Считать имя комнаты, к которой осуществляется подключение
		InputField nameServerManipulator = serverNumberTextField.GetComponent<InputField>();
		nameServerText = nameServerManipulator.text;
		Debug.Log("Name Lobby is: " + nameServerText);
		
		PhotonNetwork.NickName = enteredNamePlayer;
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.GameVersion = "1.0.0";

		// Подкючение к мастер-серверу
		PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster(){
    	Debug.Log("Connected to master");
    	// Создание комнаты после подключения к мастеру
    	// Установим имя игрока на сервер
		PhotonNetwork.NickName = enteredNamePlayer;
    	PhotonNetwork.JoinRoom(nameServerText);
    }

    public override void OnJoinedRoom(){
    	Debug.Log("Joined room");
    	JoinOkServerText.SetActive(true);
    	// PhotonNetwork.LoadLevel("GameScene");
    	// JoinOkServerText.SetActive(false);
    	// JoinErrorServerText.SetActive(false);
    	// PhotonNetwork.LoadLevel("GameScene");
    	// SceneManager.LoadScene(5);
    }

    public override void OnJoinRoomFailed(short returnCode, string message){
    	JoinErrorServerText.SetActive(true);
    }
}
