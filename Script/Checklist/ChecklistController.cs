using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChecklistController : MonoBehaviour
{
    public static ChecklistController Instance;

    [Header("UI References")]
    [SerializeField]private GameObject checklistPanel;
    //[SerializeField]private GameObject popupPanel;
    [SerializeField]private TextMeshProUGUI titleText;
    [SerializeField]private Transform contentParent;               // Content under ScrollView
    [SerializeField]private Button submitButton;
    [SerializeField]private Button closeButton;
    [SerializeField] private ScrollRect scrollRect;
    
    private string requiredWarningMessage;


    [Header("Prefabs")]
    [SerializeField]private ChecklistItemUI checklistItemPrefab;

    private List<ChecklistItemUI> spawnedItems = new List<ChecklistItemUI>();

    private Action<List<ChecklistData>> onSubmitCallback;
    private Action onCloseCallback;

    [SerializeField] private ChecklistItemPool itemPool;
    
    [SerializeField] private FloatingWindowController floatingWindowController;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        checklistPanel.SetActive(false);

        submitButton.onClick.AddListener(OnSubmit);
        closeButton.onClick.AddListener(OnClose);
    }

    public void Show(ChecklistRoot checklistRoot, Action<List<ChecklistData>> submitCallback, Action closeCallback = null, string requiredWarningMessage = "Please complete all required fields.")
    {
        checklistPanel.SetActive(true);

        //Title comes from ChecklistRoot
        titleText.text = checklistRoot.title;

        onSubmitCallback = submitCallback;
        onCloseCallback = closeCallback;

        itemPool.ReturnAllObjects();
        spawnedItems.Clear();
        floatingWindowController.isMaximized = false;

        //Checklist items come from ChecklistRoot
        foreach (var data in checklistRoot.checklist)
        {
            var itemUI = itemPool.GetObject();

            itemUI.SetLabel(data.label);
            itemUI.toggle.isOn = data.alreadychecked;
            itemUI.SetRequired(data.required);
            itemUI.BindData(data);

            spawnedItems.Add(itemUI);
        }

        // Close button controlled by JSON
        floatingWindowController.SetCloseButtonVisibility(
            checklistRoot.showCloseButton
        );

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            contentParent.GetComponent<RectTransform>()
        );
    }


    private void OnSubmit()
    {
        foreach (var itemUI in spawnedItems)
        {
            if (!itemUI.gameObject.activeSelf)
                continue;

            var data = itemUI.GetData();

            // If required but NOT selected → show warning
            if (data.required && !itemUI.IsSelected())
            {
                checklistPanel.SetActive(false);
                PopUpHandler.Instance.ShowPopup("Incomplete Checklist", "Please check all the required options", () =>
        {
            checklistPanel.SetActive(true);   // <-- restore the checklist panel
        }, null, true);
                return; // stop submit
            }
        }

        List<ChecklistData> updatedData = new List<ChecklistData>();

        foreach (var itemUI in spawnedItems)
        {
            if (!itemUI.gameObject.activeSelf)
            continue; // ignore pooled inactive items
            ChecklistData data = itemUI.GetData();
            data.currentstatus = itemUI.IsSelected();
            updatedData.Add(data);
        }
        Debug.Log("=== CHECKLIST SUBMIT RESULTS ===");
        foreach (var d in updatedData)
        {
            Debug.Log($"ID: {d.id}, Label: {d.label}, Required: {d.required}, Selected: {d.currentstatus}");
        }
        onSubmitCallback?.Invoke(updatedData);
        Hide();
    }
    
    private void OnClose()
    {
        onCloseCallback?.Invoke();
        Hide();
    }

    
    private void Hide()
    {
        checklistPanel.SetActive(false);
        onSubmitCallback = null;
        onCloseCallback = null;
    }
}