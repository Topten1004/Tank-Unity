using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    //TODO: MAKE A SINGLE PLAYER VERSİON
    //TODO: MAKE A SINGLE PLAYER BUTTON

    public void OnClick_Rooms()
    {
        Debug.Log("Join a Room Scene is loading");
        SceneManager.LoadScene("Rooms");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }


}
