//using UnityEngine;
//using UnityEngine.EventSystems;

//public class FloatingWindowController : MonoBehaviour, IDragHandler
//{
//    [SerializeField] private RectTransform window;
//    [SerializeField] private Vector2 minimizedSize = new Vector2(300, 200);
//    [SerializeField] private Vector2 maximizedSize = new Vector2(800, 600);

//    private bool isMaximized = false;
//    private Vector2 lastPosition;

//    public void ToggleMaximize()
//    {
//        Debug.Log("MAX/MIN BUTTON CLICKED");

//        isMaximized = !isMaximized;

//        Debug.Log("Before: " + window.sizeDelta);

//        if (isMaximized)
//        {
//            lastPosition = window.anchoredPosition;
//            window.sizeDelta = maximizedSize;
//            window.anchoredPosition = Vector2.zero;
//        }
//        else
//        {
//            window.sizeDelta = minimizedSize;
//            window.anchoredPosition = lastPosition;
//        }
//        Debug.Log("After: " + window.sizeDelta);
//    }


//    public void OnDrag(PointerEventData eventData)
//    {
//        window.anchoredPosition += eventData.delta;
//    }
//}

/// Working version with prefab minimized size capture

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;   

public class FloatingWindowController : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform window;
    [SerializeField] private Vector2 maximizedSize = new Vector2(800, 600);

    private Vector2 minimizedSize;
    private Vector2 minimizedPosition;

    [SerializeField] private GameObject closeButton;
    [SerializeField] private Image toggleButtonImage;
    [SerializeField] private Sprite maximizeSprite;
    [SerializeField] private Sprite minimizeSprite;


    private bool isMaximized = false;

    void Start()
    {
        // Capture REAL minimized state from prefab/runtime
        minimizedSize = window.sizeDelta;
        minimizedPosition = window.anchoredPosition;

        // Start minimized (your requirement)
        ApplyMinimized();
    }

    public void ToggleMaximize()
    {
        if (isMaximized)
            ApplyMinimized();
        else
            ApplyMaximized();

        isMaximized = !isMaximized;
        UpdateToggleIcon();
    }

    private void ApplyMaximized()
    {
        window.sizeDelta = maximizedSize;
        window.anchoredPosition = Vector2.zero;
    }

    public void SetCloseButtonVisibility(bool show)
    {
        if (closeButton != null)
            closeButton.SetActive(show);
    }


    private void ApplyMinimized()
    {
        window.sizeDelta = minimizedSize;
        window.anchoredPosition = minimizedPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMaximized)
            window.anchoredPosition += eventData.delta;
    }

    private void UpdateToggleIcon()
    {
        if (toggleButtonImage == null) return;

        // Show opposite action icon
        toggleButtonImage.sprite = isMaximized ? maximizeSprite : minimizeSprite;
    }
}


/// Working with required number of checklist options visible in maximised view


//    private void ApplyMaximized()
//    {
//        float itemHeight = checklistItemPrefab.sizeDelta.y;

//        float height =
//            (itemHeight * maxVisibleItems)
//            + titleBarHeight
//            + bottomBarHeight
//            + verticalPadding;

//        window.sizeDelta = new Vector2(windowWidth, height);
//        window.anchoredPosition = Vector2.zero;
//    }


//    void ApplyMinimized()
//    {
//        window.sizeDelta = minimizedSize;
//        window.anchoredPosition = minimizedPosition;
//        isMaximized = false;
//    }

