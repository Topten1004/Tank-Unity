using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace PUN_Tanks
{
    public class Bullet : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float speed, damage;
        [SerializeField] GameObject Player;
        Rigidbody rb;
        public int ownerActorNum { get; set; }
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            rb.velocity = transform.forward * speed;

        }

        private void OnTriggerEnter(Collider other)
        {

            if(other.CompareTag("Player"))
            {
                NetworkPlayer player = other.GetComponent<NetworkPlayer>();
                if (player.photonView.IsMine)
                {
                    player.ApplyDamage(damage);
                }
            } else if (other.CompareTag("HealingZone"))
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}
