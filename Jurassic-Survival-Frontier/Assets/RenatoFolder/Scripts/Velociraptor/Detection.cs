using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public bool targetDetected { get; private set; } = false;
    public Transform target { get; private set; } = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject.transform;
            targetDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetDetected = false;
        }
    }
}
