using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//писала час
//а потом поняла, что имелось ввиду не по наведению, а просто по перемещению мыши) через минуту исправила
public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 bounds = new Vector2(-2, 2);

    private Camera cam;
    //дистанция между камерой и игроком-платформой
    private float distance;
    private Vector3 mouseOffset;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        distance = (cam.transform.position - transform.position).magnitude;
        startPos = transform.position;
    }

    void Update()
    {
        Drag();
    }
   /*private void OnMouseOver()
    {
        mouseOver = true;
        if (Input.GetMouseButton(0)) return;
        Drag();
    }
    private void OnMouseDrag()
    {
        Drag();
    }
    private void OnMouseEnter()
    {
        OffsetCalculate();
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        OffsetCalculate();
        mouseOver = false;
    }
    private void OnMouseDown()
    {
        OffsetCalculate();
        mouseOver = true;
    }
    private void OffsetCalculate()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }*/
    private void Drag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) + mouseOffset;
        startPos.z = Mathf.Clamp(objPosition.z, bounds.x, bounds.y);
        transform.position = startPos;

    }

    
}
