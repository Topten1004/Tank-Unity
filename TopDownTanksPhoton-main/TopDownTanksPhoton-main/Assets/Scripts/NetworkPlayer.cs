using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

namespace PUN_Tanks
{
    public class NetworkPlayer : MonoBehaviourPun, IPunObservable
    {
        [Header("Movement")]
        Rigidbody rb;
        [SerializeField] float movSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] Transform cannonPivot;

        [SerializeField] Bullet bulletPrefab;
        [SerializeField] HealingZone healingZonePrefab;
        [SerializeField] Transform cannonMuzzle;
        [SerializeField] GameObject ExplosiveRange;
        

        [Header("HUD")]
        [SerializeField] TextMeshProUGUI txt_PlayerName;
        [SerializeField] Image Health_Bar;

        [SerializeField] float maxHealth;
        [SerializeField] float currHealth;

        bool isOnCoolDown;
        public bool playerReady;
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(currHealth);
            }
            else
            {
                currHealth = (float)stream.ReceiveNext();
            }
            UpdateHealthBar();

            
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            txt_PlayerName.text = photonView.Owner.NickName;
            if (photonView.IsMine)
            {
                currHealth = 50;
            }

            UpdateHealthBar();

            UpdateColor();

            
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                Vector3 mov = new Vector3(rb.position.x + h * movSpeed * Time.deltaTime, rb.position.y, rb.position.z + v * movSpeed * Time.deltaTime);
                rb.MovePosition(mov);

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    cannonPivot.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    cannonPivot.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    photonView.RPC(nameof(Shoot), RpcTarget.All, cannonMuzzle.position, cannonPivot.rotation);
                }
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    photonView.RPC(nameof(SpawnHealingZone), RpcTarget.All);
                }
            }

        }

        [PunRPC]

        public void Shoot(Vector3 spawnPos, Quaternion rot)
        {
            Bullet b = Instantiate(bulletPrefab, spawnPos, rot);
            b.ownerActorNum = photonView.OwnerActorNr;
            
        }

        [PunRPC]
        public void SpawnHealingZone()
        {
            if(!isOnCoolDown)
            {
                HealingZone hz = Instantiate(healingZonePrefab, transform.position, transform.rotation);
                hz.ownerActorNum = photonView.OwnerActorNr;
                //hz.transform.parent = gameObject.transform;
                isOnCoolDown = true;
                StartCoroutine(HealingZoneCoolDown());

               
            }
        }

        IEnumerator HealingZoneCoolDown()
        {
            yield return new WaitForSeconds(10f);
            isOnCoolDown = false;
        }

        public void ApplyDamage(float damage)
        {

            if (!photonView.IsMine)
            {
                Debug.LogWarning("eh yasta?");
                return;
            }
            currHealth -= damage;
        }

        public void Heal(float buff)
        {
            if (photonView.IsMine)
            {
                if (currHealth < maxHealth)
                {
                    currHealth += buff * Time.deltaTime;
                    Mathf.Clamp(currHealth, 0, 100);
                }
            }
        }

        public void UpdateHealthBar()
        {
            Health_Bar.fillAmount = currHealth / maxHealth;
        }

        void UpdateColor()
        {
            
            float pColor_r = (float)photonView.Owner.CustomProperties["Color_r"];
            float pColor_g = (float)photonView.Owner.CustomProperties["Color_g"];
            float pColor_b = (float)photonView.Owner.CustomProperties["Color_b"];
            Color pColor = new Color(pColor_r, pColor_g, pColor_b);
            GetComponent<MeshRenderer>().material.color = pColor;
        }
        
    }
}
