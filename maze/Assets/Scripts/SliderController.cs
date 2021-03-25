using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Text text;

    public void setText()
    {
        text.text = "Size : "+(2 * (this.GetComponent<Slider>().value) + 5).ToString();
    }
}
