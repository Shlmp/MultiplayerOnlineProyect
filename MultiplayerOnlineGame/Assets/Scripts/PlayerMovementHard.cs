using UnityEngine;
using Fusion;

public class PlayerMovementHard : NetworkBehaviour
{
    private CharacterController charCon;

    public float speed = 2f;

    private void Awake()
    {
        gameObject.TryGetComponent(out charCon);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput<MyInput>(out var inputs) == false) {  return; }

        Vector3 vector = new Vector3();

        if (inputs.buttons.IsSet(MyButtons.Forward)) { vector.z += 1; }
        if (inputs.buttons.IsSet(MyButtons.Backward)) { vector.z -= 1; }
        if (inputs.buttons.IsSet(MyButtons.Left)) { vector.x += 1; }
        if (inputs.buttons.IsSet(MyButtons.Right)) { vector.x -= 1; }

        charCon.Move(vector * speed * Runner.DeltaTime);
    }
}
