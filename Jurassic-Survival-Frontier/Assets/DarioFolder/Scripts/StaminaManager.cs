using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    #region Variables
    [Header("VARIABLES")]
    public Image staminaImage;

    private float maxStamina = 100;
    [SerializeField] private float currentStamina;

    #region Properties
    [SerializeField] private float staminaRecharge = 2f;
    public float StaminaRecharge
    {
        get { return staminaRecharge; }
        set { staminaRecharge = value; }
    }

    [SerializeField] private float accuracy;
    public float Accuracy
    {
        get { return accuracy; }
        set { accuracy = value; }
    }
    #endregion

    [SerializeField] public bool isActionPerformed = false;

    [HideInInspector] public float staminaCost;
    private float malusSleep = 1f;
    public bool isSleeping;
    [SerializeField] private float sleep = 100;
    [SerializeField] private float timeWakeUp;

    #endregion

    private void Start()
    {
        currentStamina = maxStamina;
        timeWakeUp = 0;
    }

    private void Update()
    {
        staminaImage.fillAmount = currentStamina / 100;
        if (!isActionPerformed && currentStamina < 100)
        {
            currentStamina += staminaRecharge * Time.deltaTime;
        }

        if (currentStamina > 100)
        {
            currentStamina = 100;
        }
        else if (currentStamina < 0)
            currentStamina = 0;
        if(timeWakeUp > 14)
        {

        }
        //if (needToSleep)
        //{
        //    maxStamina -= malusSleep * Time.deltaTime;
        //    currentStamina = maxStamina;

        //}

        //if(currentStamina < currentStamina/3 && !isSleeping)
        //{
        //    accuracy--;
        //}
        //else
        //{
        //    accuracy++;
        //}
    }

    #region METHODS
    /*private void SleepCycle()
    {
        if (NeedToSleep())
        {
            accuracy--;
            maxStamina -= malusSleep * Time.deltaTime;
            currentStamina = maxStamina;
        }
        else //check if go to bed
        {
            maxStamina += malusSleep * 2 * Time.deltaTime;
            timeWakeUp = 0; //until the animation of sleeping end
            accuracy++;
            sleep--;
        }
    }*/

    private bool NeedToSleep()
    {
        timeWakeUp += 1 * Time.deltaTime;
        if(timeWakeUp > 18)
        {
            sleep++;
            if (sleep > 50)
            {
                Debug.Log("You are start ecc.");
            }
            if (sleep > 75)
            {
                Debug.Log("You need to sleep");
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public float StaminaReduceAction(float staminaCost = 5)
    {
        currentStamina -= staminaCost;
        isActionPerformed = true;
        return staminaCost;
    }

    #endregion
}
