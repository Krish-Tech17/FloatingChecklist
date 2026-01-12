
/// Working version with prefab minimized size capture

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class FloatingWindowController : MonoBehaviour, IDragHandler
{
    [Header("Window")]
    [SerializeField] private RectTransform floatingChecklistWindow;

    [Header("Sizes")]
    [SerializeField] private Vector2 maximizedSize = new Vector2(1200, 1000);

    private Vector2 minimizedSize;
    private Vector2 minimizedPosition;

    [Header("UI")]
    [SerializeField] private GameObject closeButton;
    [SerializeField] private Image toggleButtonImage;
    [SerializeField] private Sprite maximizeSprite;
    [SerializeField] private Sprite minimizeSprite;
    [SerializeField] private ScrollRect checklistScroll;


    public bool isMaximized = false;

    void Start()
    {
        //Capture prefab/ runtime minimized state
        minimizedSize = floatingChecklistWindow.sizeDelta;
        minimizedPosition = floatingChecklistWindow.anchoredPosition;
    }

    void OnEnable()
    {
        StartCoroutine(ResetNextFrame());
    }

    private IEnumerator ResetNextFrame()
    {
        yield return null; // wait 1 frame for layout calculation

        ForceMinimizedState();
        ResetScroll();
    }


    private void ForceMinimizedState()
    {
        isMaximized = false;

        floatingChecklistWindow.sizeDelta = minimizedSize;
        floatingChecklistWindow.anchoredPosition = minimizedPosition;

        UpdateToggleIcon();
    }

    private void ResetScroll()
    {
        if (checklistScroll == null)
            return;

        // Vertical scroll → top
        checklistScroll.verticalNormalizedPosition = 1f;

        // (Optional) horizontal safety
        checklistScroll.horizontalNormalizedPosition = 0f;
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
        floatingChecklistWindow.sizeDelta = maximizedSize;
        floatingChecklistWindow.anchoredPosition = Vector2.zero;
    }


    public void ApplyMinimized()
    {
        floatingChecklistWindow.sizeDelta = minimizedSize;
        floatingChecklistWindow.anchoredPosition = minimizedPosition;
    }

    public void SetCloseButtonVisibility(bool show)
    {
        if (closeButton != null)
            closeButton.SetActive(show);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMaximized)
            floatingChecklistWindow.anchoredPosition += eventData.delta;
    }

    private void UpdateToggleIcon()
    {
        if (toggleButtonImage == null) return;
        toggleButtonImage.sprite = isMaximized ? maximizeSprite : minimizeSprite;
    }

    public void ResetToMinimized()
    {
        ApplyMinimized();
        isMaximized = false;
        UpdateToggleIcon();
    }
}
