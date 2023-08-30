using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAdjuster : MonoBehaviour
{
    private GameObject planet;
    [SerializeField] private float scale = 1;
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
        Vector3 sizeScaleVector = new Vector3(scale, scale, scale);

        transform.localScale = sizeScaleVector;

        ScaleGravityFIeld(scale);
    }

    public void ScaleGravityFIeld(float scale)
    {
        gravityField.scale = scale;
    }
}
