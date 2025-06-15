using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacterVehiculoAldeano : IACharacterVehiculo
{

    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();

    }

    public override void MoveToPosition(Vector3 pos)
    {
        base.MoveToPosition(pos);
    }

    public override void MoveToEnemy()
    {
        base.MoveToEnemy();
    }
    public override void MoveToAllied()
    {
        base.MoveToAllied();
    }
    public override void MoveToEvadEnemy()
    {
        base.MoveToEvadEnemy();
    }
    public override void MoveToWander()
    {
        // Si ve un zombie, huye
        if (AIEye.ViewEnemy != null && AIEye.ViewEnemy._UnitGame == UnitGame.Zombie)
        {
            MoveToEvadEnemy();
            return;
        }
        // Si ve un golem, lo sigue
        if (AIEye.ViewAllie != null && AIEye.ViewAllie._UnitGame == UnitGame.golem)
        {
            MoveToAllied();
            return;
        }
        // Si no ve nada, wander
        base.MoveToWander();
    }

}
