using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    [SerializeField]
    private float walkRadius;
    [SerializeField]
    private NavMeshAgent me;

    public bool pickedUp = false;

    private Vector3 finalPosition;

    private void Start()
    {
        finalPosition = transform.position;   
    }

    void Update()
    {
        if (!pickedUp)
        {
            me.isStopped = false;
            if (findingLocation != null) { StopCoroutine(findingLocation); }
            else { findingLocation = StartCoroutine(FindingLocation()); }

            me.SetDestination(finalPosition);
        }
        else
        {
            me.isStopped = true;
        }
    }

    public bool isFindingLocation { get { return findingLocation != null; } }
    Coroutine findingLocation = null;
    public IEnumerator FindingLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        finalPosition = hit.position;

        print("Yum");
        yield return new WaitForSeconds(2f);
        print("Bum");
        findingLocation = null;
    }
}
