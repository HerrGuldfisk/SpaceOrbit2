using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SonarWave : MonoBehaviour {
    public float ChargeLeft { get; set; } = 10f;
    private bool _isActive = false;
    private Vector3 _targetSize;
    private float _startCharge;

    public delegate void StarPositionDetected(Vector2 position);

    public static event StarPositionDetected OnStarPositionDetected;

    void Update() {
        if (_isActive == false) {
            return;
        }

        if (ChargeLeft < 0) {
            Destroy(gameObject);
        }

        float nextScaleValue = Mathf.Lerp(1, _targetSize.x,
            Mathf.Sin(((_startCharge - ChargeLeft) / _startCharge * Mathf.PI) / 2));
        transform.localScale = new Vector3(nextScaleValue, nextScaleValue, nextScaleValue);
        ChargeLeft -= Time.deltaTime / 2;
    }

    public void BeginSonar(float power) {
        ChargeLeft = power;
        _startCharge = power;
        _targetSize = new Vector3(300f * power, 300f * power, 300f * power);
        _isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Star")) {
            OnStarPositionDetected?.Invoke(collision.gameObject.transform.position);
        }
    }
}