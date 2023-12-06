using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovementScript : MonoBehaviour
{
    public bool runaway = false;

    [SerializeField] private float _destroyTimer = 6f;

    private Vector2 _velocity;
    private Vector2 _direction;
    private float _speed = 24f;

    // Update is called once per frame
    void Update()
    {
        if (!runaway) return;

        // Maybe translate instead or something idk
        transform.position = (Vector2)transform.position + _velocity * Time.deltaTime;
    }

    public void Setup(Vector2 givenDir)
    {
        SetNewVelocity(givenDir);
        runaway = true;
        StartCoroutine(DestroyAfterDelay(_destroyTimer));
    }

    public void SetNewVelocity(Vector2 newDir)
    {
        _direction = newDir;
        _velocity = _direction * _speed;
    }
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null) return;
        if (collision.collider.CompareTag("Asteroid")) return;

        gameObject.SetActive(false);
    }
}
