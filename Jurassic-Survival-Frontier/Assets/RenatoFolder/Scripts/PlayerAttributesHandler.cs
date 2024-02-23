using UnityEngine;

public class PlayerAttributesHandler : MonoBehaviour
{
    public static PlayerAttributesHandler instance { get; private set; }
    public float parameterValue;

    private void Awake()
    {
        if (instance == null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public float ParamaterReduction(float parameterValue, bool isActionPerformed, float decreaseValue= 1)
    {
        this.parameterValue = parameterValue;
        parameterValue -= decreaseValue;
        return decreaseValue;
    }

    public float ParameterIncrease(float parameterValue, bool isActionPerformed, float increaseValue = 1)
    {
        this.parameterValue = parameterValue;
        parameterValue += increaseValue;
        return increaseValue;
    }
}
