using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views;
using CuaHangVangBacDaQuy.views.userControlDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class AccountViewModel : BaseViewModel
    {
        #region Params
        private ObservableCollection<NguoiDung> _NguoiDungList;
        public ObservableCollection<NguoiDung> NguoiDungList { get => _NguoiDungList; set { _NguoiDungList = value; OnPropertyChanged(); } }

        private ObservableCollection<QuyenHan> _QuyenHanList;
        public ObservableCollection<QuyenHan> QuyenHanList { get => _QuyenHanList; set { _QuyenHanList = value; OnPropertyChanged(); } }

        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        private QuyenHan _SelectedQuyenHan;
        public QuyenHan SelectedQuyenHan { get => _SelectedQuyenHan; set { _SelectedQuyenHan = value; OnPropertyChanged(); } }

        private string _TenNguoiDung;
        public string TenNguoiDung { get => _TenNguoiDung; set { _TenNguoiDung = value; OnPropertyChanged(); } }

        private NguoiDung _SelectedAccount;
        public NguoiDung SelectedAccount
        {
            get => _SelectedAccount;
            set
            {

                _SelectedAccount = value;
                OnPropertyChanged();
                if (SelectedAccount != null)
                {
                    TenDangNhap = SelectedAccount.TenDangNhap;
                    SelectedQuyenHan = SelectedAccount.QuyenHan;
                    TenNguoiDung = SelectedAccount.TenND;
                }
            }
        }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }

        private OpenDiaLog _IsOpenDialogAccount;
        public OpenDiaLog IsOpenDialogAccount { get => _IsOpenDialogAccount; set { _IsOpenDialogAccount = value; OnPropertyChanged(); } }
        private AddOrEditAccountUC _addOrEditAccountUC;
        public AddOrEditAccountUC addOrEditAccountUC { get => _addOrEditAccountUC; set { _addOrEditAccountUC = value; OnPropertyChanged(); } }

        private AddOrEditAccountViewModel addOrEditAccountViewModel;
        #endregion

        #region Commands
        public ICommand LoadAccountView { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand SavePasswordCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        #endregion

        private List<string> _searchTypes;
        public List<string> SearchTypes { get { return _searchTypes; } set { _searchTypes = value; OnPropertyChanged(); } }
        private string _selectedSearchType;
        public string SelectedSearchType { get { return _selectedSearchType; } set { _selectedSearchType = value; OnPropertyChanged(); } }

        private string _contentSearch;
        public string ContentSearch
        {
            get { return _contentSearch; }
            set
            {
                _contentSearch = value;
                OnPropertyChanged();
                //if (ContentSearch == "")
                //    Load(false);
            }
        }
        private bool _IsOpenDialog;
        public bool IsOpenDialog { get => _IsOpenDialog; set { _IsOpenDialog = value; OnPropertyChanged(); } }
        public AccountViewModel()
        {

            NguoiDungList = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
            QuyenHanList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);
            IsOpenDialogAccount = new OpenDiaLog() { IsOpen = false };
            SearchTypes = new List<string> { "Quyền hạn", "Tên người dùng", "Tên đăng nhập", };
            SelectedSearchType = SearchTypes[1];
            SearchCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Search());
            AddCommand = new RelayCommand<AccountView>(p => true, p => ActionDialog("Add"));
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => ActionDialog("Edit"));
            DeleteAccountCommand = new RelayCommand<DataGridTemplateColumn>((p) => true, p => DeletedAccount());
            /*AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedQuyenHan == null)
                    return false;
                return true;

            }, (p) =>
            {
                var NguoiDung = new NguoiDung { TenDangNhap = TenDangNhap, MaQH = SelectedQuyenHan.MaQH, };

                //DataProvider.Ins.DB.Objects.Add(Object);
                //DataProvider.Ins.DB.SaveChanges();

                //List.Add(Object);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return isItemSelected();

            }, (p) =>
            {
                var nguoidung = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == SelectedItem.MaND).SingleOrDefault();
                nguoidung.TenDangNhap = TenDangNhap;
                nguoidung.QuyenHan = SelectedQuyenHan;
                nguoidung.TenND = TenNguoiDung;
                DataProvider.Ins.DB.SaveChanges();
                NguoiDungList = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
            });
            ChangePasswordCommand = new RelayCommand<object>((p) =>
            {
                return isItemSelected();

            }, (p) =>
            {
                IsOpenDialog = true;
                Password = "";
                ConfirmPassword = "";
            });
            SavePasswordCommand = new RelayCommand<object>((p) =>
            {
                if(Password == null || Password == "")
                {
                    return false;
                }
                if (Password != ConfirmPassword) return false;
                return true;

            }, (p) =>
            {
                var nguoidung = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == SelectedItem.MaND).SingleOrDefault();
                nguoidung.MatKhau = MD5Hash(Base64Encode(Password));
                DataProvider.Ins.DB.SaveChanges();
                NguoiDungList = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
                IsOpenDialog = false;

            });*/


        }


        public void Search()
        {
            switch (SelectedSearchType)
            {
                case "Quyền hạn":
                    NguoiDungList = new ObservableCollection<NguoiDung>(
                        DataProvider.Ins.DB.NguoiDungs.Where(
                            x => x.QuyenHan.TenQH.ToString().Contains(ContentSearch)));
                    break;
                case "Tên người dùng":
                    NguoiDungList = new ObservableCollection<NguoiDung>(
                         DataProvider.Ins.DB.NguoiDungs.Where(
                             x => x.TenND.ToString().Contains(ContentSearch)));
                    break;
                case "Tên đăng nhập":
                    NguoiDungList = new ObservableCollection<NguoiDung>(
                         DataProvider.Ins.DB.NguoiDungs.Where(
                             x => x.TenDangNhap.ToString().Contains(ContentSearch)));
                    break;
                default:
                    break;
            }
        }

        private void ActionDialog(string caseDiaglog)
        {
            IsOpenDialogAccount.IsOpen = true;
            switch (caseDiaglog)
            {
                case "Add":
                    AddNewAccount();
                    break;
                case "Edit":
                    EditAccount();
                    break;

            }
        }
        private void AddNewAccount()
        {
            addOrEditAccountViewModel = new AddOrEditAccountViewModel("Thêm tài khoản mới", ref _IsOpenDialogAccount, ref _NguoiDungList);
            addOrEditAccountUC = new AddOrEditAccountUC()
            {
                DataContext = addOrEditAccountViewModel
            };
        }
        private void EditAccount()
        {
            if (SelectedAccount == null) { MessageBox.Show("null"); IsOpenDialogAccount.IsOpen = false; return; }
            addOrEditAccountViewModel = new AddOrEditAccountViewModel("Chỉnh sửa thông tin tài khoản", ref _IsOpenDialogAccount, ref _NguoiDungList, ref _SelectedAccount);
            addOrEditAccountUC = new AddOrEditAccountUC()
            {
                DataContext = addOrEditAccountViewModel
            };
        }

        private bool isItemSelected()
        {
            if (string.IsNullOrEmpty(TenDangNhap) || SelectedAccount == null)
                return false;

            var displayList = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == TenDangNhap);
            if (displayList == null || displayList.Count() == 0)
                return false;

            return true;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private void DeletedAccount()
        {
            if (SelectedAccount == null) { MessageBox.Show("null"); IsOpenDialogAccount.IsOpen = false; return; }
            var deletedCustomer = DataProvider.Ins.DB.NguoiDungs.Where(c => c.MaND == SelectedAccount.MaND).SingleOrDefault();

            if (deletedCustomer.MaND == 1)
            {
                MessageBox.Show("Đây là tài khoản mặc định, không thể xóa!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (MessageBox.Show("Bạn có chắc chắc muốn xóa tài khoản" + deletedCustomer.TenDangNhap + " không?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            DataProvider.Ins.DB.NguoiDungs.Remove(deletedCustomer);
            DataProvider.Ins.DB.SaveChanges();
            NguoiDungList.Remove(SelectedAccount);


        }

    }
}
