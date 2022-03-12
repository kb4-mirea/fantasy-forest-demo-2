using System;
using UnityEngine;

public class ThumbstickAngleControl : MonoBehaviour
{
    [SerializeField] private bool EnableAxisX;
    [SerializeField] private bool EnableAxisZ ;
    [SerializeField] private float MaxAngle = 30;

    private double EPS = 0.0001;
    
    Vector2 GetControllerInput()
    {
        Vector2 controllerInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        return controllerInput;
    }
    
    void Update()
    {
        Quaternion curRot = transform.rotation;
        Vector2 controllerInput = GetControllerInput();

        Vector2 rotVec = new Vector2();
        
        if (controllerInput == Vector2.zero)
        {
            if (EnableAxisX)
            {
                rotVec.x = Math.Abs(curRot.x) > EPS ? -180 * curRot.x / MaxAngle : 0;
            }

            if (EnableAxisZ)
            {
                rotVec.y = Math.Abs(curRot.z) > EPS ? -180 * curRot.z / MaxAngle: 0;
            }
        }
        else
        {
            if (EnableAxisX && Math.Abs(curRot.x * 180) < MaxAngle)
            {
                var toPointX = controllerInput.y * MaxAngle;
                var distance = toPointX - curRot.x * 180;

                if (Math.Abs(distance) > EPS) rotVec.x = distance / MaxAngle;
            }
            
            if (EnableAxisZ && Math.Abs(curRot.z * 180) < MaxAngle)
            {
                var toPointZ = -controllerInput.x * MaxAngle;
                var distance = toPointZ - curRot.z * 180;

                if (Math.Abs(distance) > EPS) rotVec.y =  distance / MaxAngle;
            }
        }
        
        transform.Rotate(rotVec.x, 0.0f, rotVec.y, Space.World);
    }
}