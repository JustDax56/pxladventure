using UnityEngine;

public class ChaseState : StateMachineBehaviour
{
    MovementChamaleon controlador;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlador = animator.gameObject.GetComponent<MovementChamaleon>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (controlador.objetivo != null)
        {
           
            Vector2 direction = (controlador.objetivo.transform.position - controlador.transform.position).normalized;

            controlador.rb.linearVelocity = new Vector2(direction.x * controlador.velocidad, controlador.rb.linearVelocity.y);

            if (direction.x > 0)
            {
                controlador.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                controlador.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlador.rb.linearVelocity = new Vector2(0, controlador.rb.linearVelocity.y);
    }
}
