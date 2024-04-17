using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPanZoom : MonoBehaviour, IScrollHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public Camera mainCamera;
    private float dragScale = .02f;
    private float zoomScale = .5f;
    private PointerEventData.InputButton panButton = PointerEventData.InputButton.Middle;

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

        //calculate new position, limit to background boundaries 
        Vector3 cameraCenter = mainCamera.transform.position;
        GameObject board = GameObject.Find("Background");
        Vector3 boardCenter = board.transform.position;
        float   boardWidth  = GetComponent<SpriteRenderer>().bounds.size.x;
        float   boardHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 amountToChange = eventData.delta * dragScale;
        float newX = Mathf.Clamp(cameraCenter.x + amountToChange.x, boardCenter.x - boardWidth/2,  boardCenter.x + boardWidth/2);
        float newY = Mathf.Clamp(cameraCenter.y + amountToChange.y, boardCenter.y - boardHeight/2, boardCenter.y + boardHeight/2);
        mainCamera.transform.position = new Vector3(newX, newY, cameraCenter.z);
    }
    public void OnScroll(PointerEventData eventData)
    {
        float amountToChange = eventData.scrollDelta.y * zoomScale;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + amountToChange, 1, 10);
    }

}
