using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlToMainMenu : MonoBehaviour
{
    public void OpenStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
