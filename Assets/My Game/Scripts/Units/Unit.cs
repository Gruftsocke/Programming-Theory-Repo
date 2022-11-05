/*********************************************************
 * Filename: Unit.cs
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
using UnityEngine;
using UnityEngine.AI;

namespace SchnabelSoftware.MyGame.Units
{
    /// <summary>
	/// This is the bass class for all unit objects.
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
    public abstract class Unit : MonoBehaviour
	{
        [Header("Unit Properties")]
        [SerializeField] private Transform symbolPoint = null;

        [Header("Agent Properties")]
        [SerializeField] private float speed = 3.6f;

        protected NavMeshAgent agent = null;
        protected Building target = null;
        protected int waypointCount = 0;
        protected int currentWaypointIndex = 0;
        protected bool isFinished = false;
        protected Vector3 currentTargetPos = default;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            agent.acceleration = 999f;
            agent.angularSpeed = 999f;
        }

        protected virtual void Update()
        {
            if (target && !isFinished)
            {
                float distance = Vector3.Distance(currentTargetPos, transform.position - (Vector3.up * agent.baseOffset));
                if (distance < target.StopDistance)
                {
                    currentWaypointIndex++;
                    if (currentWaypointIndex < waypointCount)
                    {
                        currentTargetPos = target.GetWaypoints()[currentWaypointIndex];
                        agent.SetDestination(currentTargetPos);
                    }
                    else
                    {
                        agent.isStopped = true;
                        isFinished = true;
                        //BuildingInRange();
                    }
                }
            }
        }

        public virtual void GoTo(Vector3 worldPosition)
        {
            target = null;
            waypointCount = 0;
            currentWaypointIndex = 0;
            isFinished = false;
            currentTargetPos = default;

            agent.SetDestination(worldPosition);
            agent.isStopped = false;
        }

        public virtual void GoTo(Building building)
        {
            if (target == building && isFinished)
                return;

            target = building;
            if (target)
            {
                isFinished = false;
                waypointCount = target.GetWaypoints().Length;
                currentWaypointIndex = 0;

                if (waypointCount > 0)
                    currentTargetPos = target.GetWaypoints()[0];
                else
                    currentTargetPos = target.GetPosition();

                agent.SetDestination(currentTargetPos);
                agent.isStopped = false;
            }
        }

        public void SetSelectionSymbol(GameObject symbol)
        {
            symbol.transform.SetParent(symbolPoint);
            symbol.transform.localPosition = Vector3.zero;
            symbol.transform.localScale = Vector3.one;
            symbol.SetActive(true);
        }
    }
}
