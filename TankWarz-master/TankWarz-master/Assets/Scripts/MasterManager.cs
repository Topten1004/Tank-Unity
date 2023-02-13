using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : ScriptableObjectSingleton<MasterManager>
{
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private PlayerSettings _playerSettings;
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }
    public static PlayerSettings PlayerSettings { get { return Instance._playerSettings; } }
}
