using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public GameObject _movementBonusPrefab;
    public float timeLeft = 100.0f;
    public bool stop = true;

    private float minutes;
    private float seconds;
    private bool ISMOVEMENTSPEEDACTİVE = false;
    public Text bonusTimeText;
    public GameObject BONUSPANEL_;


    public void startTimer(float from)
    {
        stop = false;
        timeLeft = from;
        Update();
        StartCoroutine(updateCoroutine());
    }


    private void Update()
    {

        if (stop) return;
        timeLeft -= Time.deltaTime;

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            stop = true;
            minutes = 0;
            seconds = 0;
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(this.gameObject.tag == "MovementBonus")
            {
                //taking effect add
                Debug.Log("I'm giving movement bonus!");
                GiveMovementBonus();
                Destroy(_movementBonusPrefab);
                collision.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                //BONUSPANEL_.SetActive(true);
                //startTimer(timeLeft);
                //ISMOVEMENTSPEEDACTİVE = true;
            }
        }
    }

    void GiveMovementBonus()
    {
        PlayerMovement movementScript = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        if (movementScript)
        {
            Debug.Log("I've found the movement script");
            movementScript.AddSpeed();
        }
    }

    private IEnumerator updateCoroutine()
    {
        while (!stop)
        {
            bonusTimeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }


}
