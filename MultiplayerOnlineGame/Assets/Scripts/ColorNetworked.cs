using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class ColorNetworked : NetworkBehaviour
{
    [Networked, OnChangedRenderAttribute(nameof(ColorChanged))]
    public Color networkColor { get; set; }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            networkColor = Color.red;
        }
    }

    public void ColorChanged()
    {
        Debug.Log("COLOR");
    }
}