using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Manager/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
   
    [SerializeField]
    private string _nickName = "default";

    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickName + value.ToString();
        }

    }


    public void SetNickName(string name)
    {
        _nickName = name;
    }


}
