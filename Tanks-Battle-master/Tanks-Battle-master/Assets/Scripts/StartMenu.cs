using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class StartMenu : MonoBehaviour
{
    public void OpenCreateGame()
    {
        try{
        PhotonNetwork.Disconnect();
        }catch{}

        SceneManager.LoadScene(1);
    }

    public void OpenJoinGame()
    {
        try{
        PhotonNetwork.Disconnect();
        }catch{}
        SceneManager.LoadScene(2);
    }


    public void OpenControl()
    {
        SceneManager.LoadScene(6);
    }

    public void OpenRecords()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
