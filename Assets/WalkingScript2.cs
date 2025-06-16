using UnityEngine;
using FMODUnity;

public class WalkingScript2 : StateMachineBehaviour
{
    [EventRef]
    public string footstepSound = "event:/Character/footsteps2";

    [Range(0f, 1f)]
    public float volume = 1f; // Controle de volume (0 = mudo, 1 = volume m�ximo)

    // Armazena a inst�ncia do som para poder control�-la
    private FMOD.Studio.EventInstance footstepInstance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Cria uma inst�ncia do som
        footstepInstance = RuntimeManager.CreateInstance(footstepSound);

        // Define o volume
        footstepInstance.setVolume(volume);

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

    // Se quiser atualizar o volume em tempo real durante a anima��o
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Atualiza a posi��o do som enquanto o personagem se move
        footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(animator.transform));

        // Atualiza o volume se necess�rio
        footstepInstance.setVolume(volume);
    }
}