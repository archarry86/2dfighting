using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFighterScript : MonoBehaviour {


    [SerializeField]
    private GameObject Enemy;


    private Transform kickGO;
    private Transform pubchGO;

    // Use this for initialization
    void Start () {


        kickGO = this.transform.GetChild(1);
        kickGO.gameObject.SetActive(false);
        pubchGO = this.transform.GetChild(2);
        pubchGO.gameObject.SetActive(false);

        GetComponent<SpriteRenderer>().color = Color.blue;
    }
	
	// Update is called once per frame
	void Update () {
        checkEnemyPosition();
    }

    private void checkEnemyPosition()
    {
        var result = Mathf.Sign(this.transform.position.x - this.Enemy.transform.position.x);

        if (result < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x) * 1;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x) * -1;
            transform.localScale = theScale;
        }

    }
}
