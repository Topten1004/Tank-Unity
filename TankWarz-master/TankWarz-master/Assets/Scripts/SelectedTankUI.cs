using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTankUI : MonoBehaviour
{
    public TankSelectionScroolBar _scroolbar;
    public Character[] characters;
    public Image _body;
    public Image head;
    public Text _attackPower;
    public Text _attackSpeed;
    public Text _shield;
    public Text _criticalAttack;
    public Text _TankName;


    public void OnClick_Done()
    {
       if(_scroolbar.GetSelected().y == 122.5f)
        {
            _body.sprite = characters[0]._body;
            head.sprite = characters[0]._head;
            _attackPower.text = characters[0]._ap.ToString();
            _attackSpeed.text = characters[0]._as.ToString();
            _shield.text = characters[0]._shild.ToString();
            _criticalAttack.text = characters[0]._cd.ToString();
            _TankName.text = characters[0]._name;
            characters[0]._ISSELECTED = true;
            Debug.Log(characters[0].name);
        }
       else if(_scroolbar.GetSelected().y == 22.5f)
        {
            _body.sprite = characters[1]._body;
            head.sprite = characters[1]._head;
            _attackPower.text = characters[1]._ap.ToString();
            _attackSpeed.text = characters[1]._as.ToString();
            _shield.text = characters[1]._shild.ToString();
            _criticalAttack.text = characters[1]._cd.ToString();
            _TankName.text = characters[1]._name;
            characters[1]._ISSELECTED = true;
            Debug.Log(characters[1].name);
        }
       else if(_scroolbar.GetSelected().y == -77.5f)
        {
            _body.sprite = characters[2]._body;
            head.sprite = characters[2]._head;
            _attackPower.text = characters[2]._ap.ToString();
            _attackSpeed.text = characters[2]._as.ToString();
            _shield.text = characters[2]._shild.ToString();
            _criticalAttack.text = characters[2]._cd.ToString();
            _TankName.text = characters[2]._name;
            characters[2]._ISSELECTED = true;
            Debug.Log(characters[2].name);
        }
    }


}
