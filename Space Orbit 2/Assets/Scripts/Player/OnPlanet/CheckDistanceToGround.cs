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
                // TODO: Currently getting scale from first shild. Make more robust
                RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + Vector2.down * transform.GetChild(0).localScale.y, Vector2.down, MaxMeasureDistance, LayerMask.GetMask("Ground"));

                if (hit.collider != null)
                {
                    DistanceToGround = hit.distance;
                    UpdateDistanceText();
                }
            }
        }

        // Fix a proper solution later
        public void UpdateDistanceText()
        {
            if (textField == null) { return; }

            textField.text = $"{DistanceToGround:0.0}";
        }
    }   
}

