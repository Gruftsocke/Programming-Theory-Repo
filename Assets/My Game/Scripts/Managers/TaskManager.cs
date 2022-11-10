/*********************************************************
 * Filename: TaskManager.cs
 * Project : SchnabelSoftware.MyGame
 * Date    : 05.11.2022
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
using SchnabelSoftware.MyGame.Buildings;
using SchnabelSoftware.MyGame.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SchnabelSoftware.MyGame.Managers
{
    /// <summary>
	/// 
	/// </summary>
	public class TaskManager : MonoBehaviour
	{
		public static TaskManager Current { get; private set; }

		[SerializeField] private List<Task> taskList = new List<Task>();
		[SerializeField] private List<Forklift> vehicles = new List<Forklift>();
		[SerializeField] private List<Building> parkingAreas = new List<Building>();

		private void Awake()
		{
			if (Current)
			{
				Destroy(gameObject);
				return;
			}

			Current = this;
			// DontDestroyOnLoad(gameObject); // <<< for later
		}

		public Task GetTask(Building owner)
		{
			return taskList.Find(p => p.retrieveFrom == owner);
		}

		public void RequestVehicle(Building client)
		{
			foreach (var vehicle in vehicles)
			{
				if (!vehicle.IsFree)
					continue;

				vehicle.SetTask(client);
				vehicle.GoTo(client);
				break;
			}
		}

		public void RequestParkingArea(Unit unit)
		{
            foreach (var place in parkingAreas)
            {
                if (!place.IsFree)
                    continue;

				unit.SetTask(null);
				unit.GoTo(place);
                break;
            }
        }
	}

	[Serializable]
	public class Task
	{
		public Building retrieveFrom;
		public Building deliverTo;
	}
}
