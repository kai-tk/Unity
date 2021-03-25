using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangeColor : MonoBehaviour
{
    public float Time = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if(Music.IsPlaying && Music.IsJustChangedBar())
        {
            Color color = new Color(
                Random.Range(0.5f, 0.8f),
                Random.Range(0.5f, 0.8f),
                Random.Range(0.5f, 0.8f),
                1f
                );
            this.GetComponent<Image>().DOColor(color, Time);
        }
    }
}
