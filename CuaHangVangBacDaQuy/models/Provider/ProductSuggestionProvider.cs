using AutoCompleteTextBox.Editors;
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
                        .FirstOrDefault(state => string.Equals(state.TenSP, filter, StringComparison.CurrentCultureIgnoreCase));
            }

            public IEnumerable<SanPham> GetSuggestions(string filter)
            {
                if (string.IsNullOrWhiteSpace(filter)) return null;
                //System.Threading.Thread.Sleep(1000);
                return
                    ProductList
                        .Where(state => state.TenSP.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
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
                var products = DataProvider.Ins.DB.SanPhams;
                ProductList = products;
            }
        }
    }

