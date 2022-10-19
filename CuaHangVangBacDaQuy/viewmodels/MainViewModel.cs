using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class MainViewModel : BaseViewModel
    {

        private object _dataTemplate;
        public object DataTemplate
        {
            get => _dataTemplate;
            set
            {
                _dataTemplate = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel HomeViewModel { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public bool isLoaded {get; set;} = false;
        public MainViewModel()
        {
            InitNavBar();
            _dataTemplate = HomeViewModel;
            //LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    isLoaded = true;
            //    LoginWindow loginWindow = new LoginWindow();
            //    loginWindow.ShowDialog();
            //});
           
        }
        public void InitNavBar()
        {
            HomeViewModel = new HomeViewModel();
        }
    }
}
