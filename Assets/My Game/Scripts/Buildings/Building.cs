/*********************************************************
 * Filename: Building.cs
 * Project : SchnabelSoftware.MyGame
 * Date    : 04.11.2022
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
using UnityEngine.AI;

namespace SchnabelSoftware.MyGame.Buildings
{
    /// <summary>
	/// 
	/// </summary>
	public abstract class Building : MonoBehaviour
	{
		[Header("Building Properties")]
		[SerializeField] protected float stopDistance = 2f;

		public float StopDistance => stopDistance;
		public abstract Vector3[] GetWaypoints();
		public virtual Vector3 GetPosition() => transform.position;
	}
}
