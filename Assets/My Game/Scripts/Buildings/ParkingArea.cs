/*********************************************************
 * Filename: ParkingArea.cs
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SchnabelSoftware.MyGame.Buildings
{
    /// <summary>
	/// 
	/// </summary>
	public class ParkingArea : Building
	{
		[Header("Parking Area Properties")]
		[SerializeField] private Transform[] alignmentPoint = null;

		private Vector3[] waypoints = null;

		private void Awake()
		{
			List<Vector3> data = new List<Vector3>();
			foreach (var item in alignmentPoint)
			{
				data.Add(item.position);
			}

			waypoints = data.ToArray();
		}

		public override Vector3[] GetWaypoints()
		{
			return waypoints;
		}
	}
}
