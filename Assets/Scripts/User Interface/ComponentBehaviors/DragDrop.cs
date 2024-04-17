using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler
{
    private LayoutHelper layoutHelper;
    private Vector2 startPosition;
    public bool overObject = false; //s TODO - is this unused?


    public void Start()
    {
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
        SetHighlightForDrag(true);
    }
    public void OnMouseDrag()
    {
        transform.position = layoutHelper.GetBoundedPosition(new Vector3(GetMousePos().x, GetMousePos().y, transform.position.z));
    }
    public void OnMouseUp()
    {
        SetHighlightForDrag(false);
        // Enable Snapping and check for overlapping object tiles
        if (gameObject.CompareTag("Tiles")){
            if (overObject){
                transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
            } else {
                var currentPos = transform.position;
                transform.position = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), transform.position.z);
            }
        }
    }
    public void OnMouseOver() {
        if (Input.GetMouseButtonDown(1))
            Destroy(gameObject); //TODO - do this through GameDefinitionManager, to ensure that cache remains valid 
    }
    Vector2 GetMousePos() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

    private void SetHighlightForDrag(bool dragging){
        if (dragging) gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
    }
}
