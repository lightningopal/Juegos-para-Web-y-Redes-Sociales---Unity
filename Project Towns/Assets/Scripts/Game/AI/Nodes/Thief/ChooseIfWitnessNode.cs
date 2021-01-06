using UnityEngine;
public class ChooseIfWitnessNode : Node
{
    private Thief thief;

    public ChooseIfWitnessNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;

        int randomWitnessNumber = Random.Range(0, 100);
        if (randomWitnessNumber < thief.fakeWitnessProbability)
        {
            _nodeState = NodeState.SUCCESS;
            thief.isWitness = true;
            thief.CalculateFakeItem();
        }
            
        return _nodeState;
    }
}
