using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public DropZone currentDropZone;
    public DropZone initialZone;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition;
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    void Start()
    {
        if (initialZone != null)
        {
            initialZone.currentItem = this;
            currentDropZone = initialZone;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        if (currentDropZone != null)
            currentDropZone.currentItem = null;

        Vector3 worldPos = transform.position;
        transform.position = worldPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        RectTransform canvasRect = canvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropZone detectedZone = null;

        foreach (RaycastResult result in results)
        {
            detectedZone = result.gameObject.GetComponent<DropZone>();

            if (detectedZone != null)
                break;
        }

        if (detectedZone != null)
        {
            detectedZone.PlaceItem(this);
        }
        else
        {
            ReturnToInitialZone();
        }
    }

    public void ReturnToInitialZone()
    {
        initialZone.PlaceItem(this);
    }

    public void SnapToZone(DropZone zone)
    {
        transform.position = zone.transform.position;
        currentDropZone = zone;
    }
}