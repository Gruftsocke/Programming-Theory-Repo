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
using SchnabelSoftware.MyGame.ScriptableObjects;
using SchnabelSoftware.MyGame.Units;
using System.Collections.Generic;
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
		[SerializeField] protected float stopDistance = .15f;
		[SerializeField] protected ItemDataSO item = null;
		[SerializeField] private Transform[] points = null;

        protected Vector3[] waypoints = null;
		protected List<Unit> units = new List<Unit>();

		public bool IsFree { get; set; } = true;
		public ItemDataSO Item => item;
        public float StopDistance => stopDistance;

        protected virtual void Awake()
        {
            List<Vector3> data = new List<Vector3>();

            foreach (var item in points)
				data.Add(item.position);

            waypoints = data.ToArray();
        }
		
		public virtual Vector3[] GetWaypoints() => waypoints;
		public virtual Vector3 GetPosition() => transform.position;
		public virtual bool AddItem(ItemDataSO item) => true;
		public virtual void AddUnit(Unit unit)
		{
			if (!units.Contains(unit))
				units.Add(unit);
		}

		public virtual void RemoveUnit(Unit unit)
		{
            if (units.Contains(unit))
                units.Remove(unit);
        }

		public virtual ItemDataSO GetItemAt(UnitType unitType)
		{
			if (item != null)
			{
				if (HasFlags(item.unitType, unitType))
					return item;
			}

			return null;
		}

		public virtual void LoadItem(Unit unit, Transform equipPoint)
		{

		}

		protected static bool HasFlags(UnitMask mask, UnitType unitType)
		{
			return ((int)mask & (1 << (int)unitType)) != 0;
		}
	}
}
