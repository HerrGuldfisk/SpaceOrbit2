using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAdjuster : MonoBehaviour
{
    private GameObject _planet;
    [SerializeField] private float _scale = 1;
    public GravityHandling gravityField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if (_scale > 0)
        {
            Vector3 sizeScaleVector = new Vector3(_scale, _scale, _scale);

            transform.localScale = sizeScaleVector;

            ScaleGravityFIeld(_scale);
        }
    }

    public void ScaleGravityFIeld(float scale)
    {
        gravityField.scale = scale;
    }
}
