using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerHitInfo
{

    private Players player;
    private readonly RaycastHit[] def = new RaycastHit[0];
    private RaycastHit[]  hits;
    private FighterStates playerstate = FighterStates._None;
    private FighterStates enemystate = FighterStates._None; 

    public PlayerHitInfo(Players player)
    {
        this.player = player;
        hits = def;
    }

    public void setHitInfoData(RaycastHit[] hits , FighterStates playerstate, FighterStates enemystate)
    {
        this.hits = hits;
        this.playerstate = playerstate;
        this.enemystate = enemystate;
    }

    public void setHitInfoDataNone()
    {
        this.hits = def;
        this.playerstate = FighterStates._None;
        this.enemystate = FighterStates._None;
    }



}
