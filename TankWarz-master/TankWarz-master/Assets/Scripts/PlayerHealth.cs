using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;



public class PlayerHealth : MonoBehaviourPunCallbacks
{

    private int Health = 100;
    public PhotonPlayerCreator _playerCreator;
    private DeathCounter deathCounter;
    public static int Score = 0;
    public static int DeathC = 0;

    private void Start()
    {

        deathCounter = FindObjectOfType<DeathCounter>();
        if (photonView.IsMine)
        {

        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
        }

        _playerCreator = GameObject.Find("MultiplayerManager").GetComponent<PhotonPlayerCreator>();
    }

    public int GetHealth()
    {
        if (photonView.IsMine)
        {
            return this.Health;
        }
        else
        {
            return 0;
        }
    }


    public void GetDamage(string shooterName, GameObject shooter)
    {
        Health -= Random.Range(30,60);
        ExitGames.Client.Photon.Hashtable PlayerCustomProps = new ExitGames.Client.Photon.Hashtable();
        PlayerCustomProps["Score"] = DeathC;
        shooter.GetComponent<PhotonView>().Owner.SetCustomProperties(PlayerCustomProps);
        DeathC = (int)shooter.GetComponent<PhotonView>().Owner.CustomProperties["Score"] + 1;
        Death(shooterName);
    }

    public void Death(string shooter)
    {
        if(Health <= 0 && photonView.IsMine)
        {
            DeathC++;
            deathCounter.GetComponent<PhotonView>().RPC("DeathHappen", RpcTarget.All, shooter, photonView.Owner.NickName);
            _playerCreator.ReSpawn();
            Debug.Log("One player is death");
            PhotonNetwork.Destroy(this.gameObject);

        }
     
    }

    
}
