using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class CVClickController : MonoBehaviour
{
    private Color color = Color.white;
    Vector2Int webcamTexSize = new Vector2Int(1280, 960);
    CVCamController script;

    void Start()
    {
        script = GetComponent<CVCamController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && script.Getflag())
        { 
            Vector2 mousepos = Input.mousePosition;

            RectTransform rectTransform = transform as RectTransform;
            Canvas rootCanvas = rectTransform.GetComponentInParent<Canvas>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousepos, rootCanvas.worldCamera, out Vector2 localpos);
            UnityEngine.Rect rect = rectTransform.rect;
            Vector2 uv = (localpos - rect.position) / rect.size;

            Vector2Int texPos = new Vector2Int((int)(uv.x * webcamTexSize.x), (int)(webcamTexSize.y - uv.y * webcamTexSize.y));
            
            if(0<=texPos.x && texPos.x<webcamTexSize.x && 0 <= texPos.y && texPos.y < webcamTexSize.y)
            {
                script.GetColor(texPos);
            }
        }
    }
}
