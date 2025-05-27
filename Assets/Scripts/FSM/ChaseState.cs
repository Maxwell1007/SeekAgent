using UnityEngine;
using UnityEngine.AI;
using System;

public class ChaseState : IBaseStates
{
    private readonly NavMeshAgent _agent;
    private readonly Transform _target;
    private readonly float _interactionDistance;
    private readonly Action _onComplete;

    public ChaseState(NavMeshAgent agent, Transform target, Action onComplete, float interactionDistance = 1.5f)
    {
        _agent = agent;
        _target = target;
        _onComplete = onComplete;
        _interactionDistance = interactionDistance;
    }

    public void Enter() { }

    public void Tick()
    {
        if (_agent == null || _target == null || !_agent.isOnNavMesh)
            return;

        _agent.SetDestination(_target.position);

        float distance = Vector3.Distance(_agent.transform.position, _target.position);
        if (distance <= _interactionDistance)
        {
            InteractWithTarget();
            _onComplete?.Invoke();
        }
    }

    public void Exit() { }

    private void InteractWithTarget()
    {
        // 1. Покрасить цель в красный (если есть Renderer)
        var renderer = _target.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
        // 2. Удалить тег
        _target.tag = "Untagged";
    }
}