using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;

    public void Shoot()
    {
        Debug.Log("PISTOLA DISPARA");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (firePoint != null)
        {
            // Visualizar el cañón
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(firePoint.position, 0.1f);

            // Visualizar la trayectoria del disparo
            Gizmos.color = Color.cyan;
            Vector3 trajectory = firePoint.forward * bulletSpeed;
            Gizmos.DrawRay(firePoint.position, trajectory);
        }
    }
}
