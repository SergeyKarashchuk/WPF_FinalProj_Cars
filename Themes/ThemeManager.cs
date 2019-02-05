using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Themes
{
    public static class ThemeManager
    {
        private static ResourceDictionary GetThemeResourceDictionary(string theme)
        {
            if (theme != null)
            {
                Assembly assembly = Assembly.LoadFrom("Themes.dll");
                string packUri = $"Themes;component/{theme}.xaml";
                return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            }
            return null;
        }

        public static string[] GetThemes()
        {
            string[] themes = new string[]
            {
                "ExpressionDark", "ExpressionLight", 
                "ShinyBlue", "ShinyRed",
            };
            return themes;
        }

        private static void ApplyTheme(this Application app, string theme)
        {
            if (theme.StartsWith("Clear"))
            {
                app.Resources.MergedDictionaries.Clear();
                theme = theme.Substring(("Clear").Length);
            }

            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);
            if (dictionary != null)
            {
                app.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        private static void ApplyTheme(this ContentControl control, string theme)
        {
            if (theme.StartsWith("Clear"))
            {
                control.Resources.MergedDictionaries.Clear();
                theme = theme.Substring(("Clear").Length);
            }

            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);
            if (dictionary != null)
            {
                control.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        #region Theme

        /// <summary>
        /// Theme Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(string), typeof(ThemeManager),
                new FrameworkPropertyMetadata((string)string.Empty,
                    new PropertyChangedCallback(OnThemeChanged)));

        /// <summary>
        /// Gets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetTheme(DependencyObject d)
        {
            return (string)d.GetValue(ThemeProperty);
        }

        /// <summary>
        /// Sets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetTheme(DependencyObject d, string value)
        {
            d.SetValue(ThemeProperty, value);
        }

        /// <summary>
        /// Handles changes to the Theme property.
        /// </summary>
        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string theme = e.NewValue as string;
            if (theme == string.Empty)
                return;

            ContentControl control = d as ContentControl;
            if (control != null)
            {
                control.ApplyTheme(theme);
            }
        }
        #endregion


    }
}
