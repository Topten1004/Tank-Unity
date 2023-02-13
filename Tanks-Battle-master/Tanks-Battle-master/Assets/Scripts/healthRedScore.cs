using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class healthRedScore : MonoBehaviour, IPunObservable
{
	public GameObject go;
    RedTank healthController;
    public float healthPointTotal;
    public float healthPointCurrent;
    public float timerCooldown;
    public string namePlayer = "";
    public TextMeshProUGUI manipulatorText;
    public TextMeshProUGUI manipulatorCooldown;
    public TextMeshProUGUI manipulatorName;

    // Локальное представление объекта для клиента
    private PhotonView photonView;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if (stream.IsWriting){
            stream.SendNext(healthPointCurrent);
            stream.SendNext(healthPointTotal);
            stream.SendNext(timerCooldown);
            stream.SendNext(namePlayer);
            Debug.Log("Отправка КД Красный: " + timerCooldown);
        }else{
            healthPointCurrent = (float) stream.ReceiveNext();
            healthPointTotal = (float) stream.ReceiveNext();
            timerCooldown = (float) stream.ReceiveNext();
            namePlayer = (string) stream.ReceiveNext();
            Debug.Log("Прием КД Красный: " + timerCooldown);
        }
    }

    void Start()
    {
            // Найти объект по имени
    		go = GameObject.Find("RedTankMaus");
    		// взять его компонент где лежит здоровье
    		healthController = go.GetComponent<RedTank>();
    		// взять переменную здоровья
    		healthPointTotal = healthController.healthPointTotal;
    		healthPointCurrent = healthController.healthPointCurrent;
    		timerCooldown = healthController.timer;
        
            photonView = GetComponent<PhotonView>();
    }

    
    [PunRPC]
    void RecCooldown(){
        timerCooldown = healthController.timer;
    }

    void Update()
    {

        if (photonView.IsMine){
            healthController = go.GetComponent<RedTank>();
            photonView.RPC("RecCooldown", RpcTarget.All);
        	//timerCooldown = healthController.timer;
        	healthPointCurrent = healthController.healthPointCurrent;
            healthPointTotal = healthController.healthPointTotal;
            namePlayer = PhotonNetwork.PlayerList[0].NickName;
        }
            manipulatorName.text = PhotonNetwork.PlayerList[0].NickName;
        	string s = string.Format("{0:0.00}", timerCooldown);
        	manipulatorText.text = '[' + healthPointCurrent.ToString() + '/' + healthPointTotal.ToString() + ']';
            manipulatorCooldown.text = "КД: " + s + " с";
    }
}
