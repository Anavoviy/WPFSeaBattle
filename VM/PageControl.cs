using SeaBattleWPF.mvvm;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SeaBattleWPF.VM
{
    public class PageControl : BaseVM
    {
        static PageControl instance;

        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != null)
                    OnAppClose(this, null);
                currentPage = value;
                Signal();
            }
        }

        internal static PageControl GetInstance()
        {
            if (instance == null)
                instance = new PageControl();
            return instance;    
        }

        internal void OnAppClose(object sender, ExitEventArgs e)
        {
            ((BaseVM)CurrentPage.DataContext).OnClose();
        }
    }
}