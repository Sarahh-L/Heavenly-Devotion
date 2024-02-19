using Systems.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputPanel : MonoBehaviour
{
    #region Variables
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private TMP_InputField inputField;

    private CanvasGroupController cg;
    #endregion

    void Start()
    {
        cg = new CanvasGroupController(this, canvasGroup);

        canvasGroup.alpha = 0;
        SetCanvasState(active: false);
        acceptButton.gameObject.SetActive(false);

        inputField.onValueChange.AddListener(OnInputChanged);
        acceptButton.onClick.AddListener(OnAcceptInput);
    }

    public void Show(string title)
    {
        titleText.text = title;
        inputField.text = string.Empty;
        cg.Show();
        SetCanvasState(active: true);
    }

    public void Hide()
    {
        cg.Hide();
        SetCanvasState(active: false);
    }

    public void OnAcceptInput()
    {
        if (inputField.text == string.Empty)
            return;
        
        lastInput = inputField.text;
        Hide();
    }

    private void SetCanvasState(bool active)
    {
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }

    public void OnInputChanged(string value)
    {
        acceptButton.gameObject.SetActive(HasValidText());
    }

    private bool HasValidText()
    {
        return inputField.text != string.Empty;
    }
}