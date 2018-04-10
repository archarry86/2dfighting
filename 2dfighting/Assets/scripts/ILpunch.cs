using UnityEngine;

public interface IHit
{
    RaycastHit2D[] CheckHit(string layername);
    void HitDisabled();
    void HitEnabled();
    void HitInitialization();
    void _drawGizmo();
}