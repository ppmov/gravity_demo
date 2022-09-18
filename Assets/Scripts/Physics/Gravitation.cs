using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 9.807f;
    private readonly HashSet<Rigidbody> bodies = new HashSet<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
            bodies.Add(other.attachedRigidbody);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
            bodies.Remove(other.attachedRigidbody);
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody otherBody in bodies)
        {
            Vector3 direction = transform.position - otherBody.position;
            
            if (direction.magnitude > 1) // без условия тело никогда не остановится
                direction = direction.normalized;

            otherBody.AddForce(acceleration * otherBody.mass * direction);
        }
    }
}
