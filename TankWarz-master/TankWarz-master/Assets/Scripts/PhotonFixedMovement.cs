using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonFixedMovement : MonoBehaviourPun, IPunObservable
{
    Vector3 _playerPosition;
    Quaternion _playerRotation;
    float delay = 8.0f;

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, _playerPosition, delay * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _playerRotation, delay * Time.deltaTime);
        }
    }

    //Player Movement Delay Fixed
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            _playerPosition = (Vector3)stream.ReceiveNext();
            _playerRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
