using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChairControl.ChairWork;
using ChairControl.ChairWork.Options;
public class FuturifController : MonoBehaviour
{
    private FutuRiftController futuRiftController;

    void OnEnable()
    {
        futuRiftController = new FutuRiftController(new UdpOptions
        {
            ip = "192.168.50.125", // ip компьютера, на котором запущен контроллер
            port = 6065 // порт, на который настроен контролле
        });
        futuRiftController.Start();
    }

    void OnDisable()
    {
        futuRiftController.Stop();
    }
    
    void Update()
    {
        var euler = transform.eulerAngles;
        futuRiftController.Pitch = -(euler.x > 180 ? euler.x - 360 : euler.x);
        futuRiftController.Roll = -(euler.z > 180 ? euler.z - 360 : euler.z);
    }
}
