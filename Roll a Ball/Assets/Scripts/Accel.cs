using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 15), ForceMode.VelocityChange);
    }
}
