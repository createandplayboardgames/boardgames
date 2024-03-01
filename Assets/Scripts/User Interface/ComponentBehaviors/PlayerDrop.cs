using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerDrop : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Collider2D[] overlapping = Physics2D.OverlapCircleAll(transform.position, 1f);
        SnapToTile(null); //unsnap
        foreach (Collider2D collider in overlapping)
        {
            if (collider.gameObject == gameObject) //skip self
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
            LayoutPlayersOnTile(tileData);
        }
    }

    private void LayoutPlayersOnTile(TileData tileData)
    {
        List<PlayerData> playersOnTile = new(); 
        // what players are on the tile
        foreach (PlayerData player in FindObjectOfType<GameDefinitionManager>().players){
            if (player.location == tileData)
                playersOnTile.Add(player);
        }
        int count = playersOnTile.Count;
        if (count == 0) // nothing to do
            return;
        if (count == 1){ // center it
            var t = gameObject.transform;
            t.position = tileData.gameObject.transform.position; //position
            t.parent = tileData.gameObject.transform; //parent;
        }
        else // layout in a circle
        {
            for (int i = 0; i < count; i++)
            {
                //first, center
                var t = playersOnTile[i].gameObject.transform;
                t.position = tileData.gameObject.transform.position; //position
                t.parent = tileData.gameObject.transform; //parent;

                float angleIncrement = 360 / count;
                float startAngle = 180;
                //calculate deltaX and deltaY
                float degrees = (i * angleIncrement + startAngle) % 360; 
                float radians = degrees * (Mathf.PI / 180);
                float radius = (tileData.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f) 
                    - (playersOnTile[i].GetComponent<SpriteRenderer>().bounds.size.x / 2.0f);
                float deltaX = radius * Mathf.Cos(radians);
                float deltaY = radius * Mathf.Sin(radians);
                //move 
                t.position = new Vector3(t.position.x + deltaX, t.position.y + deltaY, t.position.z);
            }

            // NOTE - scaling shouldn't be needed, assuming player image size is limited
            

        }
    }
 
}
