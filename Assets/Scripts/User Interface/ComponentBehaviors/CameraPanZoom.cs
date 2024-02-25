using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IScrollHandler, IDragHandler, IPointerDownHandler
{
    public Camera mainCamera;
    private float dragScale = .01f;
    private float zoomScale = .5f;


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("DOWN");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG");
        Vector2 amountToChange = eventData.delta * dragScale;
        mainCamera.transform.Translate(amountToChange.x, -amountToChange.y, 0);
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("SCROLL");
        float amountToChange = eventData.scrollDelta.y * zoomScale;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + amountToChange, 1, 10);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICK");
    }

    Vector2 GetMousePos() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }


}