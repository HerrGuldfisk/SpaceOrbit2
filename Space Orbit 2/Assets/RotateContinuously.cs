using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the object rotate continously around its own axis with a set speed
public class RotateContinuously : MonoBehaviour {
    public float angularVelocity = 0;

    private void Update() {
        if (angularVelocity != 0) {
            float degreesToRotateThisFrame = angularVelocity * Time.deltaTime;
            Vector3 rotationVector = new Vector3(0, 0, degreesToRotateThisFrame);
            transform.Rotate(rotationVector);
        }
    }
}