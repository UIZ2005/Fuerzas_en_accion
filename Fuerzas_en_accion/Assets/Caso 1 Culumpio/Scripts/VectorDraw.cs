using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDraw : MonoBehaviour
{
    public RectTransform canvasRect;
    public RectTransform[] linePrefab;

    private RectTransform currentLine;
    private Vector2 startPoint;
    private bool isDragging = false;
    private int N = 5;
    private float distance;

    void Update()
    {
        if (N < 5)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    Input.mousePosition,
                    null,
                    out localPoint
                );

                startPoint = localPoint;
                isDragging = true;

                currentLine = Instantiate(linePrefab[N], canvasRect);
            }

            if (isDragging && Input.GetMouseButton(0))
            {
                Vector2 currentPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    Input.mousePosition,
                    null,
                    out currentPoint
                );

                UpdateLine(startPoint, currentPoint);
            }

            if (isDragging && Input.GetMouseButtonUp(0))
            {
                if (distance < 80)
                {
                    Destroy(currentLine.gameObject);
                }
                else
                {
                    N = 5;
                }
                isDragging = false;
                currentLine = null; // ya queda fija  
            }
        }
       
    }

    void UpdateLine(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        distance = direction.magnitude;
  
        currentLine.sizeDelta = new Vector2(distance, 30f);

        currentLine.anchoredPosition = start + direction / 2f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentLine.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void cambiarlinea(int linea)
    {
        N = linea;
    }
}
