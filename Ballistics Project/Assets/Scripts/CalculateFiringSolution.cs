using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalculateFiringSolution
{

    public Vector3 start;
    public Vector3 end;
    public float muzzleV;
    public Vector3 gravity;

    public Nullable<float> ShotestForAngle(Vector3 start,Vector3 end, float muzzleV, Vector3 gravity)
    {
        //Vector3 delta = start - end;
        Vector3 delta = end - start;

        float a = gravity.sqrMagnitude;
        float b = -4 * (Vector3.Dot(gravity, delta) + muzzleV * muzzleV);
        float c = 4 * delta.sqrMagnitude;

        float b2minus4ac = (b * b) - (4 * a * c);
        if (b2minus4ac < 0)
        {
            return null;
        }

        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b2minus4ac)) / (2 * a));

        Nullable<float> ttt;
        if (time0 < 0)
        {
            if (time1 < 0)
            {
                return null;
            }
            else
            {
                ttt = time1;
            }
        }
        else if (time1 < 0)
        {
            ttt = time0;
        }
        else
        {
            ttt = Mathf.Min(time0, time1);
        }

        return ttt;
    }


    public Nullable<Vector3> AngleForTargeting(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        Nullable<float> ttt = ShotestForAngle(start, end, muzzleV, gravity);
        if (!ttt.HasValue)
        {
            return null;
        }

        Vector3 delta = end - start;

        Vector3 n1 = delta * 2;
        Vector3 n2 = gravity * (ttt.Value * ttt.Value);
        float d = 2 * muzzleV * ttt.Value;
        Vector3 solution = (n1 - n2) / d;


        return solution;
    }


}
