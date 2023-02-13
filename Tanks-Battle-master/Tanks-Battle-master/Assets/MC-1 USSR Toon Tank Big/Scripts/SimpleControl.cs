using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleControl : MonoBehaviour {
	
	//all left wheels
    public GameObject[] LeftWheels;
	//all right wheels
    public GameObject[] RightWheels;

    public GameObject LeftTrack;

    public GameObject RightTrack;

    public float wheelsSpeed = 2f;
    public float tracksSpeed = 2f;
    public float forwardSpeed = 1f;
    public float rotateSpeed = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Keyboard moves =======================================//
        //Forward Move
        if (Input.GetKey(KeyCode.W))
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f,Time.deltaTime*tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);

            //Move Tank

            transform.Translate(new Vector3(0f, 0f, forwardSpeed));

        }
        //Back Move
        if (Input.GetKey(KeyCode.S))
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //Move Tank
            transform.Translate(new Vector3(0f, 0f, -forwardSpeed));
        }
        //On Left
        if (Input.GetKey(KeyCode.A))
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //Rotate Tank
            transform.Rotate(new Vector3(0f,-rotateSpeed,0f));
        }
        //On Right
        if (Input.GetKey(KeyCode.D))
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
            //Rotate Tank
            transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
        }
		//=======================================//
                                                   



    }
}
