using UnityEngine;
using UnityEditor;

public interface IDecisionMaker
{
    

    void AddPunchInfo(GameObject gameObject, PlayerHitInfo info);

    void MakeDecision();
}

public interface IFigther
{
    FighterStates FighterStates { get; set; }

    Players Player { get;  }
}