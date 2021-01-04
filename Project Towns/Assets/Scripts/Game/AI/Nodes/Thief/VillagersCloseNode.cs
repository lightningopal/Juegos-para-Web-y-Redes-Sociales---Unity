﻿using System.Collections.Generic;
using UnityEngine;
public class VillagersCloseNode : Node
{
    private Thief thief;

    public VillagersCloseNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;
        List<Villager> villagersInRange = new List<Villager>();

        // Por cada aldeano, se calcula si está en rango
        foreach (Villager villager in GameManager.instance.NPCs)
        {
            if (Vector3.Distance(thief.transform.position, villager.transform.position) < thief.CLOSE_VILLAGERS_RANGE)
            {
                villagersInRange.Add(villager);
                _nodeState = NodeState.SUCCESS;
            }
        }

        // Se asignan los aldeanos en rango
        thief.villagersInRange = new List<Villager>(villagersInRange);

        return _nodeState;
    }
}
