using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

// , IPunObservable
public class ManagerEnemyTanks : MonoBehaviourPunCallbacks
{

    public GameObject EnemyPrefab;
    public EnemyTanks EnemyTankManipulator;
    public List<GameObject> ListEnemyTanks;
    public int totalGenerated = 0;

    // Часть, связанная с окончанием игры
	public GameObject finishObject;
    public TextMeshProUGUI manipulatorText;
    public PhotonView photonView;

    void Start()
    {
    	// Создание вражеских танков
    	if (PhotonNetwork.IsMasterClient){
	        if (EnemyPrefab.gameObject.name == "EnemyTankBlackNetwork"){
	        	   //Debug.Log("Нашел батю");
	        	   EnemyTankManipulator = EnemyPrefab.GetComponent<EnemyTanks>();
	    	       ListEnemyTanks.Add(EnemyPrefab);
	    	       // Вызов функции создания
	        	   InvokeRepeating("NewEnemyTankGenerate", 1, 10);

	        }
    	}
    }

    public void NewEnemyTankGenerate()
    {
        // Дабы не создавать потомков от клонов
        // Создаем итого 10 новых танков
        Debug.Log("НЕ Зашел в функцию генерации пог");
        if (PhotonNetwork.IsMasterClient){
        	Debug.Log("Зашел в функцию генерации пог");
        if ((EnemyPrefab.gameObject.name == "EnemyTankBlackNetwork") && (totalGenerated <= 10)) {
            Vector3 enemyStartPosition = new Vector3(2.611f, 7.05f, 1.42f);
            GameObject instantedObject = PhotonNetwork.Instantiate(EnemyPrefab.name, enemyStartPosition, Quaternion.identity);
            int index = ListEnemyTanks.Count + 1;
            instantedObject.name = "EnemyTankBlack " + index.ToString();
            instantedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            instantedObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            instantedObject.SetActive(true);
            // Зададим свойства
            EnemyTanks enemy = instantedObject.GetComponent<EnemyTanks>();
            enemy.timer = EnemyTankManipulator.cooldown;
            enemy.speed = EnemyTankManipulator.speed;
            enemy.healthPointCurrent = EnemyTankManipulator.healthPointTotal;
            enemy.pivot = Vector3.up;
            enemy.alive = true;
            enemy.aiEnemyTank.enabled = true;

            // Новый сгенерированный танк хранится в списке (как наследованные от главного)
            ListEnemyTanks.Add(instantedObject);
            totalGenerated += 1;

        	}
    		}
	}

    [PunRPC]
    public void RecieveGeneratedAll(){
        totalGenerated = 11;
    }

    public void OpenFinishMenu()
    {
    	finishObject.gameObject.SetActive(true);
        // Найти объект по имени
		// Выясним выиграли мы или проиграли
		GameObject enemyManager = GameObject.Find("EnemyTankManager");
        ManagerEnemyTanks enemyManagerManipulator = enemyManager.GetComponent<ManagerEnemyTanks>();
        //EnemyTanks mainEnemyManipulator = mainEnemy.GetComponent<EnemyTanks>();

		// взять переменную здоровья

		int totalAlive = enemyManagerManipulator.ListEnemyTanks.Count;
        if (PhotonNetwork.IsMasterClient){
        if (totalAlive != 1){
        	manipulatorText.text = "Победа ботов в сражении!";
        }else{
            //PhotonNetwork.PlayerList[1].NickName
            photonView.RPC("RecieveGeneratedAll", RpcTarget.All);
        	manipulatorText.text = "Победа союзников в сражении!";
    	}
    	}else{
    	        GameObject HPGreen = GameObject.Find("healthPoint");
                TextMeshProUGUI HPCurrentGreen = HPGreen.GetComponent<TextMeshProUGUI>();
                GameObject HPRed = GameObject.Find("healthPointRed");
                TextMeshProUGUI HPCurrentRed = HPRed.GetComponent<TextMeshProUGUI>();
                // Если нечего удалять - поражение
                if ((HPCurrentGreen.text[1] == '0') && (HPCurrentRed.text[1] == '0'))
                	manipulatorText.text = "Победа ботов в сражении!";
        		else
        			manipulatorText.text = "Победа союзников в сражении!";
        	}
	}


    void Update()
    {
        
    }
}
