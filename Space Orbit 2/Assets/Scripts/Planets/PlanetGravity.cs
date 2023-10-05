using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PlanetGravity : GravityProvider
{
    PlanetSettings _planet;

    // Start is called before the first frame update
    void Start()
    {
        _planet = transform.root.GetComponent<PlanetSettings>();
    }

    public override void CalculateGravity()
    {
        foreach (Gravitable gravitable in _objectsInOrbit)
        {
            switch (_planet.GravityMode)
            {
                case PlanetSettings.PlanetGravityMode.Constant:
                    ApplyGravity(gravitable, _planet.GravityStrength);
                    break;

                case PlanetSettings.PlanetGravityMode.Linear:
                    ApplyGravity(gravitable, ((_planet.GravityFieldSize / 2) - Vector3.Distance(_planet.transform.position, gravitable.rootObject.transform.position)) * _planet.GravityStrength);
                    break;
            }
        }
    }

    public override void ApplyGravity(Gravitable objectInOrbit, float gravityStrength)
    {
        objectInOrbit.rb.AddForce((_planet.transform.position - objectInOrbit.transform.position) * gravityStrength / 10);
    }
}
