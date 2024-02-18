using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    public float speed = 2f;
    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
    }
}
