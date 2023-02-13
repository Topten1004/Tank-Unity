using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public int ObjectLeft; //Object which is coming from
    [SerializeField]
    private GameObject _exploEffect;
    public int KillC = 0;

    private void Start()
    {
        Destroy(this.gameObject, 10);
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(ObjectLeft != collision.gameObject.GetComponent<PhotonView>().ViewID)
            {
                photonView.RPC("Damage", RpcTarget.All, ObjectLeft, collision.GetComponent<PhotonView>().ViewID); 
                //bu efecti multiplayer için düzenle
                GameObject _expoEffect = Instantiate(_exploEffect);
                _expoEffect.transform.position = new Vector3(collision.gameObject.transform.position.x + 1, collision.gameObject.transform.position.y, 0);
                Destroy(_expoEffect.gameObject,2);

            }
        }
        else if(collision.tag == "Block")
        {
            PhotonNetwork.Destroy(this.gameObject);

        }
        
    }

    [PunRPC]
    public void SenderID(int playerID)
    {
        ObjectLeft = playerID;
    }

    [PunRPC]
    public void Damage(int ReceiverPlayer, int localPlayer)
    {
        OfflineControl(ReceiverPlayer, localPlayer);
    }


    public List<GameObject> allPlayerObjects;
    GameObject damageTaker, shooter;
    string shooterName;
    public void OfflineControl(int rPlayer, int lPlayer)
    {
        foreach(GameObject receiverPlayer in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (receiverPlayer.GetComponent<PhotonView>())
            {
                allPlayerObjects.Add(receiverPlayer);
            }
        }

        for(int i = 0; i<allPlayerObjects.Count; i++)
        {
            int newID = i;
            if(allPlayerObjects[newID].GetComponent<PhotonView>().ViewID == lPlayer)
            {
                damageTaker = allPlayerObjects[newID];
            }
            if(allPlayerObjects[newID].GetComponent<PhotonView>().ViewID == ObjectLeft)
            {
                shooterName = allPlayerObjects[newID].GetComponent<PhotonView>().Owner.NickName;
                shooter = allPlayerObjects[newID];
            }
           
        }
        damageTaker.GetComponent<PlayerHealth>().GetDamage(shooterName, shooter);
        

        Destroy(this.gameObject);
    }

}
