using UnityEngine;
using System.Collections;
/*  
"/joints"  as six 6 floats 
"/tcp"  xyz as 3 float
"/orient" 4 float quaernion"

*/
public class UR10OSCListener : MonoBehaviour
{
    public OSC osc;
    public Transform shoulder; // y axis
    public Transform upperArm; // x axis
    public Transform forearm; // x axis
    public Transform wrist1; // x axis
    public Transform wrist2; //
    public Transform wrist3;
    public Transform eef;

    public bool anglesInDeg;


    // Use this for initialization
    void Start()
    {

        //osc.SetAllMessageHandler(OnReceive);
        osc.SetAddressHandler("/joints", OnReceiveAngle);  //y axis rotation
        //osc.SetAddressHandler("/joint_4", OnReceiveJoint4);  //y axis rotation
                                                           // set address handler to end effector location  in xyz
        //osc.SetAddressHandler("/tcp", OnReceiveTCP);
        //osc.SetAddressHandler("/orient", OnReceiveOrient);
        }

 
    void OnReceive(OscMessage message)
    {
        Debug.Log("Received:  address: " + message.address + " | Content: " + string.Join(" ", message.values.ToArray()));
       
    }


    /*void OnReceiveTCP(OscMessage message)
    {
        float x = message.GetFloat(0);
        float y = message.GetFloat(1);
        float z = message.GetFloat(2);

        transform.position = new Vector3(x, y, z);
    }
    /* Change this function to receive and rotation angle message for end effector
    void OnReceiveOrient(OscMessage message) { }
   {
        float x = message.GetFloat(0);
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
    }*/

    void OnReceiveAngle(OscMessage message)
    {
        string topic = message.address;
        Debug.Log("Received:  address: " + message.address + " | Content: " + string.Join(" ", message.values.ToArray()));

        // get a list of joint anglse and assign to joints

        // Transform shoulder; X axis rotation
        // Transform forearm   Y axis rotation
        // Transform wrist1    Y axis rotation
        // Transform wrist2    Y axis rotation
        // Transform wrist3    X axis rotation -not used
        float scalar = (anglesInDeg) ? 1 : Mathf.Rad2Deg;

        float angle0 = (message.GetFloat(0)* scalar + 180 - 180 + 360) % 360 - 180;
        shoulder.localEulerAngles = new Vector3(shoulder.localEulerAngles.x, angle0, shoulder.localEulerAngles.z); // x axis
        
        float angle1 = (message.GetFloat(1) * scalar + 180 + 90 + 360) % 360 - 180;
        upperArm.localEulerAngles = new Vector3(angle1, upperArm.localEulerAngles.y, upperArm.localEulerAngles.z); // y axis

        float angle2 = (message.GetFloat(2) * scalar + 360) % 360 - 180;
        forearm.localEulerAngles = new Vector3(angle2, forearm.localEulerAngles.y, forearm.localEulerAngles.z); // y axis

        float angle3 = (message.GetFloat(3) * scalar + 180 + 360) % 360 - 180;
        //wrist1.localEulerAngles = new Vector3(angle3, wrist1.localEulerAngles.y, wrist1.localEulerAngles.z); 
        wrist1.localEulerAngles = new Vector3(angle3, wrist1.localEulerAngles.y, wrist1.localEulerAngles.z); 

        float angle4 = (message.GetFloat(4) * scalar + 180 + 360) % 360 - 180;
        wrist2.localEulerAngles = new Vector3(wrist2.localEulerAngles.x, angle4, wrist2.localEulerAngles.z);

        float angle5 = (message.GetFloat(5) * scalar +180+ 360) % 360 - 180;
        wrist3.localEulerAngles = new Vector3(angle5, wrist3.localEulerAngles.y, wrist3.localEulerAngles.z);

        Debug.Log(angle0.ToString("F3") + "," + angle1.ToString("F3") + "," + angle2.ToString("F3") + "," + angle3.ToString("F3") + "," + angle4.ToString("F3") + "," + angle5.ToString("F3") );

        

    }

    //void OnReceiveJoint4(OscMessage message)
    //{
    //    string topic = message.address;

 


    //}


}