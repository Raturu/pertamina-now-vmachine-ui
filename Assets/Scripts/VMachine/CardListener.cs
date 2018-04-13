using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum REQUEST_STATE {
    SUCCESS,
    NOT_ENOUGH_BALANCE,
    ERROR
}

public class CardListener : MonoBehaviour {


    REQUEST_STATE ValidateCard(string uid) {
        //TODO: HTTP Request bla-bla
        Debug.Log("Request for");
        Debug.Log(uid);
        Debug.Log(VMachineApplication.instance.gasolineValue);

        if (uid == "80:E8:80:A4")
            return REQUEST_STATE.NOT_ENOUGH_BALANCE;
        else
            return REQUEST_STATE.SUCCESS;
    }

    PanelDetails GenerateSuccessPanel() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Tempel E-KTP";
        panel.question = "Permintaan Transaksi Berhasil";

        panel.successButton = new PanelButtonDetails();
        panel.successButton.title = "Sukses";
        //panel.successButton.action = null;

        return panel;
    }

    PanelDetails GenerateNotEnoughBalancePanel() {
        PanelDetails panel = new PanelDetails();

        panel.title = "Tempel E-KTP";
        panel.question = "Saldo Tidak Mencukupi\nSilakan Tempel E-KTP Lain";

        panel.alternativeButton = new PanelButtonDetails();
        panel.alternativeButton.title = "Batalkan";
        //panel.alternativeButton.action = null;

        return panel;
    }

    void ProcessCard(object uid) {
        REQUEST_STATE state = ValidateCard((string)uid);
        ModalManager.instance.CloseModal();

        switch (state) {
            case REQUEST_STATE.SUCCESS:
                ModalManager.instance.ShowModal(GenerateSuccessPanel());
                EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_SUCCESS, null);
                break;
            case REQUEST_STATE.NOT_ENOUGH_BALANCE:
                ModalManager.instance.ShowModal(GenerateNotEnoughBalancePanel());
                break;
            case REQUEST_STATE.ERROR:
                break;
            default:
                break;
        }
    }

    void CardPrint(object uid) {
        UnityMainThreadDispatcher.Instance().Enqueue(() => ProcessCard(uid));
    }

	void OnEnable () {
        EventManager.StartListening(EventType.CARD_READ, CardPrint);
	}

    void OnDisable() {
        EventManager.StopListening(EventType.CARD_READ, CardPrint);
    }
	
}
