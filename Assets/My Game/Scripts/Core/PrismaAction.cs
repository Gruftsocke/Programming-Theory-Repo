/*********************************************************
 * Filename: PrismaAction.cs
 * Project : SchnabelSoftware.MyGame
 * Date    : 02.11.2022
 *
 * Author  : Daniel Schnabel
 * E-Mail  : info@schnabel-software.de
 *
 * Purpose : Programming-Theory-Repo
 *		     It shows the theory of the four pillars of OOP (object-oriented programming):
 *		     1. Abstraction
 *		     2. Inheritance
 *		     3. Polymorphism
 *		     4. Encapsulation.
 *
 * © Copyright by Schnabel-Software 2009-2022
 */
using UnityEngine;

namespace SchnabelSoftware.MyGame.Core
{
    /// <summary>
	/// 
	/// </summary>
	public class PrismaAction : MonoBehaviour
	{
		[SerializeField] private float turnSpeed = 25f;
		[SerializeField] private float bounceTimeInterval = 1f;
		[SerializeField] private float bounceLength = .4f;

		private float elapsedTime = 1f;
		private Vector3 startPosition = default;
		private bool upwards = false;
		private Vector3 fromPos = default;
		private Vector3 toPos = default;
		private Transform childTransform = default;

		private void Start()
		{
			childTransform = transform.GetChild(0);
			startPosition = childTransform.localPosition;
		}

		private void OnEnable()
		{
			upwards = false;
			elapsedTime = 1f;
		}

		private void Update()
		{
			elapsedTime += Time.deltaTime / bounceTimeInterval;

			if (elapsedTime <= 1f)
			{
                childTransform.localPosition = Vector3.Lerp(fromPos, toPos, elapsedTime);
			}
			else
			{
				elapsedTime = 0f;
				if (upwards)
				{
					upwards = false;
					fromPos = startPosition + (Vector3.up * bounceLength);
					toPos = startPosition;
				}
				else
                {
                    upwards = true;
                    fromPos = startPosition;
                    toPos = startPosition + (Vector3.up * bounceLength);
                }
            }

            childTransform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.Self);
		}
	}
}
