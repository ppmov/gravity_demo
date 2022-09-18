using System.Collections.Generic;
using UnityEngine;

public class Repulsion : MonoBehaviour
{
    [SerializeField]
    private float force;
    [SerializeField]
    private Material coloredMaterial;

    private Dictionary<Collider, MeshRenderer> colliderMeshPairs;

    private void Start()
    {
        // ���������� MeshRenderer ���� ����������� � �������, ���� �� ������ ���������� GetComponent
        Collider[] colliderArray = GetComponentsInChildren<Collider>();
        colliderMeshPairs = new Dictionary<Collider, MeshRenderer>(colliderArray.Length);

        foreach (Collider collider in colliderArray)
            colliderMeshPairs.Add(collider, collider.GetComponent<MeshRenderer>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null)
            return;

        ContactPoint contact = collision.GetContact(0);

        // ������
        Vector3 direction = (contact.point - transform.position).normalized;
        collision.rigidbody.AddForceAtPosition(direction * force, contact.point, ForceMode.Impulse);

        // ������� ��� �������
        ColorCollider(contact.thisCollider);

        // ���� ������������
        Counter.AddContact(contact);
    }

    private void ColorCollider(Collider collider)
    {
        MeshRenderer mesh = colliderMeshPairs[collider];

        if (mesh != null)
            mesh.material = coloredMaterial;
    }
}
