using UnityEngine;

public class StateMachine : MonoBehaviour
{
   
       private IBaseStates _currentState;
   
       public void ChangeState(IBaseStates newState)
       {
           if (_currentState != null)
               _currentState.Exit();
   
           _currentState = newState;
           _currentState.Enter();
       }
   
       public void Update()
       {
           _currentState?.Tick();
       }
}
