﻿using CuaHangVangBacDaQuy.views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public AccountViewModel AccountViewModel { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand HomeViewCommand { get; set; }
        public ICommand AccountViewCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public bool isLoaded = false;
        public MainViewModel()
        {
            InitNavBar();
            _dataTemplate = HomeViewModel;

            CloseWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Application.Current.Shutdown();
            });

            HomeViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = HomeViewModel;
            });

            AccountViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = AccountViewModel;
            });

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                isLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    //LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            });

        }
        public void InitNavBar()
        {
            HomeViewModel = new HomeViewModel();
            AccountViewModel = new AccountViewModel();
            //viewmodel init
        }
    }
}