/*********************************************************
 * Filename: ItemDataSO.cs
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
using SchnabelSoftware.MyGame.Units;
using System;
using UnityEngine;

namespace SchnabelSoftware.MyGame.ScriptableObjects
{
    /// <summary>
	/// 
	/// </summary>
	[CreateAssetMenu(fileName = "NewItemDataSO", menuName = "Schnabel Software/Item Data")]
	public class ItemDataSO : ScriptableObject
	{
        public new string name = string.Empty;
        [TextArea(5, 25)]
        public string description = string.Empty;

        public Sprite icon = null;
        public GameObject prefab = null;

        public UnitMask unitType = 0;
    }
}
