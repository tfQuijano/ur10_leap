using UnityEngine;
using System.Collections;
/*  
"/joints"  as six 6 floats 
"/tcp"  xyz as 3 float
"/orient" 4 float quaernion"

*/  
public class TestOSCListener : MonoBehaviour
{

    public OSC osc;
    public Transform shoulder;
    public Transform upperArm;
    public Transform forearm;
    public Transform wrist1;
    public Transform wrist2;
    public Transform wrist3;
    public Transform eef;



    // Use this for initialization
    void Start()
    {
        // set address hadler to joint names, then on receive, get and angle...another script will set the angle)
        osc.SetAddressHandler("/joints", OnReceiveAngle);  //y axis rotation
 
                // set address handler to end effector location  in xyz
        osc.SetAddressHandler("/tcp", OnReceiveTCP);
        osc.SetAddressHandler("/orient", OnReceiveOrient);
        }

    // Update is called once per frame
    void Update()
    {

    }

    void OnReceiveTCP(OscMessage message)
    {
        float x = message.GetFloat(0);
        float y = message.GetFloat(1);
        float z = message.GetFloat(2);

        transform.position = new Vector3(x, y, z);
    }
    // Change this function to receive and rotation angle message for end effector
    void OnReceiveOrient(OscMessage message) { }
   /*{
        float x = message.GetFloat(0);
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
    }*/

    void OnReceiveAngle(OscMessage message)
    {
        string topic = message.address;
       
        // get a list of joint anglse and assign to joints

        // Transform shoulder; X axis rotation
        // Transform forearm   Y axis rotation
        // Transform wrist1    Y axis rotation
        // Transform wrist2    Y axis rotation
        // Transform wrist3    X axis rotation

        float angle0 = message.GetFloat(0);
        shoulder.eulerAngles = new Vector3(shoulder.eulerAngles.x, angle0, shoulder.eulerAngles.z); 

        float angle1 = message.GetFloat(2);
        forearm.eulerAngles = new Vector3(upperArm.eulerAngles.x, angle1, upperArm.eulerAngles.z);

        float angle2 = message.GetFloat(2);
        forearm.eulerAngles = new Vector3(forearm.eulerAngles.x, angle2, forearm.eulerAngles.z);

        float angle3 = message.GetFloat(3);
        wrist1.eulerAngles = new Vector3(wrist1.eulerAngles.x, angle3, wrist1.eulerAngles.z); 

        float angle4 = message.GetFloat(4);
        wrist2.eulerAngles = new Vector3(wrist2.eulerAngles.x, angle4, wrist2.eulerAngles.z); 

        float angle5 = message.GetFloat(5);
        wrist3.eulerAngles = new Vector3(wrist3.eulerAngles.x, angle5, wrist3.eulerAngles.z); 

        



    }

  
}