using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacterVehiculoGolem : IACharacterVehiculo
{
    private Vector3 initialPosition;

    void Start()
    {
        this.LoadComponent();
        initialPosition = transform.position;
    }

    public override void MoveToWander()
    {
        // El golem no se mueve por defecto
        agent.SetDestination(initialPosition);
    }

    public void MoveToEnemy()
    {
        if (AIEye.ViewEnemy != null && AIEye.ViewEnemy._UnitGame == UnitGame.Zombie)
        {
            agent.SetDestination(AIEye.ViewEnemy.transform.position);
        }
    }

    public void ReturnToInitialPosition()
    {
        agent.SetDestination(initialPosition);
    }
}
