/*********************************************************
 * Filename: ResourcePile.cs
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
using SchnabelSoftware.MyGame.Managers;
using SchnabelSoftware.MyGame.ScriptableObjects;
using SchnabelSoftware.MyGame.Units;
using UnityEngine;

namespace SchnabelSoftware.MyGame.Buildings
{
    /// <summary>
	/// 
	/// </summary>
	public class ResourcePile : Building
	{
		[Header("Resource Properties")]
		[SerializeField] private GameObject[] itemObjects = null;
		[SerializeField] private ItemDataSO itemBundle = null;

        private int itemCount = 0;
		private int maxItemCount = 0;

		protected override void Awake()
		{
			// Initalize waypoints...
			base.Awake();
			HideAllItems();

			maxItemCount = itemObjects.Length;
		}

		private void HideAllItems()
		{
			foreach (var item in itemObjects)
				item.SetActive(false);

			itemCount = 0;
		}

		public override ItemDataSO GetItemAt(UnitType unitType)
		{
			if (itemBundle)
			{
				if (HasFlags(itemBundle.unitType, unitType))
					return itemBundle;
            }

            if (item)
            {
				if (HasFlags(item.unitType, unitType))
					return item;
            }

            return null;
        }

		public override void LoadItem(Unit unit, Transform equipPoint)
		{
			var itemToLoad = GetItemAt(unit.UnitType);
			if (itemToLoad)
			{
				Instantiate(itemToLoad.prefab, equipPoint);
				HideAllItems();

                foreach (var worker in units)
					worker.StartAction();
            }
		}

		public override bool AddItem(ItemDataSO item)
		{
			if (this.item == item)
			{
				if (itemCount >= maxItemCount)
				{
					Debug.Log("More items can't be stacked!");
					return false;
				}

                itemObjects[itemCount].SetActive(true);
                itemCount++;

                if (itemCount == maxItemCount)
                {
                    Debug.Log("Stack is full!");
					foreach (var unit in units)
					{
						unit.StopAction();
					}

					TaskManager.Current.RequestVehicle(this);
					return false;
                }

				return true;
            }

			return false;
		}
	}
}
