using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IScrollHandler, IDragHandler
{
    public Camera mainCamera;
    private float dragScale = .01f;
    private float zoomScale = .5f;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 amountToChange = eventData.delta * dragScale;
        mainCamera.transform.Translate(amountToChange.x, -amountToChange.y, 0);
    }
    public void OnScroll(PointerEventData eventData)
    {
        float amountToChange = eventData.scrollDelta.y * zoomScale;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + amountToChange, 1, 10);
    }
    Vector2 GetMousePos() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }


}