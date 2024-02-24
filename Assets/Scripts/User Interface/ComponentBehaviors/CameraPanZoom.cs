using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IDragHandler, IScrollHandler, IPointerClickHandler
{
    public Camera mainCamera;
    private float dragScale = .031f;
    private float zoomScale = .5f;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 amountToChange = eventData.delta * dragScale;
        mainCamera.transform.Translate(amountToChange.x, -amountToChange.y, 0);
    }

    public void OnScroll(PointerEventData eventData)
    {
        float amountToChange = eventData.scrollDelta.y * zoomScale;
        mainCamera.orthographicSize += amountToChange;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICK");
    }
}