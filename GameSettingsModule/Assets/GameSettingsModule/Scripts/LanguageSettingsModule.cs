#if GAMESETTINGSMODULE_LOCALIZATION_SUPPORT
using System;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;

namespace GameSettingsModule
{
    public class LanguageSettingsModule
    {
        public string TargetLocaleCode { get; private set; }

        public event Action<string> OnValueChanged;

        public LanguageSettingsModule()
        {
            TargetLocaleCode = LocalizationSettings.SelectedLocale.Identifier.Code;
        }

        public void GetLocaleCodes(List<string> results)
        {
            results.Clear();
            var locales = LocalizationSettings.AvailableLocales.Locales;
            for (int i = 0; i < locales.Count; i++)
            {
                results.Add(locales[i].Identifier.Code);
            }
        }

        public static int GetLocaleIndex(string localeCode)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            for (int i = 0; i < locales.Count; i++)
            {
                if (locales[i].Identifier.Code == localeCode)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SetLocale(int index)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            if (index >= 0 && index < locales.Count)
            {
                SetLocale(locales[index].Identifier.Code);
            }
        }

        public void SetLocale(string localeCode)
        {
            if (TargetLocaleCode != localeCode)
            {
                var locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
                if (locale != null)
                {
                    TargetLocaleCode = localeCode;
                    LocalizationSettings.SelectedLocale = locale;
                    OnValueChanged?.Invoke(localeCode);
                }
            }
        }

        public static string GetLocaleName(string localeCode)
        {
            var locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
            return locale?.Identifier.CultureInfo.DisplayName;
        }
    }
}
#endif
