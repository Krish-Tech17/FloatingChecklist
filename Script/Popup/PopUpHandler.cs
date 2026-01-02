using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopUpHandler : MonoBehaviour
{
    public static PopUpHandler Instance;

    [Header("Main Popup Panel")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button okButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private TextMeshProUGUI okButtonText;
    [SerializeField] private TextMeshProUGUI closeButtonText;

    [Header("Click Overlay for Truncated Text")]
    [SerializeField] private GameObject textOverlayButton;

    [Header("Full Message Panel")]
    [SerializeField] private GameObject fullMessagePanel;
    [SerializeField] private TextMeshProUGUI fullMessageText;
    [SerializeField] private Button fullMessageCloseButton;

     [SerializeField]private int maxCharacters = 120; // Adjust this however you want

     private string originalFullMessage = "";

    private Action onOkCallback;
    private Action onCloseCallback;
    private Action onYesCallback;
    private Action onNoCallback;
    private string currentMessage = "";


    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        popupPanel.SetActive(false);
        fullMessagePanel.SetActive(false);
        textOverlayButton.SetActive(false);

        okButton.onClick.AddListener(OnOkClicked);
        closeButton.onClick.AddListener(OnCloseClicked);
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
        fullMessageCloseButton.onClick.AddListener(OnFullCloseClicked);

        var overlayBtn = textOverlayButton.GetComponent<Button>();
        overlayBtn.onClick.AddListener(ShowFullMessage);
    }

    public void ShowPopup(string title, string msg, Action ok = null, Action close = null, bool isOkButtonRequired = true)
    {
        popupPanel.SetActive(true);

        titleText.text = title ?? "";
        originalFullMessage = msg ?? "";

        if (originalFullMessage.Length > maxCharacters)
        {
            messageText.text = originalFullMessage.Substring(0, maxCharacters) + "...";
            textOverlayButton.SetActive(true);
        }
        else
        {
            messageText.text = originalFullMessage;
            textOverlayButton.SetActive(false);
        }

        onOkCallback = ok;
        onCloseCallback = close;

        // DEFAULT STATE
        okButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);

        //If OK button is required
        if (isOkButtonRequired)
        {
            okButton.gameObject.SetActive(true);
        }
        Canvas.ForceUpdateCanvases();
    }

    public void ShowYesNoPopup(string type, string message, Action yesCallback, Action noCallback, Action closeCallback)
    {
        popupPanel.SetActive(true);

        titleText.text = type ?? "";
        originalFullMessage = message ?? "";

        
        if (originalFullMessage.Length > maxCharacters)
        {
            messageText.text = originalFullMessage.Substring(0, maxCharacters) + "...";
            textOverlayButton.SetActive(true);
        }
        else
        {
            messageText.text = originalFullMessage;
            textOverlayButton.SetActive(false);
        }

        okButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

        onYesCallback = yesCallback;
        onNoCallback = noCallback;
        onCloseCallback = closeCallback;

        Canvas.ForceUpdateCanvases();
    }

    private void OnOkClicked()
    {
        onOkCallback?.Invoke();
        HidePopup();
    }

    private void OnCloseClicked()
    {
        onCloseCallback?.Invoke();
        HidePopup();
    }

    private void OnYesClicked()
    {
        onYesCallback?.Invoke();
        HidePopup();
    }

    private void OnNoClicked()
    {
        onNoCallback?.Invoke();
        HidePopup();
    }

    private void HidePopup()
    {
        popupPanel.SetActive(false);
        textOverlayButton.SetActive(false);

        onOkCallback = null;
        onCloseCallback = null;
    }

    private void ShowFullMessage()
    {

        fullMessageText.text = originalFullMessage;

        popupPanel.SetActive(false);
        fullMessagePanel.SetActive(true);

        Canvas.ForceUpdateCanvases();
    }

    private void OnFullCloseClicked()
    {
        fullMessagePanel.SetActive(false);
        popupPanel.SetActive(true);
    }


    private void RefreshTruncationOverlay()
    {
        Canvas.ForceUpdateCanvases();
        messageText.ForceMeshUpdate();

        bool isTruncated = IsTruncated(messageText);
        textOverlayButton.SetActive(isTruncated);
    }

    private bool IsTruncated(TextMeshProUGUI tmp)
    {
        float preferredHeight = tmp.preferredHeight;
        float rectHeight = tmp.rectTransform.rect.height;

        // TMP word wrap overflow check
        if (tmp.enableWordWrapping)
            return preferredHeight > rectHeight + 0.1f;

        // Single line overflow
        float preferredWidth = tmp.preferredWidth;
        float rectWidth = tmp.rectTransform.rect.width;

        return preferredWidth > rectWidth + 0.1f;
    }

}


