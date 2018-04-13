using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanelButtonDetails {
    public string title;
    public UnityAction action;
}

public class PanelDetails {
    public string title;
    public string question;
    public PanelButtonDetails successButton;
    public PanelButtonDetails alternativeButton;
}

public class ModalManager : Singleton<ModalManager> {
    
    public GameObject modalPanelObject;
    public Text modalTitle;
    public Text modalQuestion;

    public Button successButton;
    public Button alternativeButton;

    public Text successButtonText;
    public Text alternativeButtonText;

    //private static ModalManager modalPanel;

    //public static ModalManager Instance() {
    //    if (!modalPanel) {
    //        modalPanel = FindObjectOfType(typeof(ModalManager)) as ModalManager;
    //        if (!modalPanel)
    //            Debug.LogError("There needs to be one active ModalManager script on a GameObject in the scene.");
    //    }

    //    return modalPanel;
    //}

    void InitModal() {
        modalPanelObject.SetActive(true);

        successButton.gameObject.SetActive(false);
        alternativeButton.gameObject.SetActive(false);
    }

    void InitQuestion(PanelDetails details) {
        modalTitle.text = details.title;
        modalQuestion.text = details.question;
    }

    void InitButton(Button button, Text buttonText, PanelButtonDetails details) {
        if (details == null)
            return;

        button.gameObject.SetActive(true);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(details.action);

        buttonText.text = details.title;
    }

    public void ShowModal(PanelDetails details) {
        InitModal();
        InitQuestion(details);

        InitButton(successButton, successButtonText, details.successButton);
        InitButton(alternativeButton, alternativeButtonText, details.alternativeButton);
    }

    public void CloseModal() {
        modalPanelObject.SetActive(false);
    }
}
