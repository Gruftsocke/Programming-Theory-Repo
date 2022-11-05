/*********************************************************
 * Filename: Worker.cs
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

namespace SchnabelSoftware.MyGame.Units
{
    /// <summary>
	/// 
	/// </summary>
	public class Worker : Unit
	{
		[Header("Worker Properties")]
		[SerializeField] private Animator animator = null;

		private bool isWalking = false;

		private readonly int speedHash = Animator.StringToHash("Speed");

		protected override void Update()
		{
			base.Update();
			if (agent.hasPath)
			{
				if (!isWalking)
				{
					isWalking = true;
					animator.SetFloat(speedHash, .15f);
				}
			}
			else
			{
				if (isWalking)
				{
					isWalking = false;
					animator.SetFloat(speedHash, 0f);
				}
			}
		}
	}
}
