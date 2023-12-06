using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndMoveTowardsTarget : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _moveSpeed;

    private Vector2 _direction;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<Flock>())
        {
            this.enabled = false;
            return;
        }

        if (FindObjectOfType<BasicMovement>() != null)
        {
            target = FindObjectOfType<BasicMovement>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        _direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, _moveSpeed * Time.deltaTime);
    }
}
