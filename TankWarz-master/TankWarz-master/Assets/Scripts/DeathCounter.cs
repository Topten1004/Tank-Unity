using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DeathCounter : MonoBehaviour
{
    public GameObject SlotPrefab;
    public Transform places;

  [PunRPC]
  public void DeathHappen(string _Killer, string _Dead)
    {
        InstantiateObject(_Killer, _Dead); //Will instantiate ui object. 
    }

    private void InstantiateObject(string Killer, string Dead)
    {
        GameObject newKillObject = Instantiate(SlotPrefab, places);
        newKillObject.transform.Find("Player1Text").GetComponent<Text>().text = Killer;
        newKillObject.transform.Find("Player2Text").GetComponent<Text>().text = Dead;
        Destroy(newKillObject, 3);
    }
}
