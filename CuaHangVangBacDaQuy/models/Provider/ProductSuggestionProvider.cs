using AutoCompleteTextBox.Editors;
using CuaHangVangBacDaQuy.viewmodels.Converter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.models.Provider
{
  
        public class ProductSuggestionProvider : IComboSuggestionProvider
        {
            public IEnumerable<SanPham> ProductList { get; set; }

            public SanPham GetExactSuggestion(string filter)
            {
                if (string.IsNullOrWhiteSpace(filter)) return null;
                return
                    ProductList
                        .FirstOrDefault(product => string.Equals(product.TenSP, filter, StringComparison.CurrentCultureIgnoreCase));
            }

            public IEnumerable<SanPham> GetSuggestions(string filter)
            {
                if (string.IsNullOrWhiteSpace(filter)) return null;
                // System.Threading.Thread.Sleep(300);
                return
                    ProductList
                        .Where(product => product.TenSP.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1 && CaculateInventoryConverter.CaculateInventory(product.MaSP) > 0)
                        .ToList();
            }

            IEnumerable IComboSuggestionProvider.GetSuggestions(string filter)
            {
                return GetSuggestions(filter);
            }
            IEnumerable IComboSuggestionProvider.GetFullCollection()
            {
                return ProductList.ToList();
            }

            public ProductSuggestionProvider()
            {
                ProductList = DataProvider.Ins.DB.SanPhams;
                 
            }
        }
    }

