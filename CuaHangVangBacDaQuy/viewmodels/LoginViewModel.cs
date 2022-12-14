using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.Converter;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class LoginViewModel: BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        public bool CheckLogin()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)) { 
                if(!IsLogin) MessageBox.Show("Thiếu thông tin đăng nhập"); 
                return false; 
            }
            string passwordEncode = Encode.EncodePassword(Password);
            var account = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == UserName && x.MatKhau == passwordEncode).FirstOrDefault();
            if (account == null)
            {
                if (!IsLogin) MessageBox.Show("Sai thông tin đăng nhập");
                return false;
            }
            return true;
        }

        void Login(Window p)
        {
            if (!CheckLogin()) return;

            NguoiDung.Logged = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == UserName).FirstOrDefault();
            MessageBox.Show("Xin chào " + NguoiDung.Logged.QuyenHan.TenQH + " " + NguoiDung.Logged.TenND);
            IsLogin = true;
            p.Close();
        }
        
    }
}
