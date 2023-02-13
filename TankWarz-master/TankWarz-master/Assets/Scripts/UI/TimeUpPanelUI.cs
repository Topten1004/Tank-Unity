using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUpPanelUI : MonoBehaviour
{
   

    public void OnClick_BackButton()
    {
        SceneManager.LoadScene("Rooms");
    }

}
