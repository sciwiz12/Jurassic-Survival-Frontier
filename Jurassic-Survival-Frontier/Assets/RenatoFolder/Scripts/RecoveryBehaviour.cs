using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryBehaviour : MonoBehaviour
{
    private BarManager barManager;
    private Image barImage;
    private float barMaxValue;

    [SerializeField] private float recoveryValue = 10f ;

    [SerializeField] private KeyCode keyCode;

    // Start is called before the first frame update
    void Start()
    {
        barManager = GetComponent<BarManager>();
        barImage = barManager.barImage;
        barMaxValue = barManager.barMaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        Recovery();
    }

    void Recovery()
    {
        if(Input.GetKeyDown(keyCode))
        {
            barImage.fillAmount += recoveryValue/barMaxValue;
        }
    }
}
