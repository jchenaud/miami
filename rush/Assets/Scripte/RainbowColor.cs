using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColor : MonoBehaviour
{
    public List<Color> listColors;
    int i;
    float t;

    void Start()
    {
        // listColors = new List<Color>();
        // listColors.Add(Color.red);
        // listColors.Add(Color.blue);
        // listColors.Add(Color.green);
        i = 0;
    }

    void Update()
    {
        Color nexColor = listColors[(i + 1) % listColors.Count];
        t += Time.deltaTime;
        Camera.main.backgroundColor = Color.Lerp(listColors[i], nexColor, t);
        if (t >= 1.0f)
        {
            i++;
            if (i == listColors.Count)
                i = 0;
            t = 0.0f;
        }
    }
}
