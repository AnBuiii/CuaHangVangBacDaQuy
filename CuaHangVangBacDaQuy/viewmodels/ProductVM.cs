using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace CuaHangVangBacDaQuy.viewmodels
{
    internal class ProductVM : BaseViewModel
    {
        public ICommand LoadedCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ProductVM()
        {
            LoadedCommand = new RelayCommand<ProductWindow>(parameter => true, parameter => Loaded(parameter));
            AddProductCommand = new RelayCommand<ProductWindow>(parameter => true, parameter => Add(parameter));
        }
        public void Loaded(ProductWindow weddingHallUC)
        {

        }
        public void Add(ProductWindow parameter)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
        }
    }
}
