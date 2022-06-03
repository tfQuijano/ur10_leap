using UnityEngine;
using System.Collections;
using System.Linq;

public class LooktoListener : MonoBehaviour
{
    public OSC osc;
    public Transform LookTo;
    public Transform KinectLocation;
    private string Text;


    // Use this for initialization
    void Start()
    {
        osc.SetAllMessageHandler(OnReceive);



    }

    void OnReceive(OscMessage message)
    {
        Debug.Log("Received:  address: " + message.address + " | Content: " + string.Join(" ", message.values.ToArray()));
        if (message.address.ToLower().Contains("tx"))
        {
            float x = message.GetFloat(0);

            LookTo.position = KinectLocation.TransformPoint(new Vector3(x, LookTo.position.y, LookTo.position.z));
        }
        if (message.address.ToLower().Contains("tz"))
        {
            float z = message.GetFloat(0);
            LookTo.position = KinectLocation.TransformPoint(new Vector3(LookTo.position.x, LookTo.position.y, z));
            
        }
        if (message.address.ToLower().Contains("ty"))
        {
            float y = message.GetFloat(0);

            LookTo.position = KinectLocation.TransformPoint(new Vector3(LookTo.position.x, y, LookTo.position.z));
        }
    }


}