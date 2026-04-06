using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float autoDestroyTime = 2f;
    void Start()
    {
        Destroy(gameObject, autoDestroyTime);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Escenario"))
        {
            Destroy(gameObject);
        }
    }
}
