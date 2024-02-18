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

    [SerializeField] private float staminaRecharge = 2f;
    public float StaminaRecharge
    {
        get { return staminaRecharge; }
        set { staminaRecharge = value; }
    }

    [SerializeField] public bool isActionPerformed = false;

    [HideInInspector] public float staminaCost;
    private float malusSleep = 1f;
    public bool needToSleep;
    private LightingManager lightingManager;
    #endregion


    private void Start()
    {
        lightingManager = FindObjectOfType<LightingManager>();
        currentStamina = maxStamina;
    }

    private void Update()
    {
        staminaImage.fillAmount = currentStamina / 100;
        if (!isActionPerformed && currentStamina < 100)
        {
            currentStamina += staminaRecharge * Time.deltaTime;
        }

        if(currentStamina > 100)
        {
            currentStamina = 100;
        }

        if (needToSleep)
        {
            maxStamina -= malusSleep * Time.deltaTime;

            if (maxStamina < 0)
                maxStamina = 0;

            currentStamina = maxStamina;
        }
        else
        {
            maxStamina = 100;
            currentStamina = maxStamina;
        }

        if (lightingManager.isNight)
        {
            needToSleep = true;
        }
        else
            needToSleep = false;
    }

    public float StaminaReduceAction(float staminaCost = 5)
    {
        currentStamina -= staminaCost;
        isActionPerformed = true;
        return staminaCost;
    }
}
