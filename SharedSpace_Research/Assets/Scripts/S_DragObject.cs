using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DragObject : MonoBehaviour

{
    private Vector3 mOffset;
    private float mZCoord;

    void Update()
    {
        // Move object in Z-axis if S is pressed
        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Moving towards screen");
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - 1);
        }

        // Move object in Z-axis if S is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Moving towards screen");
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + 1);
        }
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // Z-coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {

        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
}