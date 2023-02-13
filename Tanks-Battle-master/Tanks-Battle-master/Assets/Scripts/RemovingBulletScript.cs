using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RemovingBulletScript : MonoBehaviour
{
    void Start(){}
    
    void Update(){}

    // Проверка на получение урона
    void OnCollisionEnter(Collision myTrigger){
  		if ((myTrigger.gameObject.name == "firstLevelBullet(Clone)") || (myTrigger.gameObject.name == "fiveLevelBullet(Clone)"))
  		{
        PhotonView pv = myTrigger.gameObject.GetComponent<PhotonView>();
        // Для красного удаляет мастер,  для грина каждый сам
        if (pv.IsMine && (pv.Owner == PhotonNetwork.PlayerList[1])){
    	    PhotonNetwork.Destroy(myTrigger.gameObject);
    		  Debug.Log("Bullet removed: ");
        }

        if (PhotonNetwork.IsMasterClient && (pv.Owner == PhotonNetwork.PlayerList[0])){
          Debug.Log("Решил что я мастер: ");
          PhotonNetwork.Destroy(myTrigger.gameObject);
          Debug.Log("Зачем то удалил пулю мастера: ");
        }
      }
    }
}