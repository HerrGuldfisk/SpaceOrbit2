using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

namespace OnPlanet
{
    public class CheckDistanceToGround : MonoBehaviour
    {
        public bool IsRunning = true;
        public TextMeshProUGUI textField = null;

        public float DistanceToGround { get; private set; }

        public float MaxMeasureDistance { get; private set; } = 100;

        void Update()
        {
            if (IsRunning)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + Vector2.down, Vector2.down, MaxMeasureDistance, LayerMask.GetMask("Ground"));

                if (hit.collider != null)
                {
                    DistanceToGround = hit.distance;
                    UpdateDistanceText();
                }
            }
        }

        // Fix aproper solution later
        public void UpdateDistanceText()
        {
            if (textField == null) { return; }

            textField.text = $"{DistanceToGround:0.0}";
        }
    }   
}

