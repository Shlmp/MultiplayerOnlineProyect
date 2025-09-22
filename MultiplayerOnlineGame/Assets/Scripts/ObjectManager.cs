using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public TrafficLight trafficLight;
    public static ObjectManager singleton;

    private void Awake()
    {
        singleton = this;
    }
}
