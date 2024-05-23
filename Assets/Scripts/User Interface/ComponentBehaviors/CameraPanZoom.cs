using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IScrollHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private LayoutHelper layoutHelper;
    public Camera mainCamera;
    private float dragScale = .02f;
    private float zoomScale = .5f;
    private PointerEventData.InputButton panButton = PointerEventData.InputButton.Left;
    private PointerEventData.InputButton altPanButton = PointerEventData.InputButton.Middle;
    private PointerEventData.InputButton altZoomButton = PointerEventData.InputButton.Right;
    private KeyCode altKey = KeyCode.LeftControl;

    public void Start(){
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("ON POINTER DWONWN");
        if (PanOrZoomButtonPressed(eventData))
            Cursor.visible = false;
    }
    public void OnPointerUp(PointerEventData eventData){
        if (PanOrZoomButtonPressed(eventData))
            Cursor.visible = true;
    }
    private bool PanOrZoomButtonPressed(PointerEventData eventData){
        return eventData.button == panButton || eventData.button == altPanButton || eventData.button == altZoomButton;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((Input.GetKey(altKey) && eventData.button == altPanButton) || eventData.button == panButton)
            PanCamera(eventData.delta);
        if (Input.GetKey(altKey) && eventData.button == altZoomButton)
            ZoomCamera(eventData.delta);
    }
    public void OnScroll(PointerEventData eventData)    {
        ZoomCamera(eventData.scrollDelta);
    }

    private void PanCamera(Vector2 delta){
        //calculate new position 
        Vector3 cameraCenter = mainCamera.transform.position;
        Vector2 amountToChange = delta * dragScale;
        mainCamera.transform.position = layoutHelper.GetBoundedPosition(
            new Vector3(cameraCenter.x + amountToChange.x, cameraCenter.y + amountToChange.y, cameraCenter.z));
        return;
    }
    private void ZoomCamera(Vector2 delta){
        float amountToChange = delta.y * zoomScale;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + amountToChange, 1, 10);
    }

}
