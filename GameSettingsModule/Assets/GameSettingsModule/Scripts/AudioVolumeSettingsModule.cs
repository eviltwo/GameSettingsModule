using System;
using UnityEngine;
using UnityEngine.Audio;

namespace GameSettingsModule
{
    public class AudioVolumeSettingsModule
    {
        private readonly AudioMixer _audioMixer;
        private readonly string _parameterName;
        public readonly int StepCount;
        public float TargetVolumeRatio { get; private set; } = 1;
        public event Action<float> OnValueChanged;

        public AudioVolumeSettingsModule(AudioMixer audioMixer, string parameterName, int stepCount)
        {
            _audioMixer = audioMixer;
            _parameterName = parameterName;
            StepCount = stepCount;
        }

        public int GetStep()
        {
            return Mathf.RoundToInt(TargetVolumeRatio * StepCount);
        }

        public void SetStep(int stepIndex)
        {
            stepIndex = Mathf.Clamp(stepIndex, 0, StepCount);
            SetVolumeRatio(stepIndex / (float)StepCount);
        }

        public void SetVolumeRatio(float ratio)
        {
            if (TargetVolumeRatio != ratio)
            {
                TargetVolumeRatio = ratio;
                _audioMixer.SetFloat(_parameterName, RatioToVolume(TargetVolumeRatio));
                OnValueChanged?.Invoke(TargetVolumeRatio);
            }
        }

        private static float RatioToVolume(float ratio)
        {
            const float EffectMultiply = 2.0f;
            return Mathf.Log10(Mathf.Max(ratio, 0.0001f)) * 20 * EffectMultiply;
        }
    }
}
