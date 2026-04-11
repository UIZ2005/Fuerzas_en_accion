using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIraycast : MonoBehaviour
{
    GameObject currentHover;
    // Start is called before the first frame update
    void Update()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2);

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        GameObject newHover = results.Count > 0 ? results[0].gameObject : null;

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var result in results)
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }

        if (newHover != currentHover)
        {
            if (currentHover != null)
                ExecuteEvents.Execute(currentHover, pointerData, ExecuteEvents.pointerExitHandler);

            if (newHover != null)
                ExecuteEvents.Execute(newHover, pointerData, ExecuteEvents.pointerEnterHandler);

            currentHover = newHover;
        }
    }
}
