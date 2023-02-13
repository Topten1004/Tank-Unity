using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class PlayerCharacter : MonoBehaviourPun
{
    [SerializeField]
    Character _character;
    [SerializeField]
    private GameObject _roketBullet;
    public float _bulletSpeed = 1000f;
    [SerializeField]
    private string _nickname;


    private void Start()
    {
        Debug.Log(_character._name);
        GameObject body = this.gameObject.transform.GetChild(1).gameObject;
        GameObject gun = this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log(body.name);
        Debug.Log(gun.name);
        body.GetComponent<SpriteRenderer>().sprite = _character._body;
        gun.GetComponent<SpriteRenderer>().sprite = _character._head;
    }

    private void Update()
    {
       
        Fire();
    }


    public void Fire()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject _bullet = PhotonNetwork.Instantiate("Bullet", this.transform.position, Quaternion.identity, 0);
                //_bullet.transform.position = this.transform.position;
                _bullet.GetComponent<PhotonView>().RPC("SenderID", RpcTarget.All, photonView.ViewID);
                _bullet.GetComponent<Rigidbody2D>().AddForce(this.transform.up * _bulletSpeed);
            }
        }
        
      
    }





}
