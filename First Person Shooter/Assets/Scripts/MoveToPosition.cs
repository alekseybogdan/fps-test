using UnityEngine;
using UnityEngine.AI;

public class MoveToPosition : MonoBehaviour
{

    [HideInInspector]
    public Transform goal;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        agent.SetDestination(goal.position);
    }
}