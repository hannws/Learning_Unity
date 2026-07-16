using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        Rigidbody rb = collision.rigidbody;

        ContactPoint contact = collision.contacts[0];

        Vector3 direction = contact.normal;

        rb.AddForce(direction * bounceForce, ForceMode.Impulse);
    }
}