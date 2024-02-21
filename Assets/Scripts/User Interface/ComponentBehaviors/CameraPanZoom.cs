using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IDragHandler, IScrollHandler
{
    public Camera camera;

    public void OnDrag(PointerEventData eventData)
    {
        // TODO - PAN
    }

    public void OnScroll(PointerEventData eventData)
    {
        // TODO - ZOOM
    }
}