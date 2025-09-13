using Microsoft.Win32;

public static class SaverConfig
{
    private const string RegistryKeyPath = @"Software\ScreenSaverPlayer";
    private const string VideoPathValue = "VideoPath";
    private const string EnableSoundValue = "EnableSound";
    private const string VolumeValue = "Volume";
    private const string LockOnExitValue = "LockOnExit";

    public static void SetVideoPath(string path)
    {
        using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
        key?.SetValue(VideoPathValue, path);
    }

    public static string? GetVideoPath()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        return key?.GetValue(VideoPathValue) as string;
    }

    public static void SetEnableSound(bool enable)
    {
        using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
        key?.SetValue(EnableSoundValue, enable ? 1 : 0, RegistryValueKind.DWord);
    }

    public static bool IsSoundEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        object? value = key?.GetValue(EnableSoundValue);
        return value is int intVal && intVal == 1;
    }

    public static void SetVolume(int volume)
    {
        using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
        key?.SetValue(VolumeValue, Math.Clamp(volume, 0, 100), RegistryValueKind.DWord);
    }

    public static int GetVolume()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        object? value = key?.GetValue(VolumeValue);
        return value is int intVal ? Math.Clamp(intVal, 0, 100) : 50; // default 50%
    }

    public static void SetLockOnExit(bool lockOnExit)
    {
        using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
        key?.SetValue(LockOnExitValue, lockOnExit ? 1 : 0, RegistryValueKind.DWord);
    }

    public static bool GetLockOnExit()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        object? value = key?.GetValue(LockOnExitValue);
        return value is int intVal && intVal == 1;
    }
}
