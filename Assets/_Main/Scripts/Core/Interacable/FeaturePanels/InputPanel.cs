using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputPanel : MonoBehaviour
{
    public static InputPanel instance { get; private set; } = null;
    
    
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private TMP_InputField _inputField;

    private CanvasGroupController cg;

    public string lastInput { get; private set; } = string.Empty;
    
    public bool isWaitingOnUserInput { get; private set; } 
    
    public static bool isVisable { get; private set; } 
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
       
    }

    private void Start()
    {
        cg = new CanvasGroupController(this, _canvasGroup);
        cg.SetInteractableState(false);
        acceptButton.gameObject.SetActive(false);
        _inputField.onValueChanged.AddListener(OnInputChanged);
        acceptButton.onClick.AddListener(OnAcceptInput);
    }

    public void Show(string tile)
    {
        titleText.text = tile;
        _inputField.text = string.Empty;
        cg.Show();
        cg.SetInteractableState(true);
        isWaitingOnUserInput = true;
        isVisable = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        PlayerMovement.lockMovement = true;
    }

    public void OnAcceptInput()
    {
        if (_inputField.text != string.Empty)
        {
            Tresor.CheckCode(_inputField.text);
            lastInput = _inputField.text;
            Hide();
            NoteManager.instance.notebookContainer.Hide();
        }
       

    }

    public void Hide()
    {
        cg.Hide();
        isVisable = false;
        cg.SetInteractableState(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerMovement.lockMovement = false;
        isWaitingOnUserInput = false;
    }

    public void OnInputChanged(string value)
    {
        acceptButton.gameObject.SetActive(HasValidText());
    }
    
    private bool HasValidText()
    {
        return _inputField.text != string.Empty && _inputField.text.Length == 6;
    }
    
}
