using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ManagerGreenTank : MonoBehaviourPunCallbacks
{
    public GameObject GreenPrefab;

    void Start()
    {
    	// Создание зеленого танка
    	if (!PhotonNetwork.IsMasterClient){
        	Vector3 position = new Vector3(-5.556f, 7.051047f, -1.946f);
        	GameObject objectGreen = PhotonNetwork.Instantiate(GreenPrefab.name, position, Quaternion.identity);
        	objectGreen.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
