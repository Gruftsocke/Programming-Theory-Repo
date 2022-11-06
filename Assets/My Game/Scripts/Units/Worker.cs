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
using SchnabelSoftware.MyGame.Buildings;
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

			if (agent.hasPath && !agent.isStopped && !stopAction)
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

		protected override void BuildingInRange()
		{
			if (currentTask != null)
			{
				if (target == currentTask.retrieveFrom)
				{
					Instantiate(target.Item.prefab, equipSlot);
					GoTo(currentTask.deliverTo);
				}
				else if (target == currentTask.deliverTo)
                {
                    target.AddItem(currentTask.retrieveFrom.Item);
					if (equipSlot.childCount > 0)
						Destroy(equipSlot.GetChild(0).gameObject);

                    GoTo(currentTask.retrieveFrom);
                }
			}
		}

		public override void GoTo(Building building)
		{
			if (stopAction)
				return;

			base.GoTo(building);
		}

        public override void StopAction()
        {
			base.StopAction();
            isWalking = false;
            animator.SetFloat(speedHash, 0f);
        }
    }
}
