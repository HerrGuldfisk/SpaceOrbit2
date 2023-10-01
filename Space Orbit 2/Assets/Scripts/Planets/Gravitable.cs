using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitable : MonoBehaviour
{
    [HideInInspector] public GameObject rootObject;

    private void Start()
    {
        rootObject = transform.root.gameObject;
    }
}
