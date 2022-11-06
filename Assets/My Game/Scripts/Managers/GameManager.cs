/*********************************************************
 * Filename: GameManager.cs
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
using UnityEngine;

namespace SchnabelSoftware.MyGame.Managers
{
    /// <summary>
	/// 
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		public static GameManager Current { get; private set; }

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
    }
}
