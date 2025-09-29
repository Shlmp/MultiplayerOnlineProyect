using UnityEngine;
using Fusion;
public class PlayerShoot : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPosition;

    public GameObject particles;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        bool foo = inputActions.Player.Shoot.triggered;

        if (HasInputAuthority && foo)
        {
            Rpc_ShootAShot();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_ShootAShot()
    {
        var blt = Runner.Spawn(bulletPrefab, shootPosition.position, shootPosition.rotation, Object.InputAuthority);

        if (blt.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(blt.transform.forward * 6, ForceMode.Impulse);
        }
    }
}
