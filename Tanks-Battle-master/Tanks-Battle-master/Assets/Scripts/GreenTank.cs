using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GreenTank : MonoBehaviourPunCallbacks
{
    public float speed = 1f;
    public float rotationSpeed = 100f;
    // Кулдаун пушки зеленого танка = 5 с.
    public float cooldown = 5f;
    public float timer = 0;
    // Префабы(ссылки на них) задаются в редакторе
    public GameObject bullet;
    public GameObject dulo;
    public float speedBullet = 8000f;

    // Здоровье игрока
    public float healthPointTotal = 300;
    public float healthPointCurrent = 300;
    public float countDamage = 200;
    public bool alive = true;
    // private GameObject go;
    GreenTank PositionController;

    // Локальное представление объекта для клиента
    public PhotonView photonView;

    // Первый кадр в отрисовке танка
    void Start()
    {
        /*
        go = GameObject.Find("GreenTank");
        // Найти контроллер позиции танка
        PositionController = go.GetComponent<GreenTank>();
        */  

        // Клиент создает зеленый танк и им управляет (нет)
            // Vector3 position = new Vector3(-5.556f, 7.051047f, -1.946f);
            // GameObject objectGreen = PhotonNetwork.Instantiate(GreenPrefab.name, position, Quaternion.identity);
            // objectGreen.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
            //Debug.Log("Создал зеленого: ");
            PositionController = GetComponent<GreenTank>();
            // Debug.Log(PositionController);
            // Инициализация локального предствления
            photonView = GetComponent<PhotonView>();
            // Debug.Log("Строка 50 -> " + photonView.ViewID);
            //ViewID = photonView.ViewID;
        //}
    }

    // Проверка на получение урона
    // Проверка на взятие бонуса
    void OnCollisionEnter(Collision myTrigger){
        if ((myTrigger.gameObject.name == "fiveLevelBullet(Clone)"))
        {
            healthPointCurrent -= 100;
            Debug.Log("Damaged GreenTank : " + healthPointCurrent);
            PhotonNetwork.Destroy(myTrigger.gameObject);
            Debug.Log("Bullet removed: ");
            if (healthPointCurrent <= 0){
                alive = false;
                healthPointCurrent = 0;
            }
        }
        // Бонус-аптечка
        if (myTrigger.gameObject.name == "firstaid(Clone)")
        {
            if (healthPointCurrent + 100 <= healthPointTotal)
                healthPointCurrent += 100;
            else
                healthPointCurrent = healthPointTotal;
            Destroy(myTrigger.gameObject);
            Debug.Log("HealthBox removed: ");
        }
        // Бонус-дрель
        if (myTrigger.gameObject.name == "Drill(Clone)")
        {
            if (healthPointCurrent + 100 <= healthPointTotal)
                healthPointCurrent += 100;
            else
                healthPointCurrent = healthPointTotal;
            Destroy(myTrigger.gameObject);
            Debug.Log("Drill removed: ");
        }
    }

    // Произведение выстрела
    void fire()
    {
        // Нажали на пробел -> произвести выстрел
        if (Input.GetKey(KeyCode.Space) && timer == 0 && alive)
        {
            // Координаты дула
            Vector3 SpawnPoint = dulo.transform.position;
            Quaternion SpawnRoot = dulo.transform.rotation;
            // Quaternion SpawnRoot = bullet.transform.rotation;
            // Создание пули
            GameObject bulletForFire = PhotonNetwork.Instantiate(bullet.name, SpawnPoint, SpawnRoot) as GameObject;
            // Придание ей ускорения (Rigidbody берется у bullet)
            Rigidbody Run = bulletForFire.GetComponent<Rigidbody>();
            Run.AddForce(bulletForFire.transform.up * speedBullet, ForceMode.Impulse);
            Destroy(bulletForFire, 5);
            // Выставить кулдаун
            timer = cooldown;
        }
    }

    void FixedUpdate()
    {
        // Слушатель прослушивает нажатую кнопку
        if (!photonView.IsMine) return;
        fire();
    }

    [PunRPC]
    void isDied(){
        alive = false;
    }

    void Update()
    {
        // Debug.Log(photonView + "   " +photonView.Owner);
        // if (!photonView.IsMine) return;
        // Debug.Log(photonView.ViewID);
        // if (photonView.ViewID != 2001) return;
        if (!photonView.IsMine){
            if (!alive)
                photonView.RPC("isDied", RpcTarget.All);
                return;
        }

        if (timer > 0)
            timer -= Time.deltaTime;
        else
            timer = 0;

        if (alive && (PositionController.transform.position.y < 7.2f)){
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(-1 * Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(-1 * Vector3.up * rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
