using UnityEngine;

public class BasicAttack : MonoBehaviour, IDragonAttack
{
    public int repeatCount;
    public float minTriggerRange;
    public float maxTriggerRange;

    public string Name
    {
        get
        {
            return "BasicAttack";
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

    public void CanDo()
    {
        throw new System.NotImplementedException();
    }
}

