using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleGravity : MonoBehaviour
{

    private List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();

    [SerializeField] private float m_Gravity = 5f;

    void Start()
    {
        m_Gravity = Random.Range(3f, 8f);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        foreach (Rigidbody rb in m_Rigidbodies)
        {
            rb.AddForce((transform.position - rb.position) * m_Gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TryGetComponent(out Rigidbody rb))
        {
            m_Rigidbodies.Add(rb);
        }

        
    }
}
