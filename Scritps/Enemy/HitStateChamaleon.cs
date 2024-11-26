using UnityEngine;

public class HitStateChamaleon : StateMachineBehaviour
{
    MovementChamaleon controlador;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        controlador = animator.gameObject.GetComponent<MovementChamaleon>();

        if (controlador != null && controlador.rb != null)
        {

            controlador.rb.linearVelocity = new Vector2(0, controlador.rb.linearVelocity.y);
        }
        GameObject enemigo = animator.gameObject;
        Object.Destroy(enemigo, 0.3f);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
