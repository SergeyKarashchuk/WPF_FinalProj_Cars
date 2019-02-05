using Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Themes;

namespace CarsCatalog.ViewModel.StyleAndLanguage
{
    public class StyleLangCollection : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Properties      
        public string[] ThemeList { get; }
        public string[] LangList { get; }

        private string selectedListTheme;
        public string SelectedListTheme
        {
            get => selectedListTheme;
            set
            {
                selectedListTheme = value;
                if (value != SelectedListTheme)
                {
                    SelectedTheme = $"Clear{value}";
                }
                else
                {
                    SelectedTheme = value;
                }
                SelectedLanguage = SelectedListLanguage;
                Notify();
            }
        }

        private string selectedTheme;
        public string SelectedTheme
        {
            get => selectedTheme;
            set
            {
                selectedTheme = value;
                Notify();
            }
        }

        private string selectedListLanguage;
        public string SelectedListLanguage
        {
            get => selectedListLanguage;
            set
            {
                selectedListLanguage = value;
                if (value != SelectedListLanguage)
                {
                    SelectedLanguage = $"Clear{value}";
                }
                else
                {
                    SelectedLanguage = value;
                }
                SelectedTheme = SelectedListTheme;
                Notify();
            }
        }

        private string selectedLang;
        public string SelectedLanguage
        {
            get => selectedLang;
            set
            {
                selectedLang = value;
                Notify();
            }
        }
        private static StyleLangCollection singleton;
        #endregion

        #region Methods    
        public static StyleLangCollection GetStyleLangCollection()
        {
            if(singleton == null)
            {
                singleton = new StyleLangCollection();
            }
            return singleton;
        }
        private StyleLangCollection()
        {
            ThemeList = ThemeManager.GetThemes();
            if (ThemeList.Length > 0)
            {
                SelectedListTheme = ThemeList[0];
            }
            LangList = LanguageManager.GetLanguages();
            if (LangList.Length > 0)
            {
                SelectedListLanguage = LangList[0];
            }
        }        
        #endregion
    }
}
