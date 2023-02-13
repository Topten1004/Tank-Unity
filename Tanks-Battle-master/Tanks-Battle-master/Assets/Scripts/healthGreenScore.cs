using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class healthGreenScore : MonoBehaviour, IPunObservable
{
    public GameObject go;
    GreenTank healthController;
    public float healthPointTotal = -1;
    public float healthPointCurrent = -1;
    public float timerCooldown;
    public string namePlayer = "";
    private TextMeshProUGUI manipulatorText;
    public TextMeshProUGUI manipulatorCooldown;
    public TextMeshProUGUI manipulatorName;

    // Локальное представление объекта для клиента
    private PhotonView photonView;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if (stream.IsWriting){
            Debug.Log("ОТПРАВКА ДАННЫХ Зеленый");
            stream.SendNext(healthPointCurrent);
            stream.SendNext(healthPointTotal);
            //stream.SendNext(timerCooldown);
            stream.SendNext(namePlayer);
        }else{
            Debug.Log("ПРИЕМ ДАННЫХ Зеленый");
            healthPointCurrent = (float) stream.ReceiveNext();
            healthPointTotal = (float) stream.ReceiveNext();
            //timerCooldown = (float) stream.ReceiveNext();
            namePlayer = (string) stream.ReceiveNext();
            Debug.Log("HPC " + healthPointCurrent);
            Debug.Log("HPT " + healthPointTotal);
            //Debug.Log("TC " + timerCooldown);
        }
    }


    void Start()
    {
        manipulatorText = GetComponent<TextMeshProUGUI>();
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    void GreenRecCooldown(float cdSend){
        Debug.Log("ОТПРАВКА КД Зеленый в функции: " + cdSend);
        timerCooldown = cdSend;
    }

    void Update()
    {
        /*if (photonView.IsMine){
            go = GameObject.Find("GreenTankNetwork(Clone)");
            healthController = go.GetComponent<GreenTank>();
            healthPointTotal = healthController.healthPointTotal;
            healthPointCurrent = healthController.healthPointCurrent;
            timerCooldown = healthController.timer;
            namePlayer = PhotonNetwork.PlayerList[1].NickName;
        }

        manipulatorName.text = PhotonNetwork.PlayerList[1].NickName;
        string s = string.Format("{0:0.00}", timerCooldown);
        manipulatorText.text = '[' + healthPointCurrent.ToString() + '/' + healthPointTotal.ToString() + ']';
        manipulatorCooldown.text = "КД: " + s + " с";
        }*/


        if (photonView.IsMine){
            go = GameObject.Find("GreenTankNetwork(Clone)");
            healthController = go.GetComponent<GreenTank>();
            healthPointTotal = healthController.healthPointTotal;
            healthPointCurrent = healthController.healthPointCurrent;
            //timerCooldown = healthController.timer;
            namePlayer = PhotonNetwork.PlayerList[1].NickName;
        }

        go = GameObject.Find("GreenTankNetwork(Clone)");
        float cd = 0.00f;
        if ((go != null) && !PhotonNetwork.IsMasterClient){
            healthController = go.GetComponent<GreenTank>();
            float cdSend = 0.00f;
            cd = healthController.timer;
            cdSend = healthController.timer;
            Debug.Log("ОТПРАВКА КД Зеленый до функции: " + cdSend);
            photonView.RPC("GreenRecCooldown", RpcTarget.All, cdSend);
        }

        manipulatorName.text = PhotonNetwork.PlayerList[1].NickName;
        string s = "";
        if (!PhotonNetwork.IsMasterClient)
            s = string.Format("{0:0.00}", cd);

        manipulatorText.text = '[' + healthPointCurrent.ToString() + '/' + healthPointTotal.ToString() + ']';
        if (!PhotonNetwork.IsMasterClient){
            manipulatorCooldown.text = "КД: " + s + " с";
            //Debug.Log("Текущий кулдаун на зеленом " + cd);
        }else{
            Debug.Log("Прием КД Зеленый: " + timerCooldown);
            manipulatorCooldown.text = "КД: " + string.Format("{0:0.00}", timerCooldown) + " с";
        }
        
        //if (PhotonNetwork.IsMasterClient){
        //    manipulatorCooldown.text = "КД: " + string.Format("{0:0.00}", timerCooldown) + " с";
            //Debug.Log("Текущий кулдаун на зеленом " + timerCooldown);
        //}
    }
}
