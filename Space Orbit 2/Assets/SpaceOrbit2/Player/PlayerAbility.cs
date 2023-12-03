using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    public float BaseAbilityCD { get; set; }
    public float CurrentCD { get; set; }

}
