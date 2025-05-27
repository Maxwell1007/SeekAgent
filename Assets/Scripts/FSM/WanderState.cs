using UnityEngine;
using UnityEngine.AI;

public class WanderState : IBaseStates
{
    private readonly NavMeshAgent _agent;

    public WanderState(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void Enter()
    {
        SetRandomNavMeshDestination();
    }

    public void Tick()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetRandomNavMeshDestination();
        }
    }

    public void Exit() { }

    private void SetRandomNavMeshDestination()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        int triangleIndex = Random.Range(0, triangulation.indices.Length / 3) * 3;

        Vector3 vertex1 = triangulation.vertices[triangulation.indices[triangleIndex]];
        Vector3 vertex2 = triangulation.vertices[triangulation.indices[triangleIndex + 1]];
        Vector3 vertex3 = triangulation.vertices[triangulation.indices[triangleIndex + 2]];

        Vector3 randomPoint = RandomPointInTriangle(vertex1, vertex2, vertex3);

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
    }

    private Vector3 RandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;
        return (1 - r1) * a + (r1 * (1 - r2)) * b + (r1 * r2) * c;
    }
}