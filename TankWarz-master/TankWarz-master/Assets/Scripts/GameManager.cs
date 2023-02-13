using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if(GameManager._gameManager == null)
        {
            GameManager._gameManager = this;
        }
    }
}
