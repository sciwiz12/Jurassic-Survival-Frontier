using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDDOL<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static T Instance => instance;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
