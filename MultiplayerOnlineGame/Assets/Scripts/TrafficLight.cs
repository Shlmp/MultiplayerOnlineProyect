using UnityEngine;
using Fusion;

public class TrafficLight : MonoBehaviour
{
    private MeshRenderer mr;
    public int index = 0;
    public Color[] colors = { Color.green, Color.yellow, Color.red };

    void Start()
    {
        gameObject.TryGetComponent(out mr);
    }

    public void ChangeColor()
    {
        index = (index + 1) % colors.Length;
        mr.material.color = colors[index];
    }
}
