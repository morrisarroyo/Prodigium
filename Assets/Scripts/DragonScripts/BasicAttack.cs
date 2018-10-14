using UnityEngine;

public class BasicAttack : DragonAttack
{


    public override string Name
    {
        get
        {
            return "BasicAttack";
        }
    }


    public override bool IsDoing
    {
        get;
        set;
    }

    public override void Do()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsDone()
    {
        return repeatCount == 0;
    }

    public override bool CanDo()
    {
        throw new System.NotImplementedException();
    }
}

