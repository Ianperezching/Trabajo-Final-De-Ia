using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAcharacterActionsAldeano : IACharacterActions
{


    public override void LoadComponent()
    {
        base.LoadComponent();

    }
    public void Attack()
    {

    }
    public void Shoot()
    {

        Debug.Log("Shoot " + Time.time);

    }
}
