using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSegmentRotation : MonoBehaviour
{
    public float speed;

    private Vector2 dir;
    public Transform target;

    void Update()
    {
        if (target == null) return;

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
