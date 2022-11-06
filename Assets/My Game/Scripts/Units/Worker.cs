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
using UnityEngine.AI;

namespace SchnabelSoftware.MyGame.Units
{
    /// <summary>
	/// 
	/// </summary>
	public class Worker : Unit
	{
		[Header("Worker Properties")]
		[SerializeField] private Animator animator = null;

		private Rigidbody rigid = null;
		private bool isWalking = false;

		// Root motion with nav mesh agent
		//private float direction = 0f;
		private float currentSpeed = 0f;

		private readonly int speedHash = Animator.StringToHash("Speed");
        //private readonly int directionHash = Animator.StringToHash("Direction");
        private readonly int actionIdHash = Animator.StringToHash("Action ID");

        protected override void Awake()
		{
			rigid = GetComponent<Rigidbody>();
			agent = GetComponent<NavMeshAgent>();

			agent.speed = speed;
            agent.acceleration = 999f;
            agent.angularSpeed = 9999f;
        }

		void OnAnimatorMove()
		{
			agent.velocity = animator.deltaPosition / Time.deltaTime;
			transform.rotation = animator.rootRotation;
		}

		protected override void Update()
		{
			HandleTargetWaypoints();

			//direction = Vector3.Angle(transform.forward, agent.desiredVelocity) *
			//	        Mathf.Sign(Vector3.Dot(agent.desiredVelocity, transform.right));

			if (target && !stopAction)
			{
				currentSpeed = agent.desiredVelocity.magnitude;

				//animator.SetFloat(directionHash, direction, .2f, Time.deltaTime);
				animator.SetFloat(speedHash, currentSpeed, .2f, Time.deltaTime);

				agent.nextPosition = transform.position;
			}
			//transform.rotation = Quaternion.Euler(Vector3.up * direction);

            /*
			base.Update();
*/
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
					//
					animator.SetInteger(actionIdHash, 1);
				}
				else if (target == currentTask.deliverTo)
                {
                    target.AddItem(currentTask.retrieveFrom.Item);
					if (equipSlot.childCount > 0)
						Destroy(equipSlot.GetChild(0).gameObject);

                    animator.SetInteger(actionIdHash, 0);
                    GoTo(currentTask.retrieveFrom);
                }
			}
		}

		public override void GoTo(Vector3 worldPosition)
		{
            animator.SetInteger(actionIdHash, 0);
            base.GoTo(worldPosition);
		}

		//public override void GoTo(Building building)
		//{
		//	if (stopAction)
		//		return;

		//	base.GoTo(building);
		//}

		public override void StopAction()
        {
			base.StopAction();
            isWalking = false;
            animator.SetFloat(speedHash, 0f);
        }
    }
}
