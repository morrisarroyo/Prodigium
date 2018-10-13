using UnityEngine;

public class FlameAttack : MonoBehaviour, IDragonAttack
{
    public int repeatCount;
    public float minTriggerRange;
    public float maxTriggerRange;

    public string Name
    {
        get
        {
            return "FlameAttack";
        }
    }

    public bool IsDoing
    {
        get
        {
            return IsDoing;
        }

        set
        {
            IsDoing = value;
        }
    }

    public void Do()
    {
        throw new System.NotImplementedException();
    }

    public bool IsDone()
    {
        return repeatCount == 0;
    }

    public bool CanDo()
    {
        return false;
    }
}
