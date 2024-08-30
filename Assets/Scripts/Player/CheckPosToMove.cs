using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosToMove : MonoBehaviour
{
    public LayerMask HiddenWall;

    public Vector2 DtargetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool isWalkable(Vector3 targetPos, float checkDistance)
    {
        //RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, targetPos.normalized * 0.7f, 0.7f);

        //foreach(RaycastHit2D rayHit in ray)
        //{
        //    if(rayHit.collider.tag == "Wall")
        //    {
        //        return false;
        //    }
        //}
        //return true;

        DtargetPos = targetPos;
        if (Physics2D.Raycast(transform.position, targetPos, checkDistance, HiddenWall))
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, DtargetPos);
    }
}
