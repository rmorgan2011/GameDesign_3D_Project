using System;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public bool pursuing = true;
        public GameObject teleporterEffect;
        public Transform teleporterEffectSpawn;
        public Transform[] spawnPoints;

		public float normalDetectDistance;
		//flashlight = easier to find = enemies can spot you from further away
		public float flashlightDetectDistance;
		//sprinting = louder = enemies can detect from further away
		public float sprintDetectDistance;

		//to keep track of whether or not the flashlight is on
		private GameController gameController;
		private Transform playerTransform;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
			playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
			gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
			if (gameController == null) {
				//shouldn't happen
			}

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
       //     Debug.Log("remaining Distance:" + agent.remainingDistance);
  //          if (agent.remainingDistance < 20 && agent.remainingDistance != 0)
   //         {                
   //             pursuing = true;
   //         }
			float distance = Vector3.Distance(transform.position, playerTransform.position);
			if(distance<normalDetectDistance || (distance<flashlightDetectDistance && gameController.isOn) || (distance<sprintDetectDistance && gameController.isSprinting)){
            if (pursuing)
            {
                if (target != null)
                    agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);
            }
            else
//Instantiate(teleporterEffect, teleporterEffectSpawn.position, teleporterEffectSpawn.transform.localRotation);
                character.Move(Vector3.zero, false, false);
        }
		}


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
