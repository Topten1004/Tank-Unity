using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

    float _horizontalInput, _verticalInput;
    [SerializeField]
    private float _movementSpeed = 5f;
    private bool ISFROZEN = false;

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(ISFROZEN != true)
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
            if (_verticalInput != 0 || _horizontalInput != 0)
            {
                this.transform.up = new Vector3(_horizontalInput, _verticalInput);
                this.transform.Translate(new Vector3(_horizontalInput, _verticalInput, 0) * _movementSpeed * Time.deltaTime, Space.World);
            }
        }

    }


    public void FrozeMovement()
    {
        ISFROZEN = true;
    }

    public void AddSpeed()
    {
        _movementSpeed += 10f;
    }

    public void TakeSpeed()
    {
        _movementSpeed -= 10f;
    }

    
}
