using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

namespace Planets
{
    public class GeneralPlanetSettings : MonoBehaviour
    {

        public static GeneralPlanetSettings Instance;

        
        private void Awake()
        {
            if (Instance == null || Instance == this)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        // public GlobalGravityModeEvent GlobalGravityModeEvent;

        [Header("Gravity settings")]
        [SerializeField]public GravityMode GravityMode = GravityMode.CutOff;

        /*
        public GravityMode GravityMode
        {
            get
            {
                return gravityMode;
            }
            set
            {
                GlobalGravityModeEvent?.Invoke(value);
                gravityMode = value;
            }
                
                
        }
        */

        [Tooltip("If the players ship should get a boost when exiting atmospheres")]
        public bool exitBoost = false;

        [Tooltip("Multiplier for all planets gravitational pull area")]
        public float gravityDistance = 1f;


    }

    public enum GravityMode
    {
        CutOff,
        Linear,
        Log,
    }
}

