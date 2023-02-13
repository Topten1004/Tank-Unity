using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankSelectionScroolBar : MonoBehaviour
{
    public RectTransform _transform;
    public RectTransform _firstTransform;
    public int changeValue = 80;
   
    private void Awake()
    {
        _transform = this.GetComponent<RectTransform>();
        Debug.Log(_transform.localPosition.y);
    }
    private void Update()
    {
        if(_transform.localPosition.y >= -80)
        {
           // Debug.Log("İlk pozisyon: " + _transform.localPosition.y);
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                _transform.localPosition += new Vector3(0, -changeValue, 0);
                Debug.Log(_transform.localPosition.y);
            }
        }
        else
        {
            _transform.localPosition = new Vector3(_transform.localPosition.x, 122.5f);
        }
        
    }

    public Vector3 GetSelected()
    {
        return _transform.localPosition;
    }
}
