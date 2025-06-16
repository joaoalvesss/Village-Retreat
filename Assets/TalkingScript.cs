using UnityEngine;
using FMODUnity;

public class TalkingScript : StateMachineBehaviour
{
    [EventRef]
    public string dialogueSound = "event:/Character/Dialogue"; // Evento de som

    [Range(0f, 1f)]
    public float volume = 0.4f; // Volume

    private FMOD.Studio.EventInstance dialogueInstance;
    private bool isPlaying = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Criar inst�ncia do som
        dialogueInstance = RuntimeManager.CreateInstance(dialogueSound);
        dialogueInstance.set3DAttributes(RuntimeUtils.To3DAttributes(animator.transform));
        dialogueInstance.setVolume(volume);
        dialogueInstance.start();
        isPlaying = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Atualiza a posi��o 3D
        dialogueInstance.set3DAttributes(RuntimeUtils.To3DAttributes(animator.transform));

        // Verifica se o som parou e reinicia se necess�rio
        FMOD.Studio.PLAYBACK_STATE playbackState;
        dialogueInstance.getPlaybackState(out playbackState);

        if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            dialogueInstance.start(); // Recome�a o som
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Para com fade-out e liberta
        dialogueInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        dialogueInstance.release();
        isPlaying = false;
    }
}
