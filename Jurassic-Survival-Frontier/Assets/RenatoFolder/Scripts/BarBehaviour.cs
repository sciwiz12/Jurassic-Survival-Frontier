using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarManager : MonoBehaviour
{
    // Interface
    public Image barImage;
    // Values
    public float barMaxValue = 100;
    [SerializeField] private float decreasingOverTimeValue = 5;
 

    // Update is called once per frame
    void Update()
    {
        DecreaseOverTime();
    }

    void DecreaseOverTime(){
        barImage.fillAmount -= decreasingOverTimeValue/barMaxValue *Time.deltaTime ;
    }
}
