using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class SupplierViewModel: BaseViewModel
    {
        #region Params
        private ObservableCollection<NhaCungCap> _SuppliersList;
        public ObservableCollection<NhaCungCap> SuppliersList { get => _SuppliersList; set { _SuppliersList = value; OnPropertyChanged(); } }


        private OpenDiaLog _IsOpenDiaLog;
        public OpenDiaLog IsOpenDiaLog
        {
            get { return _IsOpenDiaLog; }
            set { _IsOpenDiaLog = value; OnPropertyChanged(); }
        }

        private NhaCungCap _SelectedSupplier;
        public NhaCungCap SelectedSupplier
        {
            get => _SelectedSupplier;
            set
            {
                _SelectedSupplier = value;
                OnPropertyChanged();

            }
        }

        private AddOrEditSupplierViewModel _ContentAddSupplier;
        public AddOrEditSupplierViewModel ContentAddSupplier
        {
            get => _ContentAddSupplier;
            set
            {
                _ContentAddSupplier = value;
                OnPropertyChanged();

            }
        }

        public ICommand EditCommand { get; set; }

        public ICommand AddCommand { get; set; }
        #endregion
        #region Command
        #endregion
        public SupplierViewModel()
        {
            IsOpenDiaLog = new OpenDiaLog() { IsOpen = false };
            SuppliersList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);
            AddCommand = new RelayCommand<SupplierViewModel>((p) => true, p => ActionDiaLog("Add"));
            EditCommand = new RelayCommand<DataGridTemplateColumn>((p) => true, p => ActionDiaLog("Edit"));
        }

        private void ActionDiaLog(string caseDiaLog)
        {
            IsOpenDiaLog.IsOpen = true;
            switch (caseDiaLog)
            {
                case "Add":
                    AddNewSupplier();
                    break;

                case "Edit":
                    EditSupplier();
                    break;
            }
        }

        private void AddNewSupplier()
        {

           
            ContentAddSupplier = new AddOrEditSupplierViewModel("Thêm nhà cung cấp mới", ref _IsOpenDiaLog, ref _SuppliersList);
        }

        private void EditSupplier()
        {
            ContentAddSupplier = new AddOrEditSupplierViewModel("Chỉnh sửa thông tin nhà cung cấp", ref _IsOpenDiaLog, ref _SuppliersList, ref _SelectedSupplier);
         
        }
    }
}
