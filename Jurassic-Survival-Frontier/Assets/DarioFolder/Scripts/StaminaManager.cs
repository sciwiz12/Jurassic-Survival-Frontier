using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    #region Variables
    [Header("VARIABLES")]
    [SerializeField] private float currentStamina;
    [SerializeField] private float currentHunger;
    [SerializeField] private float currentThirst;
    [SerializeField] private float sleep = 100;
    [SerializeField] private float timeAwake;
    private float maxStamina = 100;
    private float maxHunger = 100;
    private float maxThirst = 100;
    private float reduction = 1f;
    public Image staminaImage;
    private LightingManager lightingManager;

    #region Properties
    private float staminaCost;

    public float StaminaCost
    {
        get { return staminaCost; }
        set { staminaCost = value; }
    }

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

    public bool isActionPerformed = false;
    public bool isSleeping;
    #endregion

    private void Start()
    {
        lightingManager = FindObjectOfType<LightingManager>();
        currentStamina = maxStamina;
        currentHunger  = maxHunger;
        currentThirst  = maxThirst;
    }

    private void Update()
    {
        sleep = Mathf.Clamp(sleep, 0, 100);
        timeAwake = Mathf.Clamp(timeAwake, 0, 18f);
        timeAwake += Time.deltaTime * lightingManager.timerDay;
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
        if (timeAwake > 14)
        {
            NeedToSleep();
        }
        if (isSleeping)
        {
            accuracy++;
            sleep += Time.deltaTime * 2 * lightingManager.timerDay;
            timeAwake -= Time.deltaTime * 2 * lightingManager.timerDay;
        }
    }

    #region METHODS

    private void NeedToSleep()
    {
        if (!isSleeping && timeAwake > 14)
        {
            accuracy--;
            sleep -= Time.deltaTime * lightingManager.timerDay;
            if (sleep < 75)
            {
                Debug.Log("You are starting to feel tired.");
            }
            if (sleep < 50)
            {
                Debug.Log("You need to sleep.");
            }
        }
    }

    /// <summary>
    /// Call this function when an action is performed with the cost of stamina as argument.
    /// Can be call for the run too. Make it the staminaCost very low and calling repeatly this function.
    /// </summary>
    public float StaminaDecrease(float staminaCost = 5)
    {
        currentStamina -= staminaCost;
        isActionPerformed = true;
        return staminaCost;
    }

    public float HungerDecrease(float reduction = 1)
    {
        currentHunger -= reduction;
        return currentHunger;
    }    
    
    public float ThirstDecrease(float reduction = 1)
    {
        currentThirst -= reduction;
        return currentThirst;
    }

    #endregion
}
