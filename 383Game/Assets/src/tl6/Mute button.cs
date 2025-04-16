using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Toggle muteToggle;

    private const string MUTE_KEY = "isMuted";
    private bool isMuted = false;
    private float lastVolume = 1f;
    private VolumeControlDynamicBindingBC volume_state;

    void Start()
    {
        volume_state = new VolumeControlDynamicBinding();

        // Get mute state from dynamic binding instead of PlayerPrefs
        isMuted = volume_state.ShouldBeOn();

        // Save state to PlayerPrefs if you want persistence
        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
        lastVolume = isMuted ? PlayerPrefs.GetFloat("musicVolume", 1f) : AudioListener.volume;
        ApplyMute();

        if (muteToggle != null)
        {
            muteToggle.isOn = isMuted;
            muteToggle.onValueChanged.AddListener(OnMuteToggled);
        }
    }

    public void OnMuteToggled(bool toggledOn)
    {
        isMuted = toggledOn;
        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);

        if (isMuted)
        {
            lastVolume = AudioListener.volume;
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = lastVolume;
        }

        volume_state.SetVolume(isMuted);
    }

    private void ApplyMute()
    {
        AudioListener.volume = isMuted ? 0f : lastVolume;
    }

    private void OnDestroy()
    {
        if (muteToggle != null)
        {
            muteToggle.onValueChanged.RemoveListener(OnMuteToggled);
        }
    }
}
