using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class HomeViewModel : BaseViewModel
    {

        public ICommand LoadHomeView { get; set; }
        public HomeViewModel()
        {
            LoadHomeView = new RelayCommand<HomeView>((p) => true, (p) => LoadingHomeView(p));
        }

        private void LoadingHomeView(HomeView view)
        {
            Console.WriteLine("asd");
        }
    }
    


}
