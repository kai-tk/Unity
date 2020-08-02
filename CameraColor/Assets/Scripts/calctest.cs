namespace OpenCvSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class calctest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Vec3b ghsv2 = new Vec3b(255, 174, 211);
            Vec3b ghsv = new Vec3b(105, 2, 255);
            Vec3b shsv = new Vec3b(174, 62, 241);
            Vector3 psv = new Vector3(0, (float)shsv[1] / (ghsv[1] + 1), (float)shsv[2] / (ghsv[2] + 1));
            Vector3 psv2 = new Vector3(0, (float)shsv[1] / (ghsv2[1] + 1), (float)shsv[2] / (ghsv2[2] + 1));

            //Vec3b result1 = new Vec3b(shsv[0], (byte)((int)Mathf.Clamp(ghsv[1] * psv[1], 0, 255)), (byte)((int)Mathf.Clamp(ghsv[2] * psv[2], 0, 255)));
            //Vec3b result2 = new Vec3b(shsv[0], (byte)((int)Mathf.Clamp(ghsv2[1] * psv2[1], 0, 255)), (byte)((int)Mathf.Clamp(ghsv2[2] * psv2[2], 0, 255)));
            //Vec3b result2 = new Vec3b(shsv[0], (byte)((int)Mathf.Clamp(ghsv2[1] * psv[1], 0, 255)), (byte)((int)Mathf.Clamp(ghsv2[2] * psv[2], 0, 255)));

            Vec3b result1 = new Vec3b(shsv[0], (byte)((ghsv[1] + shsv[1] * 3) / 4), (byte)((ghsv[2] + shsv[2] * 3) / 4)); 
            Vec3b result2 = new Vec3b(shsv[0], (byte)((ghsv2[1] + shsv[1] * 3) / 4), (byte)((ghsv2[2] + shsv[2] * 3) / 4));

            //Debug.Log("(" + psv[0] + " " + psv[1] + " " + psv[2] + "), (" + psv2[0] + " " + psv2[1] + " " + psv2[2] + ")");
            Debug.Log("("+result1[0] + " "+result1[1] +" "+ result1[2]+"), ("+ result2[0] + " " + result2[1] + " " + result2[2]+")");
        }
    }
}