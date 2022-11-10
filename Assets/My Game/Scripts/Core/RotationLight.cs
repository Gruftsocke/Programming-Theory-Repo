/*********************************************************
 * Dateiname: RotationLight.cs
 * Projekt  : SchnabelSoftware.
 * Datum    : 
 *
 * Author   : Daniel Schnabel
 * E-Mail   : info@schnabel-software.de
 *
 * Zweck    : 
 *
 * © Copyright by Schnabel-Software 2009-2022
 */
using UnityEngine;

namespace SchnabelSoftware.MyGame.Core
{
    /// <summary>
	/// 
	/// </summary>
	public class RotationLight : MonoBehaviour
	{
        [SerializeField] private float turnSpeed = 25f;
        [SerializeField] private new Light light = null;
        [SerializeField] private Material lightMaterial = null;

		public bool Enabled { get; set; }

        private void Awake()
        {
            GetComponent<MeshRenderer>().material = new Material(lightMaterial);
            lightMaterial = GetComponent<MeshRenderer>().material;

            light.enabled = Enabled;
            if (Enabled)
                lightMaterial.EnableKeyword("_EMISSION");
            else
                lightMaterial.DisableKeyword("_EMISSION");
        }

        private void Update()
        {
            if (!Enabled)
            {
                if (light.enabled)
                {
                    light.enabled = false;
                    lightMaterial.DisableKeyword("_EMISSION");
                }
                return;
            }

            if (!light.enabled)
            {
                light.enabled = true;
                lightMaterial.EnableKeyword("_EMISSION");
            }

            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }
}
