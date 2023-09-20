using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectWhenTaggedIsClose : MonoBehaviour
{
    [SerializeField] private string tagToCheck;
    [SerializeField] private GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[ActivateObjectWhenTaggedIsClose] object entered: " + other.tag);


        if (other.CompareTag(tagToCheck))
        {
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("[ActivateObjectWhenTaggedIsClose] object exited: " + other);

        if (other.CompareTag(tagToCheck))
        {
            objectToActivate.SetActive(false);
        }
    }
}
