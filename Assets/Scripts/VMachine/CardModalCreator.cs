using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardModalCreator : MonoBehaviour {

    public Sprite iconConfirm;
    public Sprite iconClose;
    public Sprite iconRefresh;

    //void GoToInputGasolineValue() {
    //    SceneManager.LoadScene("InputGasolineValue");
    //}

    void WaitGoToFillScene() {
        StartCoroutine(GoToFillScene());
    }

    void GoToSelectGasolineType() {
        EventManager.TriggerEvent(EventType.TRANSACTION_CANCELLED, null);
        SceneManager.LoadScene("SelectGasolineType");
    }

    void Start() {
        GenerateTapEKTP();
    }

    PanelButtonDetails GetCancelButton() {
        PanelButtonDetails alternativeButton = new PanelButtonDetails();
        alternativeButton.title = "Batalkan";
        alternativeButton.action = GoToSelectGasolineType;
        alternativeButton.icon = iconClose;

        return alternativeButton;
    }

    void GenerateTapEKTP() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Tempel E-KTP";
        panel.question = "Tempelkan E-KTP Anda";

        panel.alternativeButton = GetCancelButton();

        ModalManager.instance.CloseModal();
        ModalManager.instance.ShowModal(panel);
    }

    void GenerateWaitForProcess() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Pemrosesan Transaksi";
        panel.question = "Kartu Berhasil Dibaca\nTransaksi Sedang Diproses";

        panel.successButton = new PanelButtonDetails();
        panel.successButton.title = "Mohon Tunggu";
        panel.successButton.action = null;
        panel.successButton.icon = iconRefresh;

        ModalManager.instance.CloseModal();
        ModalManager.instance.ShowModal(panel);
    }

    void GenerateSuccessPanel() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Tempel E-KTP";
        panel.question = "Permintaan Transaksi Berhasil";

        panel.successButton = new PanelButtonDetails();
        panel.successButton.title = "Sukses";
        //panel.successButton.action = WaitGoToFillScene;
        panel.successButton.icon = iconConfirm;


        WaitGoToFillScene(); //TODO: Move this somewhere else
        ModalManager.instance.CloseModal();
        ModalManager.instance.ShowModal(panel);
    }

    void GenerateNotEnoughBalancePanel() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Tempel E-KTP";
        panel.question = "Saldo Tidak Mencukupi\nSilakan Tempel E-KTP Lain";

        panel.alternativeButton = GetCancelButton();

        ModalManager.instance.CloseModal();
        ModalManager.instance.ShowModal(panel);
    }

    void GenerateError() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Tempel E-KTP";
        panel.question = "Transaksi Tidak Dapat Diproses\nSilakan Tempel E-KTP Anda";

        panel.alternativeButton = GetCancelButton();

        ModalManager.instance.CloseModal();
        ModalManager.instance.ShowModal(panel);
    }

    IEnumerator GoToFillScene() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GasolineFill");
    }

    void OnEnable() {
        EventManager.StartListening(EventType.VALID_CARD_READED, delegate { GenerateWaitForProcess(); });
        EventManager.StartListening(EventType.TRANSACTION_REQUEST_SUCCESS, delegate { GenerateSuccessPanel(); });
        EventManager.StartListening(EventType.TRANSACTION_REQUEST_NOT_ENOUGH_BALANCE, delegate { GenerateNotEnoughBalancePanel(); });
        EventManager.StartListening(EventType.TRANSACTION_REQUEST_ERROR, delegate { GenerateError(); });
    }

    void OnDisable() {
        EventManager.StopListening(EventType.VALID_CARD_READED, delegate { GenerateWaitForProcess(); });
        EventManager.StopListening(EventType.TRANSACTION_REQUEST_SUCCESS, delegate { GenerateSuccessPanel(); });
        EventManager.StartListening(EventType.TRANSACTION_REQUEST_NOT_ENOUGH_BALANCE, delegate { GenerateNotEnoughBalancePanel(); });
        EventManager.StartListening(EventType.TRANSACTION_REQUEST_ERROR, delegate { GenerateError(); });
    }
}
