using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RedTank : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 1f;
    public float rotationSpeed = 100f;
    // Кулдаун пушки красного танка = 10 с.
    public float cooldown = 10f;
    public float timer = 0;
    // Префабы(ссылки на них) задаются в редакторе
    public GameObject bullet;
    public GameObject dulo;
    public float speedBullet = 6000f;

    // Здоровье игрока
    public float healthPointTotal = 500;
    public float healthPointCurrent = 500;
    public float countDamage = 200;
    public bool alive = true;
    private GameObject go;
    RedTank PositionController;

    // Локальное представление объекта для клиента
    private PhotonView photonView;
    public bool createBulletOnClient;

    // Передача выстрела
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if (stream.IsWriting){
            Debug.Log("ОТПРАВКА ДАННЫХ");
            stream.SendNext(createBulletOnClient);
        }else{
            Debug.Log("ПРИЕМ ДАННЫХ");
            createBulletOnClient = (bool) stream.ReceiveNext();
        }
    }

    // Первый кадр в отрисовке танка
    void Start()
    {
        go = GameObject.Find("RedTankMaus");
        // Найти контроллер позиции танка
        PositionController = go.GetComponent<RedTank>();
        // Инициализация локального предствления
        photonView = GetComponent<PhotonView>();
    }

    // Проверка на получение урона
    // Проверка на взятие бонуса
    void OnCollisionEnter(Collision myTrigger){
        if (myTrigger.gameObject.name == "fiveLevelBullet(Clone)")
        {
            healthPointCurrent -= 100;
            Debug.Log("Damaged RedTank : " + healthPointCurrent);
            PhotonNetwork.Destroy(myTrigger.gameObject);
            Debug.Log("Bullet removed: ");
            if (healthPointCurrent <= 0){
                alive = false;
                Debug.Log("RedAlive: " + alive);
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
        if (Input.GetKey(KeyCode.Space) && timer == 0 && alive && photonView.IsMine)
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
            createBulletOnClient = true;
        }
        // Для клиента принять выстрел и отобразить его у себя
        if (!photonView.IsMine && createBulletOnClient){
            Vector3 SpawnPoint = dulo.transform.position;
            Quaternion SpawnRoot = dulo.transform.rotation;
            //GameObject bulletForFire = Instantiate(bullet, SpawnPoint, SpawnRoot) as GameObject;
            // Придание ей ускорения (Rigidbody берется у bullet)
            //Rigidbody Run = bulletForFire.GetComponent<Rigidbody>();
            //Run.AddForce(bulletForFire.transform.up * speedBullet, ForceMode.Impulse);
            //Destroy(bulletForFire, 5);
            // Выставить кулдаун
            timer = cooldown;
            createBulletOnClient = false;
        }
    }

    void FixedUpdate()
    {
        // Слушатель прослушивает нажатую кнопку 
        fire();
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            timer = 0;
        
            // PhotonNetwork.IsMasterClient
        if (alive && (PositionController.transform.position.y < 7.1f) && photonView.IsMine){
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
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
