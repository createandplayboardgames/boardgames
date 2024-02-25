using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrop : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Collider2D[] overlapping = Physics2D.OverlapCircleAll(transform.position, 1f);
        SnapToTile(null); //unsnap
        foreach (Collider2D collider in overlapping)
        {
            if (collider.gameObject == gameObject)
                continue;
            if (collider.gameObject.GetComponent<TileData>() != null)
            { //overlaps with Tile
                SnapToTile(collider.gameObject.GetComponent<TileData>());
                break;
            }
        }
    }

    public void SnapToTile(TileData tileData)
    {
        if (tileData == null)
        { //unset
            GetComponent<PlayerData>().location = null; // location
            GetComponent<PlayerData>().transform.parent = null; // parent
        } else
        { //set
            GetComponent<PlayerData>().location = tileData; //location
            gameObject.transform.position = tileData.gameObject.transform.position; //position
            gameObject.transform.parent = tileData.gameObject.transform; //parent
        }
    }

    
}
