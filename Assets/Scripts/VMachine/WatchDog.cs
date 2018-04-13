using System;
using System.Threading;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchDog : MonoBehaviour {

    public String comPort = "COM3";
    public int baudRate = 115200;

    private SerialPort port;
    private Thread serialThread;

    void InitThread() {
        ThreadStart serial = new ThreadStart(SerialRead);
        serialThread = new Thread(serial);
        serialThread.Start();
    }

    void InitSerial() {
        port = new SerialPort(comPort);

        port.BaudRate = baudRate;
        port.Parity = Parity.None;
        port.StopBits = StopBits.One;
        port.DataBits = 8;
        port.Handshake = Handshake.None;
        port.RtsEnable = true;

        port.Open();

        if (!port.IsOpen) Debug.LogError("PORT IS NOT OPENED.");
    }

    void CleanUp() {
        serialThread.Abort();
        port.Close();
    }

    void SerialRead() {
        while (true) {
            try {
                string uid = port.ReadLine();
                EventManager.TriggerEvent(EventType.CARD_READ, uid);
            }
            catch (TimeoutException) { }
        }
    }

	void Start () {
        InitSerial();
        InitThread();
	}

    public void OnDisable() {
        CleanUp();
    }
}
