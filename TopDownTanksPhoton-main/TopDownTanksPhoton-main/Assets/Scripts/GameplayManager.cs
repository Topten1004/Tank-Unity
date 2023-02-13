using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace PUN_Tanks
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] List<Transform> spawnPoses;
        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayer();
        }

        void SpawnPlayer()
        {
           Vector3 spawnPos = spawnPoses[(PhotonNetwork.LocalPlayer.ActorNumber - 1) % PhotonNetwork.CurrentRoom.PlayerCount].position;
           PhotonNetwork.Instantiate("PUN_Player", spawnPos, Quaternion.identity);
        }
    }
}
