using UnityEngine;
using UnityEditor;
using System;

//[CreateAssetMenu(fileName = "New Hi Info", menuName = "HitInformation")]
[System.Serializable]
public class HitInformationCollection //: ScriptableObject
{
    [SerializeField]
    private Hit[] hits;

    public HitInformationCollection()
    {
        var names = Enum.GetNames(typeof(FighterHits));
        hits = new Hit[names.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            Hit hit = new Hit();
            hit.SetFighterHitname((FighterHits)(i));
            hits[i] = hit;
        }
    }

    public Hit GetHit(FighterHits hit)
    {
        var result = hits[(int)hit];
        result.HitInitialization();
        return result;
    }


    public Hit[] GetHits()
    {
      
        return hits;
    }


}
[System.Serializable]
public class Hit : IHit
{
    [SerializeField]
    private Color colorGizmo = Color.cyan;

    [SerializeField]
    private int indexFrameGizmo = 0;


    [SerializeField]
    private bool Enabled = true;


    private int hitStatus = (int)HitsStates.Disabled;

    public int HitStatus
    {
        get
        {
            return hitStatus;
        }
    }

    [SerializeField]
    private FighterHits fighterHitname;

    public void SetFighterHitname(FighterHits value)
    {

        fighterHitname = value;
    }

    [SerializeField]
    private HitTransformInformation[] hitTransformInformation;

    [SerializeField]
    private Transform mainTransform;

    [SerializeField]
    private Transform bodyTransform;

    private HitTransformInformation currenthitinformation;



    public Hit()
    {
        hitTransformInformation = new HitTransformInformation[1]
        {
            new HitTransformInformation()
        };
    }
    private RaycastHit2D[] infoHits = new RaycastHit2D[0];
    public RaycastHit2D[] CheckHit(string layername = "Enemy")
    {
        RaycastHit2D[] result = infoHits;
        if (hitStatus >= (int)HitsStates.Enabled && hitStatus < (int)HitsStates.Finished)
        {

            SetCurrentHitInformation();

            ApplyBodyPartTransformation();


            var playerhitdirection = currenthitinformation.PlayerHitDirection(this.mainTransform);

            var hitdirection = playerhitdirection - currenthitinformation.Origin;
            result = Physics2D.RaycastAll(CalculateOrigin(), playerhitdirection, playerhitdirection.magnitude, LayerMask.GetMask(layername));// "Enemy"));

        }
        return result;
    }

    private void SetCurrentHitInformation()
    {
        if(hitTransformInformation.Length > 1)
            currenthitinformation = hitTransformInformation[UtilMath.Mod(hitStatus, 100)];
        else
            currenthitinformation = hitTransformInformation[hitTransformInformation.Length-1];
    }

    private void ApplyBodyPartTransformation()
    {
        if (bodyTransform != null)
        {
            if (currenthitinformation.Position != Vector3.zero)
                bodyTransform.localPosition = currenthitinformation.Position;
            if (currenthitinformation.Scale != Vector3.zero)
                bodyTransform.localScale = currenthitinformation.Scale;
            if (currenthitinformation.Rotation != Vector3.zero)
                bodyTransform.localEulerAngles = currenthitinformation.Rotation;
        }
    }

    private Vector3 CalculateOrigin()
    {
        return this.mainTransform.position + currenthitinformation.Origin;
    }

    public void HitInitialization()
    {
        hitStatus = (int)HitsStates.Disabled;

    }

    public void HitEnabled()
    {
        hitStatus = (int)HitsStates.Enabled;
        if (bodyTransform != null)
            bodyTransform.gameObject.SetActive(true);

        SetCurrentHitInformation();
    }

    public void AddOneFrame()
    {
        hitStatus +=1 ;
    }

    public void HitDisabled()
    {
        hitStatus = (int)HitsStates.Finished;
        if (bodyTransform != null)
            bodyTransform.gameObject.SetActive(false);
    }


    public void _drawGizmo()
    {
        if (hitStatus >= (int)HitsStates.Enabled && hitStatus < (int)HitsStates.Finished)
        {
            Gizmos.color = Color.blue;
            var playerhitdirection = currenthitinformation.PlayerHitDirection(this.mainTransform);
          
            Gizmos.DrawSphere(this.mainTransform.position + currenthitinformation.Origin + playerhitdirection, 0.05f);
            Gizmos.DrawRay(this.CalculateOrigin(), playerhitdirection);
        }
    }

    public void _drawGizmoEdition()
    {
      
         //if (hitStatus >= (int)HitsStates.Enabled && hitStatus < (int)HitsStates.Finished)
         var _hitTransformInformation = hitTransformInformation[indexFrameGizmo % hitTransformInformation.Length];
       
        Gizmos.color = colorGizmo;

        if(this.mainTransform != null)
        Gizmos.DrawSphere(this.mainTransform.position, 0.05f);

        Gizmos.DrawSphere(_hitTransformInformation.Origin, 0.05f);

        Gizmos.DrawSphere(_hitTransformInformation.Origin+_hitTransformInformation.Direction, 0.05f);

    }
}



[System.Serializable]
public class HitTransformInformation
{
   

    [SerializeField]
    private Vector3 origin = Vector3.zero;


    public Vector3 Origin
    {
        get
        {
            return origin;
        }

    }


    [SerializeField]
    private Vector3 direction = Vector3.zero;


    public Vector3 Direction
    {
        get
        {
            return direction;
        }

    }

    [SerializeField]
    private Vector3 position = Vector3.zero;

    public Vector3 Position
    {
        get
        {
            return position;
        }

    }

    [SerializeField]
    private Vector3 rotation = Quaternion.identity.eulerAngles;


    public Vector3 Rotation
    {
        get
        {
            return rotation;
        }

    }

    [SerializeField]
    private Vector3 scale = new Vector3(1, 1, 1);

    public Vector3 Scale
    {
        get
        {
            return scale;
        }

    }

    public Vector3 PlayerHitDirection(Transform maintransform)
    {
        var Signx = Mathf.Sign(maintransform.localScale.x);

        return new Vector3(Direction.x * Signx, Direction.y, Direction.z);

    }
}