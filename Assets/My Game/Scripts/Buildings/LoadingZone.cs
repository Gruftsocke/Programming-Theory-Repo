/*********************************************************
 * Dateiname: LoadingZone.cs
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
using SchnabelSoftware.MyGame.ScriptableObjects;
using UnityEngine;

namespace SchnabelSoftware.MyGame.Buildings
{
    /// <summary>
	/// 
	/// </summary>
	public class LoadingZone : Building
	{
		[SerializeField] private Transform dropPoint = null;
		[SerializeField] private ItemDataSO[] acceptedItems = null;

		public override bool AddItem(ItemDataSO itemToStack)
		{
			foreach (var item in acceptedItems)
			{
				if (item != itemToStack)
					continue;

				Destroy(Instantiate(itemToStack.prefab, dropPoint), 2f);
				return true;
			}

			return false;
		}
	}
}
