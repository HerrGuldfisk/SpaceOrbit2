using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SnekBehaviour;
using static UnityEngine.GraphicsBuffer;

public class SeePlayerScript : MonoBehaviour
{
    public CircleCollider2D _visionCollider;
    private float _visionRadius = 10f;

    public SnekBehaviour snek;

    private Vector3 _posWhenPlayerWasSeen;

    private void Start()
    {
        _visionCollider.radius = _visionRadius;
        //_visionCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D col = collision.gameObject.GetComponent<Collider2D>();

        // is it seeing the player?
        if (col.gameObject.CompareTag("Player"))
        {
            _posWhenPlayerWasSeen = snek.transform.position;
            snek.PlayerWasSeen(col);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D col = collision.gameObject.GetComponent<Collider2D>();

        if (col.gameObject.CompareTag("Player"))
        {
            snek.PlayerWasLost(_posWhenPlayerWasSeen);
        }
    }
}
