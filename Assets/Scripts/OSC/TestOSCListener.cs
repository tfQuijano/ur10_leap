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
    public Transform forearm;
    public Transform wrist1;
    public Transform wrist2;
    public Transform wrist3;
    public Transform eef;


    public Transform eff;



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

        float angle = message.GetFloat(0);
        shoulder.eulerAngles = new Vector3(shoulder.eulerAngles.x, angle, shoulder.eulerAngles.z); 

        float angle = message.GetFloat(1);
        forearm.eulerAngles = new Vector3(forearm.eulerAngles.x, angle, forearm.eulerAngles.z); 

        float angle = message.GetFloat(2);
        wrist1.eulerAngles = new Vector3(wrist1.eulerAngles.x, angle, wrist1.eulerAngles.z); 

        float angle = message.GetFloat(3);
        wrist2.eulerAngles = new Vector3(wrist2.eulerAngles.x, angle, wrist2.eulerAngles.z); 

        float angle = message.GetFloat(4);
        wrist3.eulerAngles = new Vector3(wrist3.eulerAngles.x, angle, wrist3.eulerAngles.z); 

        float angle = message.GetFloat(5);
        eef.eulerAngles = new Vector3(eef.eulerAngles.x, angle, eef.eulerAngles.z); 



    }

  
}