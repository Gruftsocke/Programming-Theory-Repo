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
using SchnabelSoftware.MyGame.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace SchnabelSoftware.MyGame.Units
{
    /// <summary>
	/// This is the base class for all unit objects.
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
    public abstract class Unit : MonoBehaviour
	{
        [Header("Unit Properties")]
        [SerializeField] protected Transform symbolPoint = null;
        [SerializeField] protected Transform equipSlot = null;
        [SerializeField] protected UnitType unitType = default;

        [Header("Agent Properties")]
        [SerializeField] protected float speed = 3.6f;

        protected NavMeshAgent agent = null;
        protected Building target = null;
        protected int waypointCount = 0;
        protected int currentWaypointIndex = 0;
        protected bool isFinished = false;
        protected bool stopAction = false;
        protected bool loopAction = false;
        protected Vector3 currentTargetPos = default;
        protected Managers.Task currentTask = null;

        public UnitType UnitType => unitType;
        public int UnitMask => 1 << (int)unitType;
        public bool IsFree { get; protected set; } = true;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            agent.acceleration = 999f;
            agent.angularSpeed = 999f;
            //agent.updatePosition = false;
            //agent.updateRotation = false;
        }

        protected virtual void Update()
        {
            HandleTargetWaypoints();
        }

        protected virtual void HandleTargetWaypoints()
        {
            if (target && !stopAction)
            {
                if (!isFinished)
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
                        }
                    }
                }
                else
                {
                    BuildingInRange();
                }
            }

            IsFree = !agent.hasPath;
        }

        public virtual void GoTo(Vector3 worldPosition)
        {
            if (target)
                target.IsFree = true;

            target = null;
            waypointCount = 0;
            currentWaypointIndex = 0;
            isFinished = false;
            stopAction = false;
            currentTargetPos = default;
            SetTask(null);

            agent.SetDestination(worldPosition);
            agent.isStopped = false;

            IsFree = false;
        }

        public virtual void GoTo(Building building)
        {
            if (target == building && isFinished)
                return;

            if (target)
                target.IsFree = true;

            target = building;
            if (target)
            {
                target.IsFree = false;
                isFinished = false;
                //stopAction = false;
                waypointCount = target.GetWaypoints().Length;
                currentWaypointIndex = 0;

                if (waypointCount > 0)
                    currentTargetPos = target.GetWaypoints()[0];
                else
                    currentTargetPos = target.GetPosition();

                agent.SetDestination(currentTargetPos);
                agent.isStopped = stopAction;

                IsFree = false;
            }
        }

        public void SetSelectionSymbol(GameObject symbol)
        {
            symbol.transform.SetParent(symbolPoint);
            symbol.transform.localPosition = Vector3.zero;
            symbol.transform.localScale = Vector3.one;
            symbol.SetActive(true);
        }

        protected abstract void BuildingInRange();

        protected void RemoveCurrentTaskAndEquipItem()
        {
            if (equipSlot.childCount > 0)
                Destroy(equipSlot.GetChild(0).gameObject);

            if (currentTask != null)
            {
                currentTask.retrieveFrom?.RemoveUnit(this);
                currentTask.deliverTo?.RemoveUnit(this);
                currentTask = null;
            }
        }

        public virtual void SetTask(Building building)
        {
            RemoveCurrentTaskAndEquipItem();

            if (building)
            {
                currentTask = TaskManager.Current.GetTask(building);

                if (currentTask != null)
                {
                    currentTask.retrieveFrom.AddUnit(this);
                    currentTask.deliverTo.AddUnit(this);
                }
            }
        }

        public virtual void StopAction()
        {
            agent.isStopped = true;
            stopAction = true;
        }

        public virtual void StartAction()
        {
            if (!isFinished)
                agent.isStopped = false;

            stopAction = false;
        }
    }
}
