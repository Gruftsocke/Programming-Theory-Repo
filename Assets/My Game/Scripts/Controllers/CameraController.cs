/*********************************************************
 * Fielname: CameraController.cs
 * Project : SchnabelSoftware.MyGame
 * Date    : 01.11.2022
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

namespace SchnabelSoftware.MyGame.Controllers
{
    /// <summary>
	/// 
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		[Header("Movement Properties")]
		[SerializeField] private float turnSpeed = 400f;
		[SerializeField] private float moveSpped = 5f;

		[Header("Mouse Properties")]
		[Range(0f, 1f)]
		[SerializeField] private float mouseSensitvity = .5f;

		private float rotateY = 0f;

		private void Awake()
		{
			// get start rotation.
			rotateY = transform.eulerAngles.y;
		}

		private void Update()
		{
			//float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			if (v != 0f)
			{
				transform.Translate(Vector3.forward * moveSpped * Time.deltaTime * v);
			}

			if (Input.GetMouseButton(2))
			{
				float mouseX = Input.GetAxis("Mouse X") * mouseSensitvity;

				rotateY += turnSpeed * Time.deltaTime * mouseX;
				
				if (rotateY > 360f)
					rotateY -= 360f;
				else if (rotateY < 0f)
					rotateY += 360f;
			}

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * rotateY), Time.deltaTime * 3f);
		}
	}
}
