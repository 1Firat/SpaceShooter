using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGame
{
    public string type;
    public float value;
    public float value2;
    public EventGame(string type, float value, float value2)
    {
        this.type = type;
        this.value = value;
        this.value2 = value2;
    }
}
