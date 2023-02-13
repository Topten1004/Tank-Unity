using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUN_Tanks
{
    public class HealingZone : MonoBehaviour
    { // Start is called before the first frame update
        [SerializeField] float buffPercentage;
        

        public int ownerActorNum { get; set; }
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {

           
            StartCoroutine(HealingZoneTime());
        }

        IEnumerator HealingZoneTime()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                NetworkPlayer player = other.GetComponent<NetworkPlayer>();

                if (player.photonView.OwnerActorNr == ownerActorNum)
                {
                    player.Heal(buffPercentage);
                    player.UpdateHealthBar();
                }

            }
        }
    }

}
