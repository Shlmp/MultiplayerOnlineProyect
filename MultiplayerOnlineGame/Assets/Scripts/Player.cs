using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    private InputActions inputActions;
    
    void Start()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        bool foo = inputActions.Player.Action.triggered;
        if (Object.HasInputAuthority && foo == true)
        {
            RPC_CallTrafficLight();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CallTrafficLight()
    {
        ObjectManager.singleton.trafficLight.ChangeColor();
    }
}
