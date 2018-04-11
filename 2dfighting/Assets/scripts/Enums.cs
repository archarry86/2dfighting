using UnityEngine;
using UnityEditor;

public enum FighterHits
{
    DownPunch,
    DownHardPunch,
    DownKick,
    DownHardKick,

    ForwardLightPunch,
    ForwardMediumPunch,
    ForwardHardPunch,
    ForwardLightKick,
    ForwardMediumKick,  
    ForwardHardkick,
    //(jab)
    LightPunch,
    //(Strong)     
    MediumPunch,
    //(Fierce)     
    HardPunch,
    //(Short)      
    LightKick,
    //(Forward)    
    MediumKick,
    //(Hard)       
    HardKick,





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
    DownPunch,
    DownHardPunch,
    DownKick,
    DownHardKick,

    ForwardLightPunch,
    ForwardMediumPunch,
    ForwardHardPunch,
    ForwardLightKick,
    ForwardMediumKick,
    ForwardHardkick,
    //(jab)
    LightPunch,
    //(Strong)     
    MediumPunch,
    //(Fierce)     
    HardPunch,
    //(Short)      
    LightKick,
    //(Forward)    
    MediumKick,
    //(Hard)       
    HardKick,
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