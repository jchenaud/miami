using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColor : MonoBehaviour
{
    void Update()
    {
        Camera.main.backgroundColor = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1.0f));
    }
}
