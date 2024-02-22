using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public enum EnemyState
    {
        Patrolling,
        Chase,
        Attack
    }

    private void Awake()
    {
        CurrentState = States[EnemyState.Patrolling];
    }
}
