using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistItemUI : MonoBehaviour
{
    public Toggle toggle;
    public TextMeshProUGUI label;

    public TextMeshProUGUI stepnumber;

    [SerializeField]
    private Button _noteButton;

    [SerializeField]
    private GameObject _noteInputArea;

    [SerializeField]
    private TMP_InputField _noteInputField;
    private ChecklistData boundData;
    private ChecklistItemNoteController _noteController;

    public void SetLabel(string text)
    {
        label.text = text;
    }

    public void SetId(string id)
    {
        stepnumber.text = id;
    }
    public void SetRequired(bool isRequired)
    {
        if (isRequired)
        {
            label.text += " <color=#FF0000>*</color>";
        }
    }

    //public void BindData(ChecklistData data)
    //{
    //    boundData = data;
    //}

    public ChecklistData GetData()
    {
        return boundData;
    }

    public bool IsSelected()
    {
        return toggle.isOn;
    }

    public void BindData(ChecklistData data)
    {
        boundData = data;
        toggle.onValueChanged.RemoveAllListeners();
        toggle.SetIsOnWithoutNotify(data.currentstatus);
        toggle.onValueChanged.AddListener(OnToggleChanged);

        _noteInputArea.SetActive(false);
        _noteInputField.text = data.note ?? string.Empty;

        _noteButton.onClick.RemoveAllListeners();
        _noteButton.onClick.AddListener(OnNoteButtonClicked);

        _noteInputField.onValueChanged.RemoveAllListeners();
        _noteInputField.onValueChanged.AddListener(OnNoteChanged);
    }

    private void OnToggleChanged(bool value)
    {
        boundData.currentstatus = value;
    }

    // private void OnNoteButtonClicked()
    // {
    //     _noteInputArea.SetActive(!_noteInputArea.activeSelf);
    // }

    private void Awake()
    {
        _noteController = GetComponent<ChecklistItemNoteController>();
    }

    private void OnNoteButtonClicked()
    {
        _noteController?.HandleNoteButtonClicked();
    }

    private void OnNoteChanged(string value)
    {
        if (boundData == null)
            return;
        boundData.note = value;

        _noteController?.RefreshButtonLabel();
    }

    private void OnDestroy()
    {
        _noteButton.onClick.RemoveAllListeners();
        _noteInputField.onValueChanged.RemoveAllListeners();
    }
}
