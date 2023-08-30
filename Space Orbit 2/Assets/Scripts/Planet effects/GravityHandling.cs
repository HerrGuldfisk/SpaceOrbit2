using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandling : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public float scale;
    private float _planetRadius = 0.5f;

    // Borrowing from simple gravity script
    //--------------------------------------------------------------------------------------------------------------------
    private List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();

    [SerializeField] private float m_Gravity = 5f;

    [SerializeField] private float m_GravityFieldSize = 3f;

    void Start()
    {
        m_Gravity = Random.Range(3f, 8f);
        m_GravityFieldSize = Random.Range(3f, 3f);

        transform.localScale = new Vector3(m_GravityFieldSize, m_GravityFieldSize, 1);
    }

    void FixedUpdate()
    {
        foreach (Rigidbody rb in m_Rigidbodies)
        {
            rb.AddForce((transform.position - rb.position).normalized * m_Gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            Debug.Log("Starship enter");
            m_Rigidbodies.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            Debug.Log("Starship leave");
            rb.AddForce(other.transform.GetChild(0).transform.forward * 100);
            m_Rigidbodies.Remove(rb);
        }
    }

    //-----------------------------------------------------------------------------------------------

    private void OnValidate()
    {
        sphereCollider.radius = (_planetRadius + 0.2f) * scale;
    }
}
