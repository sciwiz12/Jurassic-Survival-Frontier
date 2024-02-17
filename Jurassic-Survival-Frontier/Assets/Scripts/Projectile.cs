using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}