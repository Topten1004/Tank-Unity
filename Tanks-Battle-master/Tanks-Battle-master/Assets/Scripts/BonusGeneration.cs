using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BonusGeneration : MonoBehaviour
{
    // Размер поля
    public float xLeft = -6f;
    public float xRight = 3f;
    public float zTop = -4.5f;
    public float zBot = 4.5f;
    public Vector3 generatedPosition;
    public float generatedBonus;
    // Бонус - либо здоровье, либо увеличенный урон от пушки
    public GameObject bonusPrefab;
    public GameObject healthPrefab;
    public GameObject damagePrefab;



    // Генерирование позиции
    Vector3 RandomPosition()
    {
        generatedPosition = new Vector3(Random.Range(xLeft, xRight), 7.05f, Random.Range(zTop, zBot));
        return generatedPosition;
    }

    // Генерирование бонуса
    GameObject RandomTypeBonus()
    {
        generatedBonus = Random.Range(0.0f, 1.0f);
        // Сгенерирован < 0.5 -> здоровье
        // Сгенерирован > 0.5 -> урон
        if (generatedBonus < 0.5)
            bonusPrefab = healthPrefab;
        else
            bonusPrefab = healthPrefab;
        return bonusPrefab;
    }

    void NewBonusGenerate()
    {
        GameObject instantedObject;
        instantedObject = PhotonNetwork.Instantiate(healthPrefab.name, RandomPosition(), Quaternion.identity) as GameObject;
        //instantedObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //instantedObject.transform.eulerAngles = new Vector3(0.0f, -0.0f, 0.0f);
        /*if (bonusPrefab == healthPrefab)
        {
            instantedObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            instantedObject.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
        }
        else
        {
            instantedObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            instantedObject.transform.eulerAngles = new Vector3(0.0f, -90.0f, -90.0f);
        }*/
    }


    // Отрисовка сгенерированного бонуса
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            InvokeRepeating("NewBonusGenerate", 20, 40);
    }
}
