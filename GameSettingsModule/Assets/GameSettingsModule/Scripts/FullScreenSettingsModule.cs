using UnityEngine;

namespace GameSettingsModule
{
    public class FullScreenSettingsModule
    {
        public bool TargetFullScreen { get; private set; } = true;

        public event System.Action<bool> OnValueChanged;

        public void SetFullScreen(bool isFullScreen)
        {
            if (TargetFullScreen != isFullScreen)
            {
                TargetFullScreen = isFullScreen;
                Screen.fullScreen = TargetFullScreen;
                OnValueChanged?.Invoke(TargetFullScreen);
            }
        }
    }
}
