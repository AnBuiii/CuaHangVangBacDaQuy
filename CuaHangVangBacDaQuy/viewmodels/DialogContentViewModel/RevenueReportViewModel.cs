using CuaHangVangBacDaQuy.models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    internal class RevenueReportViewModel:BaseViewModel
    {
        

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => seriesCollection;
            set
            {
                seriesCollection = value;
                OnPropertyChanged();
            }
        }

        private List<string> labels;
        public List<string> Labels
        {
            get => labels;
            set
            {
                labels = value;
                OnPropertyChanged();
            }
        }

        private Func<double, string> yFormatter;
        public Func<double, string> YFormatter
        {
            get => yFormatter;
            set
            {
                yFormatter = value;
                OnPropertyChanged();
            }
        }
        private List<string> _searchTypes;
        public List<string> SearchTypes { get { return _searchTypes; } set { _searchTypes = value; OnPropertyChanged(); } }

        private string _selectedSearchType;
        public string SelectedSearchType { get { return _selectedSearchType; } set { _selectedSearchType = value; OnPropertyChanged(); if (SelectedSearchType != null) Search(); } }

        private bool _doImport;
        public bool DoImport { get => _doImport; set { _doImport = value; OnPropertyChanged(); Search(); } }

        private bool _doExport;
        public bool DoExport { get => _doExport; set { _doExport = value; OnPropertyChanged(); Search(); } }

        public ICommand SearchCommand { get; set; }

        public RevenueReportViewModel()
        {

            Labels = new List<string>();
            SearchTypes = new List<string> { "Thời gian", "Sản phẩm" };
            SelectedSearchType = SearchTypes[0];

            SearchCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Search());
        }

        public void Search()
        {
            switch (SelectedSearchType)
            {
                case "Thời gian":
                    TimeSearch();
                    break;
                case "Sản phẩm":
                    ProductSearch();
                    break;
                default:
                    break;
            }
        }
        private void TimeSearch()
        {
            SeriesCollection = new SeriesCollection();
            if (DoImport)
            {
                SeriesCollection.Add(new LineSeries
                {
                    Title = "Tổng mua",
                    Values = GetValueOfMonth("Tổng mua"),
                });
            }
            if (DoExport)
            {
                SeriesCollection.Add(new LineSeries
                {
                    Title = "Tổng bán",
                    Values = GetValueOfMonth("Tổng bán"),
                    Stroke = Brushes.Red,
                    Fill = new SolidColorBrush(Color.FromArgb(10,255,0,0)),
                }); ;
            }
            Labels.Clear();
            string[] labelMonth = new[] { "10/2023", "11/2023", "12/2023","1/2024","2/2024","3/2024" };
            Labels = labelMonth.ToList();

            YFormatter = value => value.ToString();
        }

        private void ProductSearch()
        {
            SeriesCollection = new SeriesCollection();
            if (DoImport)
            {
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Tổng mua",
                    Values = GetValueOfProduct("Tổng mua"),
                });
            }
            if (DoExport)
            {
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Tổng bán",
                    Values = GetValueOfProduct("Tổng bán"),
                    Stroke = Brushes.Red,
                    Fill = Brushes.Red


                });
            }
            Labels.Clear();
            foreach (SanPham sanpham in DataProvider.Ins.DB.SanPhams)
            {
                Labels.Add(sanpham.TenSP);
            }

            YFormatter = value => value.ToString();
        }

        ChartValues<decimal> GetValueOfMonth(string type)
        {
            ChartValues<decimal> chartValues = new ChartValues<decimal>();
            if(type == "Tổng bán")
            {
                DateTime begin = DateTime.Now.AddMonths(-5);
                for (int i = 0; i < 6; i++)
                {
                    decimal sum = 0;
                    foreach (ChiTietPhieuBan phieuBan in DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.PhieuBan.NgayLap.Value.Month == begin.Month && p.PhieuBan.NgayLap.Value.Year == begin.Year))
                    {
                        sum += (decimal)phieuBan.SoLuong * (decimal)phieuBan.SanPham.DonGia * (1 + (decimal)phieuBan.SanPham.LoaiSanPham.LoiNhuan);
                    }
                    chartValues.Add(sum);
                    begin = begin.AddMonths(1);
                }
            } else // Tổng mua
            {
                DateTime begin = DateTime.Now.AddMonths(-5);
                for (int i = 0; i < 6; i++)
                {
                    decimal sum = 0;
                    foreach (ChiTietPhieuMua phieuMua in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.PhieuMua.NgayLap.Value.Month == begin.Month && p.PhieuMua.NgayLap.Value.Year == begin.Year))
                    {
                        sum += (decimal)phieuMua.SoLuong * (decimal)phieuMua.SanPham.DonGia ;
                    }
                    chartValues.Add(sum);
                    begin = begin.AddMonths(1);
                }
            }
            

            return chartValues;
        }

        ChartValues<decimal> GetValueOfProduct(string type)
        {
            ChartValues<decimal> chartValues = new ChartValues<decimal>();
            if (type == "Tổng bán")
            {
               
                foreach(SanPham sanPham in DataProvider.Ins.DB.SanPhams)
                {
                    decimal sum = 0;
                    foreach (ChiTietPhieuBan phieuBan in DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.SanPham.MaSP == sanPham.MaSP))
                    {
                        sum += (decimal)phieuBan.SoLuong * (decimal)phieuBan.SanPham.DonGia * (1 + (decimal)phieuBan.SanPham.LoaiSanPham.LoiNhuan);
                    }
                    chartValues.Add(sum);
                  
                }
            }
            else
            {
                foreach (SanPham sanPham in DataProvider.Ins.DB.SanPhams)
                {
                    decimal sum = 0;
                    foreach (ChiTietPhieuMua phieuMua in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.SanPham.MaSP == sanPham.MaSP))
                    {
                        sum += (decimal)phieuMua.SoLuong * (decimal)phieuMua.SanPham.DonGia;
                    }
                    chartValues.Add(sum);
                }
            }


            return chartValues;
        }
    }
}
