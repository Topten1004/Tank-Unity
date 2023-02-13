using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PauseMenu : MonoBehaviourPunCallbacks, IPunObservable
//public class PauseMenu : MonoBehaviourPunCallbacks
{

	public GameObject pauseObject;
    public bool pauseIn = false;


    // Локальное представление объекта для клиента
    public PhotonView photonView;

    //void Start()
    //{
            //photonView = GetComponent<PhotonView>();
    //}

     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if (stream.IsWriting){
            Debug.Log("ОТПРАВКА ДАННЫХ О ПАУЗЕ");
            stream.SendNext(pauseIn);
            if (!pauseIn)
                Debug.Log("ОТПРАВИТЬ ИНФУ ВЕРНУТЬСЯ В ИГРУ");
            }
            else{
                Debug.Log("ПРИЕМ ДАННЫХ О ПАУЗЕ");
                pauseIn = (bool) stream.ReceiveNext();
            if (!pauseIn)
                Debug.Log("ПРИШЛА ИНФА ВЕРНУТЬСЯ В ИГРУ");
            }
    }

    // Выйти из игры
    public void OpenStartMenu()
    {
        if (pauseObject != null)
            pauseObject.gameObject.SetActive(false);
        Time.timeScale = 1;
        PhotonNetwork.LoadLevel("StartMenu");
        Debug.Log("Уровень есть!!!");
        // PhotonNetwork.Disconnect();
        //SceneManager.LoadScene(0);
    }

    [PunRPC]
    public void PauseButtonClicked(){
        pauseIn = true;
    	pauseObject.gameObject.SetActive(true);
    	Time.timeScale = 0.02f;
    }

    [PunRPC]
    public void BackToGame(){
    	if (pauseObject != null)
            pauseObject.gameObject.SetActive(false);
        Time.timeScale = 1;
        pauseIn = false;
    }
    
    //void Start(){
    //    pauseIn = true;
    //}

    public void ReceivePause(){
        photonView.RPC("PauseButtonClicked", RpcTarget.All);
    }

    public void ReceiveBack(){
        photonView.RPC("BackToGame", RpcTarget.All);
    }

    /*void Update(){
        // Поставить паузу, если онв была поставлена хотя бы на 1 из клиентов
        if (pauseIn && Time.timeScale == 1)
            photonView.RPC("PauseButtonClicked", RpcTarget.All);
        if (!pauseIn && pauseObject == )
            photonView.RPC("BackToGame", RpcTarget.All);
        //if (pauseIn)
            //photonView.RPC("PauseButtonClicked", RpcTarget.All);
      //  else
      //      photonView.RPC("BackToGame", RpcTarget.All);
    }*/
}
