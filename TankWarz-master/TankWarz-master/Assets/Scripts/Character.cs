using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =" New Character", menuName ="Character")]
public class Character : ScriptableObject
{
    public Sprite _body, _head;
    public string _name;
    public int _ap, _as, _shild, _cd; //attack power, attack speed, shield, critical damage
    public bool _ISSELECTED = false;
    public Animator _animator; //walking animator
    //Const.
    public Character(Sprite body, Sprite head, string name, int attack_speed, int attack_power, int shild, int critical_damage)
    {
        this._body = body;
        this._head = head;
        this._name = name;
        this._ap = attack_power;
        this._as = attack_speed;
        this._shild = shild;
        this._cd = critical_damage;
    }
  

}
