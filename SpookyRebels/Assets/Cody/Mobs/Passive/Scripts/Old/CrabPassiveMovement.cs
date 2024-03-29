using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CrabPassiveMovement : MonoBehaviour
{
    private NavMeshAgent crabNav = null;

    private GameObject player = null;

    [SerializeField]
    private float walkRadius = 1f;
    [SerializeField]
    private float detectRadius = 1f;
    [SerializeField]
    private float runDistance = 1f;
    
    private float nextTurnTime = 1f;
    [SerializeField]
    private float nextTurnIncrement = 1f;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(CheckPlayerPos())
        {
            MoveAway();
        }
        else if(nextTurnTime < Time.time)
        {
            MoveTo();
        }
    }

    private void MoveTo()
    {
        Debug.Log("Moved Randomly");
        float ranX = Random.Range(transform.position.x - walkRadius, transform.position.x + walkRadius);
        float ranZ = Random.Range(transform.position.z - walkRadius, transform.position.z + walkRadius);

        Vector3 moveTo = new Vector3(ranX, 0, ranZ);
        NavMeshHit hit;
        NavMesh.SamplePosition(moveTo, out hit, 5, 1); 
        crabNav.SetDestination(hit.position);

        nextTurnTime = Time.time + nextTurnIncrement;
    }

    private void MoveAway()
    {
        Transform startTransform = transform;
        
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        Vector3 runTo = transform.position + transform.forward * runDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(runTo, out hit, 5, 1); 

        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        crabNav.SetDestination(hit.position);
    }

    private bool CheckPlayerPos()
    {
        if((player.transform.position - transform.position).sqrMagnitude < detectRadius * detectRadius)
        {
            return true;
        } else {
            return false;
        }
    }
}
