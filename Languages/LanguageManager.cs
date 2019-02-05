using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Languages
{
    public static class LanguageManager
    {       
        private static ResourceDictionary GetLangResourceDictionary(string lang)
        {
            if (lang != null)
            {
                Assembly assembly = Assembly.LoadFrom("Languages.dll");
                string packUri = $"Languages;component/Lang_{lang}.xaml";
                return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            }
            return null;
        }


        /// <summary>
        /// Method returns Collection of strings with Languages name
        /// </summary>
        /// <returns></returns>
        public static string[] GetLanguages()
        {
            string[] langs = new string[]
            {
                "en-US",
                "ru-RU"
            };           
            return langs;
        }
               
         

        private static void ApplyLanguage(this Application app, string lang)
        {
            if (lang.StartsWith("Clear"))
            {
                app.Resources.MergedDictionaries.Clear();
                lang = lang.Substring(("Clear").Length);
            }

            ResourceDictionary dictionary = LanguageManager.GetLangResourceDictionary(lang);
            if (dictionary != null)
            {
                app.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        private static void ApplyLanguage(this ContentControl control, string lang)
        {
            if (lang.StartsWith("Clear"))
            {
                control.Resources.MergedDictionaries.Clear();
                lang = lang.Substring(("Clear").Length);
            }

            ResourceDictionary dictionary = LanguageManager.GetLangResourceDictionary(lang);
            if (dictionary != null)
            {
                control.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        #region Theme

        /// <summary>
        /// Language Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty LanguageProperty =
            DependencyProperty.RegisterAttached("Language", typeof(string), typeof(LanguageManager),
                new FrameworkPropertyMetadata((string)string.Empty,
                    new PropertyChangedCallback(OnLanguageChanged)));

        /// <summary>
        /// Gets the Language property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetLanguage(DependencyObject d)
        {
            return (string)d.GetValue(LanguageProperty);
        }

        /// <summary>
        /// Sets the Language property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetLanguage(DependencyObject d, string value)
        {
            d.SetValue(LanguageProperty, value);
        }

        /// <summary>
        /// Handles changes to the Language property.
        /// </summary>
        private static void OnLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string lang = e.NewValue as string;
            if (lang == string.Empty)
                return;

            ContentControl control = d as ContentControl;
            if (control != null)
            {
                control.ApplyLanguage(lang);
            }
        }
        #endregion
    }
}
