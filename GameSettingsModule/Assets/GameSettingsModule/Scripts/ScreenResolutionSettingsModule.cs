using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettingsModule
{
    public class ScreenResolutionSettingsModule
    {
        private List<Vector2Int> _resolutionBuffer = new List<Vector2Int>();

        public Vector2Int TargetResolution { get; private set; }

        public event Action<Vector2Int> OnValueChanged;

        public ScreenResolutionSettingsModule()
        {
            TargetResolution = new Vector2Int(Screen.width, Screen.height);
        }

        public int GetResolutionCount()
        {
            CollectResolutions(_resolutionBuffer);
            return _resolutionBuffer.Count;
        }

        public int GetResolutionIndex()
        {
            CollectResolutions(_resolutionBuffer);
            return FindResolutionIndex(TargetResolution.x, TargetResolution.y, _resolutionBuffer);
        }

        public void SetResolutionIndex(int index)
        {
            CollectResolutions(_resolutionBuffer);
            var changedIndex = Mathf.Clamp(index, 0, _resolutionBuffer.Count - 1);
            var resolution = _resolutionBuffer[changedIndex];
            SetResolution(resolution.x, resolution.y);
        }

        public void SetResolution(int width, int height)
        {
            var resolution = new Vector2Int(width, height);
            if (resolution.x <= 0 || resolution.y <= 0)
            {
                resolution = new Vector2Int(Screen.width, Screen.height);
            }

            if (TargetResolution.x != resolution.x || TargetResolution.y != resolution.y)
            {
                TargetResolution = resolution;
                Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreenMode);
                OnValueChanged?.Invoke(TargetResolution);
            }
        }

        private static void CollectResolutions(List<Vector2Int> results)
        {
            results.Clear();
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                var resolution = Screen.resolutions[i];
                var res = new Vector2Int(resolution.width, resolution.height);
                if (!results.Contains(res))
                {
                    results.Add(res);
                }
            }
        }

        private static int FindResolutionIndex(int width, int height, IReadOnlyList<Vector2Int> resolutions)
        {
            for (var i = 0; i < resolutions.Count; i++)
            {
                var resolution = resolutions[i];
                if (resolution.x == width && resolution.y == height)
                {
                    return i;
                }
            }

            return resolutions.Count - 1;
        }
    }
}
