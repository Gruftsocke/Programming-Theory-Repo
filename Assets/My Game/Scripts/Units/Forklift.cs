/*********************************************************
 * Filename: Forklift.cs
 * Project : SchnabelSoftware.MyGame
 * Date    : 01.11.2022
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
using SchnabelSoftware.MyGame.Core;
using SchnabelSoftware.MyGame.Managers;
using UnityEngine;

namespace SchnabelSoftware.MyGame.Units
{
	/// <summary>
	/// 
	/// </summary>
	public class Forklift : Unit
	{
        [SerializeField] private RotationLight rotationLight = null;

        protected override void Update()
        {
            HandleTargetWaypoints();

            rotationLight.Enabled = agent.hasPath;
        }

        protected override void BuildingInRange()
		{
            if (currentTask != null)
            {
                if (target == currentTask.retrieveFrom)
                {
                    currentTask.retrieveFrom.LoadItem(this, equipSlot);
                    GoTo(currentTask.deliverTo);
                }
                else if (target == currentTask.deliverTo)
                {
                    target.AddItem(currentTask.retrieveFrom.GetItemAt(unitType));
                    if (equipSlot.childCount > 0)
                        Destroy(equipSlot.GetChild(0).gameObject);

                    if (loopAction)
                        GoTo(currentTask.retrieveFrom);
                    else
                        TaskManager.Current.RequestParkingArea(this);
                }
            }
        }
	}
}
