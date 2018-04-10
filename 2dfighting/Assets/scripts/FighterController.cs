using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{

    [SerializeField]
    private HitInformationCollection hitInformationCollection;

    private Hit currentHit = new Hit();

    [SerializeField]
    private int indexHitGizmo = 0;

    private Animator animator;
    [SerializeField]
    private FighterStates fighterstate;    // Use this for initialization

    private FighterStates lastfighterstate;








    [SerializeField]
    private Vector3 walkingvelocity = Vector3.zero;

    float cooldown = 0;
    [SerializeField]
    private float cooldowngap = 0.300f;

    [SerializeField]
    private int direction = 0;

    private Transform kickGO;
    private Transform pubchGO;

    private Transform transformchecking;

    private RaycastHit2D[] infoHits = new RaycastHit2D[0];

    private DateTime datekick;

    private string LastKick;

    private readonly RaycastHit2D[] defhits = new RaycastHit2D[0];

    [SerializeField]
    private GameObject Enemy;

    void Start()
    {
        if (hitInformationCollection == null)
            hitInformationCollection = new HitInformationCollection();

        animator = this.GetComponent<Animator>();
        fighterstate = FighterStates.Idle;




        var colliders = this.GetComponents<BoxCollider2D>();


        walkingvelocity.y = 0;
        walkingvelocity.z = 0;

        kickGO = this.transform.GetChild(1);
        kickGO.gameObject.SetActive(false);
        pubchGO = this.transform.GetChild(2);
        pubchGO.gameObject.SetActive(false);



    }

    private void FixedUpdate()
    {
        // infoHits = defhits;

        CheckHit();

    }

    private void CheckHit()
    {
        //if (_checkPunch == 1)
        //{
        //    var Signx = Mathf.Sign(this.transform.localScale.x);
        //
        //    var auxhits = defhits;
        //    LastKick = fighterstate.ToString();
        //    switch (fighterstate)
        //    {
        //        case FighterStates.Lpunch:
        //            //cast ray
        //            //shoulders.x = Mathf.Abs(shoulders.x) * Signx;
        //
        //            var armsizeaux = new Vector3(armsize.x * Signx, armsize.y, armsize.z);
        //            var direction = armsizeaux - shoulders;
        //            auxhits = Physics2D.RaycastAll(this.transform.position + shoulders, armsizeaux, direction.magnitude, LayerMask.GetMask("Enemy"));
        //
        //            break;
        //
        //        case FighterStates.Lmkick:
        //            //cast ray
        //            //legsize.x = Mathf.Abs(legsize.x) * Signx;
        //            var legsizeaux = new Vector3(legsize.x * Signx, legsize.y, legsize.z);
        //            direction = legsizeaux;//(this.transform.position+ legsizeaux) - this.transform.position;
        //            auxhits = Physics2D.RaycastAll(this.transform.position, direction, direction.magnitude, LayerMask.GetMask("Enemy"));
        //            break;
        //    }
        //
        //    // if (auxhits.Length>0)
        //    {
        //        infoHits = auxhits;
        //        _checkPunch += 0.1f;
        //    }
        //}

        var hitsresult = currentHit.CheckHit();
        if (hitsresult.Length > 0)
        {
            infoHits = hitsresult;
            datekick = DateTime.Now;
        }
    }

    // Update is called once per frame
    void Update()
    {

        FighterStates currentstate = fighterstate;
        switch (currentstate)
        {
            case FighterStates.Idle:

                checkEnemyPosition();
                if (Input.GetKeyDown(KeyCode.A))
                {

                    transformchecking = pubchGO;

                    currentstate = FighterStates.Lpunch;
                    currentHit = hitInformationCollection.GetHit(currentstate.GetFighterHit());


                }

                else if (Input.GetKeyDown(KeyCode.S))
                {
                    transformchecking = kickGO;

                    currentstate = FighterStates.Lmkick;
                    currentHit = hitInformationCollection.GetHit(currentstate.GetFighterHit());

                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {

                    currentstate = FighterStates.Walking;
                    cooldown = Time.time + cooldowngap;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {

                    currentstate = FighterStates.Walking;
                    cooldown = Time.time + cooldowngap;
                }





                break;
            case FighterStates.Lpunch:
                if (currentHit.HitStatus >= (int)HitsStates.Finished)
                {
                    currentstate = FighterStates.Idle;

                }
                break;

            case FighterStates.Lmkick:
                if (currentHit.HitStatus >= (int)HitsStates.Finished)
                {
                    currentstate = FighterStates.Idle;

                }
                break;
            case FighterStates.Walking:

                //this.transform.LookAt(Enemy.transform);
                checkEnemyPosition();
                direction = 0;
                if (cooldown <= Time.time)
                {

                    cooldown = Time.time + cooldowngap;

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        direction = 1;
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        direction = -1;
                    }

                    if (direction != 0)
                    {
                        this.transform.position = this.transform.position + walkingvelocity * direction;
                    }
                    else
                    {

                        currentstate = FighterStates.Idle;

                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    {

                        currentstate = FighterStates.Idle;



                    }
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    transformchecking = pubchGO;

                    currentstate = FighterStates.Lpunch;
                    currentHit = hitInformationCollection.GetHit(currentstate.GetFighterHit());


                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    transformchecking = kickGO;

                    currentstate = FighterStates.Lmkick;
                    currentHit = hitInformationCollection.GetHit(currentstate.GetFighterHit());

                }


                break;
        }

        SetAnimator(currentstate);
    }


    private void SetAnimator(FighterStates newState)
    {
        if (newState != fighterstate)
        {
            animator.ResetTrigger("" + fighterstate);
            fighterstate = newState;
            animator.SetTrigger("" + fighterstate);

        }

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

    void CheckPunch()
    {

        transformchecking.gameObject.SetActive(true);
        if (currentHit != null)
            currentHit.HitEnabled();
    }


    void UnCheckPunch()
    {

        transformchecking.gameObject.SetActive(false);

        if (currentHit != null)
            currentHit.HitDisabled();
    }

    void AddOneFrame()
    {
        if (currentHit != null)
            currentHit.AddOneFrame();
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor && !Application.isPlaying){

            var hits = hitInformationCollection.GetHits();
            var hit = hits[indexHitGizmo % hits.Length];
            {
                hit._drawGizmoEdition();
               
            }
        }

        if (currentHit != null)
        {
            currentHit._drawGizmo();
        }


    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, 40), " checkPunch " + currentHit.HitStatus + " " + fighterstate.ToString() + " " + direction + " " + cooldown);
        GUI.Label(new Rect(0, 40, Screen.width, 40), "  " + Mathf.Sign(this.transform.localScale.x));

        GUI.Label(new Rect(0, 80, Screen.width, 40), LastKick +" "+ datekick.ToString("dd HH:mm:ss") +" hitinfo " + " " + GetHitInfo());
    }
    private string GetHitInfo()
    {
        string result = " " + infoHits.Length;
        for (int i = 0; i < infoHits.Length; i++)
            result += infoHits[i].transform.gameObject.name + " | ";

        return result;
    }
}