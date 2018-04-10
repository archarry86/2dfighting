using UnityEngine;
using UnityEditor;

public enum FighterHits
{

    Lmkick,
    Lpunch,

}

public enum HitsStates
{

    Disabled = 0,
    Enabled = 10,
    Finished = 20

}




public enum FighterStates
{
    _None,
    Blocking,
    Idle,
    Lmkick,
    Lpunch,
    Walking,
}

public enum Players
{
    Pc,
    PlayerOne,
    PlayerTwo,
}

public static class EnumExtencionMethods
{
    public static FighterHits GetFighterHit(this FighterStates fighterStates)
    {
        return (FighterHits)System.Enum.Parse(typeof(FighterHits), fighterStates.ToString());
    }

    public static FighterStates GetFighterState(this FighterHits fighterHits)
    {
        return (FighterStates)System.Enum.Parse(typeof(FighterStates), fighterHits.ToString());
    }

   
}