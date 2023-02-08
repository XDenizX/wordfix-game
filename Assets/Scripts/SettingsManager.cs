using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameSettings Settings { get; private set; }

    private void Start()
    {
        SettingsHelper.TryLoad(Paths.SettingsRelativePath, out var settings);
        Settings = settings ?? new GameSettings();
    }

    private void OnApplicationQuit()
    {
        SettingsHelper.Save(Settings, Paths.SettingsRelativePath);
    }
}