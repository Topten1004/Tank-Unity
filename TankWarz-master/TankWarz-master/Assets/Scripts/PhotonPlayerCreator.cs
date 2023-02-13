using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using System.IO;


public class PhotonPlayerCreator : MonoBehaviour
{

   

    void Start()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        int spawnPicker = Random.Range(0, GameManager._gameManager.spawnPoints.Length);
        Debug.Log("Generated number: " + spawnPicker);
        PhotonNetwork.Instantiate("TankPlayer", GameManager._gameManager.spawnPoints[spawnPicker].position, GameManager._gameManager.spawnPoints[spawnPicker].rotation, 0);
    }

    public void ReSpawn()
    {
        StartCoroutine(ReSpawnTimer());
    }

    IEnumerator ReSpawnTimer()
    {
        yield return new WaitForSeconds(3);
        int spawnPicker = Random.Range(0, GameManager._gameManager.spawnPoints.Length);
        PhotonNetwork.Instantiate("TankPlayer", GameManager._gameManager.spawnPoints[spawnPicker].position, GameManager._gameManager.spawnPoints[spawnPicker].rotation, 0);
    }



}
