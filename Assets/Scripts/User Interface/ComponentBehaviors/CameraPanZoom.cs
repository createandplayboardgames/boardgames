using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IScrollHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private LayoutHelper layoutHelper;
    public Camera mainCamera;
    private float dragScale = .02f;
    private float zoomScale = .5f;
    private PointerEventData.InputButton panButton = PointerEventData.InputButton.Middle;

    public void Start()
    {
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != panButton)
            return;
        if (eventData.button == panButton)
            Cursor.visible = false;
    }
    public void OnPointerUp(PointerEventData eventData)
   {
        if (eventData.button != panButton)
            return;
        if (eventData.button == panButton)
            Cursor.visible = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != panButton)
            return;

        //calculate new position 
        Vector3 cameraCenter = mainCamera.transform.position;
        Vector2 amountToChange = eventData.delta * dragScale;
        mainCamera.transform.position = layoutHelper.GetBoundedPosition(
            new Vector3(cameraCenter.x + amountToChange.x, cameraCenter.y + amountToChange.y, cameraCenter.z));
    }
    public void OnScroll(PointerEventData eventData)
    {
        float amountToChange = eventData.scrollDelta.y * zoomScale;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + amountToChange, 1, 10);
    }

}
