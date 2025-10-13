using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    private InputActions inputActions;

    private bool eggPain = false;
    private bool canShoot = false;

    private void Start()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        if (!eggPain)
        {
            eggPain = inputActions.Player.Action.triggered;
        }

        if (!canShoot)
        {
            canShoot = inputActions.Player.Shoot.triggered;
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (Object.HasInputAuthority && eggPain == true)
        {
            eggPain = false;
            RPC_CallTrafficLight();
        }

        if (Object.HasInputAuthority && canShoot == true)
        {
            canShoot = false;
            Rpc_Shoot();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_CallTrafficLight()
    {
        ObjectManager.singleton.trafficLight.ChangeColor();
    }

    #region Shoot
    public GameObject bulletPrefab;
    public Transform shootPos;
    public float bulletSpeed = 3;

    public GameObject particles;

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_Shoot()
    {
        var bullet = Runner.Spawn(bulletPrefab, shootPos.position, shootPos.rotation, Object.InputAuthority);
        if (bullet.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("RigidBody not assigned");
        }
        Rpc_PlayShootEffect();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void Rpc_PlayShootEffect()
    {
        Instantiate(particles, shootPos.position, shootPos.rotation);
    }
    #endregion
}
