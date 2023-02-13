using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _createRoomPanel;
    [SerializeField]
    private Button _create_a_Room_Button;
    [SerializeField]
    private InputField _refInputField;
    
    public void BackMainMenuButton_OnClick()
    {
        Debug.Log("Going on the main menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void Create_Panel_Button_OnClick()
    {
        Debug.Log("You've clicked the create a room, panel is activeted");
        _createRoomPanel.gameObject.SetActive(true);
        _create_a_Room_Button.gameObject.SetActive(false); 
    }

    public void BackRoom_OnClick()
    {
        Debug.Log("Going back the room list");
        _createRoomPanel.gameObject.SetActive(false);
        _create_a_Room_Button.gameObject.SetActive(true);
        _refInputField.text = "";
    }

}
