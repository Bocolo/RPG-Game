using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Attributes;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction , ISaveable
    {

        [SerializeField]Transform target;
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxNavPathLength = 40f;
        NavMeshAgent navMeshAgent;
        Health health;
        //   Vector3 distanceFrom;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
       /* private void Start()
        {
            *//*navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();*//*
        }*/
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();

        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            
            MoveTo(destination, speedFraction);
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        public bool CanMoveTo(Vector3 destination)
        {

            //calculating nav mesh path
            //path needs to be assigned
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            //preventing use of partial paths
            //e.g to top of unaccessible buildings
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;


            return true;
        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }
        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            // not to full length
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            //if tweaking add print state to evaluate
            return total;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        public object CaptureState()
        {
            // methods for saving multiples pieces of data for one componenet/ dictionary
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"]= new SerializableVector3(transform.position);
            data["rotation"]= new SerializableVector3(transform.eulerAngles);

            //restriction on waht can be returned, has to be serializable
            //previous
            // return new SerializableVector3 (transform.position);

            // using dictionary method
            return data;
            //REVIEW LECTURE 85 BONUS
            //ALSO demonstractes struct method
            //must also change restor state using below
            //  Dictionary<string, object> data =(Dictionary<string, object>)state; 
            //instead of//
            //SerializableVector3 position = (SerializableVector3)state; -- see restore state
        }

        public void RestoreState(object state)
        {//called after awake but before start
         // SerializableVector3 position = (SerializableVector3)state;
         //nav mesh to prevant nave mesh interfering
         //
         //this causing yellow error--- failed to creat agent... explore navmeshAgent.wrap() 
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;

            // below is changed with dictionary changes
            //transform.position = position.ToVector();
            //changed to 
            transform.position =( (SerializableVector3) data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();

            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

/*   private void MoveToCursor()
   {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;
       bool hasHit = Physics.Raycast(ray, out hit);
       if (hasHit)
       {
           MoveTo(hit.point);
       }
   }*/
/*   if (Input.GetMouseButton(0))
        {
            MoveToCursor();
            
        }*/
