using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SeekAgent : MonoBehaviour
{
    private StateMachine _fsm;
    private NavMeshAgent _agent;
    private Transform _chaseTarget;

    [SerializeField] private float detectionRadius = 3f;
    [SerializeField] private string targetTag = "Target";

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _fsm = gameObject.AddComponent<StateMachine>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false; // 🔑 важно для 2D
        _fsm = gameObject.AddComponent<StateMachine>();
        _fsm.ChangeState(new WanderState(_agent));
    }

    private void Update()
    {
        _fsm.Update();

        if (_chaseTarget) return;
        CheckForTargets();
    }
    
    
    void CheckForTargets()
    {
        
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(targetTag))
            {
                _chaseTarget = hit.transform;
                _fsm.ChangeState(new ChaseState(_agent, _chaseTarget, OnChaseComplete));
                break;
            }
        }
    }
    public void OnChaseComplete()
    {
        _chaseTarget = null;
        _fsm.ChangeState(new WanderState(_agent));
    }
}