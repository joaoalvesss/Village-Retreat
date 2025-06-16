using UnityEngine;
using FMODUnity;

public class WalkingScript : StateMachineBehaviour
{
    [EventRef]
    public string footstepSound = "event:/Character/footsteps1";

    // Armazena a inst�ncia do som para poder control�-la
    private FMOD.Studio.EventInstance footstepInstance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Cria uma inst�ncia do som
        footstepInstance = RuntimeManager.CreateInstance(footstepSound);

        // Define a posi��o 3D do som
        footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(animator.transform));

        // Inicia o som
        footstepInstance.start();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Para o som suavemente
        footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        // Libera os recursos da inst�ncia
        footstepInstance.release();
    }

    // Opcional: Se precisar de controle mais preciso durante o estado
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     // Atualiza a posi��o do som enquanto o personagem se move
    //     footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(animator.transform));
    // }
}