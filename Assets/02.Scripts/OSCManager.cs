using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UIElements;

public class OSCManager : MonoBehaviour
{
    public OSC _sensorOSC;      
    public Vector2 RectSize;
    public List<Vector3> _position = new List<Vector3>();


    private void Start()
    {
        SetOSC_Event();
    }

    public void getStartMessage(OscMessage message)
    {
        RectSize = new Vector2(message.GetFloat(0), message.GetFloat(1));
        _position.Clear();
    }

    public void getSensorMessage(OscMessage message)
    {
        _position.Add(new Vector3(message.GetFloat(0), message.GetFloat(1), 0));
    }

    public void getStopMessage(OscMessage message)
    {
        //Debug.Log(message.GetInt(0));
    }

    void SetOSC_Event()
    {
        // ¼¾¼­
        _sensorOSC.SetAddressHandler("/Down/Start", getStartMessage);
        _sensorOSC.SetAddressHandler("/Down/Data", getSensorMessage);
        _sensorOSC.SetAddressHandler("/Down/End", getStopMessage);
    }
}
