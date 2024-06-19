using System;
using UnityEngine;

namespace GameSettingsModule
{
    public class GraphicsQualitySettingsModule
    {
        public const int UnknownQualityLevel = -1;
        public readonly string[] QualityNames = QualitySettings.names;
        public int TargetQualityLevel { get; private set; } = UnknownQualityLevel;

        public event Action<int> OnValueChanged;

        public void SetQualityLevel(int level)
        {
            if (level == UnknownQualityLevel)
            {
                level = QualitySettings.GetQualityLevel();
            }

            level = Mathf.Clamp(level, 0, QualityNames.Length - 1);
            if (TargetQualityLevel != level)
            {
                TargetQualityLevel = level;
                QualitySettings.SetQualityLevel(level);
                OnValueChanged?.Invoke(level);
            }
        }
    }
}
