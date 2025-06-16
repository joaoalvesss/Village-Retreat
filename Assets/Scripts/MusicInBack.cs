using UnityEngine;
using FMODUnity;

public class MusicInBack : MonoBehaviour
{
    [Header("FMOD Music Settings")]
    [EventRef]
    public string musicEvent = "event:/Music/SceneMusic"; // Escreves isto no Inspector

    [Range(0f, 1f)]
    public float volume = 1.0f; // Escreves isto no Inspector também

    private FMOD.Studio.EventInstance musicInstance;

    void Start()
    {
        // Usa as variáveis escritas no Inspector como "argumentos"
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.setVolume(volume);
        musicInstance.start();
    }

    void Update()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        musicInstance.getPlaybackState(out playbackState);

        if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            musicInstance.start();
        }
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}
