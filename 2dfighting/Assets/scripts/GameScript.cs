using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour,IDecisionMaker
{
    private int playerinfocount = 0;

    private readonly int  MAX_INFOPLAYER_COUNT = 2;

    private PlayerHitInfo[] information;
    // Use this for initialization
    void Start () {
        information = new PlayerHitInfo[3];

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
     
    }

    public void AddPunchInfo(GameObject gameObject, PlayerHitInfo info)
    {
       var ifigther =  gameObject.GetComponent<IFigther>();
        playerinfocount++;
        if (playerinfocount == MAX_INFOPLAYER_COUNT)
        {

        }
    }

    public void MakeDecision()
    {


        playerinfocount = 0;
    }
}
