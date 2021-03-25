using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class SEplayQuantizeButton : MonoBehaviour,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Music.QuantizePlay(this.GetComponent<AudioSource>());
        Debug.Log("クリック!");
    }
}
