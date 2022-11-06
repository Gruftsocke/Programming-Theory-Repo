/*********************************************************
 * Filename: PlayerController.cs
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
using SchnabelSoftware.MyGame.Buildings;
using SchnabelSoftware.MyGame.Units;
using UnityEngine;

namespace SchnabelSoftware.MyGame.Controllers
{
    /// <summary>
	/// 
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private LayerMask unitLayerMask = 0;
        [SerializeField] private LayerMask groundLayerMask = 0;
        [SerializeField] private float rayDistance = 500f;
		[SerializeField] private GameObject selectionSymbol = null;

		private Unit selectedUnit = null;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, unitLayerMask))//, QueryTriggerInteraction.Ignore))
				{
					if (hit.collider.TryGetComponent(out Unit unit))
					{
						selectedUnit = unit;
						selectedUnit.SetSelectionSymbol(selectionSymbol);
					}
				}
				else
				{
					selectionSymbol.SetActive(false);
					selectionSymbol.transform.parent = null;

                    selectedUnit = null;
				}
			}
			else if (Input.GetMouseButtonDown(1) && selectedUnit)
			{
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, groundLayerMask))//, QueryTriggerInteraction.Ignore))
                {
					if (hit.collider.TryGetComponent(out Building building))
					{
						selectedUnit.SetTask(building);
						selectedUnit.GoTo(building);
					}
					else
					{
						selectedUnit.GoTo(hit.point);
					}
                }
            }
		}
	}
}
