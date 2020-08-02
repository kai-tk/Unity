using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CVUpdateColor : MonoBehaviour
{
    public void setColor(Color color)
    {
        GetComponent<RawImage>().color = color;
    }
}
