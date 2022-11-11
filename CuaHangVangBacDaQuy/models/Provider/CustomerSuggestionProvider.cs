using AutoCompleteTextBox.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.models.Provider
{
    public class CustomerSuggestionProvider : IComboSuggestionProvider
    {
        public IEnumerable<KhachHang> CustomersList { get; set; }

        public KhachHang GetExactSuggestion(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            return
                CustomersList
                    .FirstOrDefault(customer => string.Equals(customer.SoDT, filter, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<KhachHang> GetSuggestions(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            System.Threading.Thread.Sleep(300);
            return
                CustomersList
                    .Where(supplier => (supplier.TenKH.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1) || (supplier.SoDT.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1))
                    .ToList();
        }

        IEnumerable IComboSuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }
        IEnumerable IComboSuggestionProvider.GetFullCollection()
        {
            return CustomersList.ToList();
        }

        public CustomerSuggestionProvider()
        {
            var customer = DataProvider.Ins.DB.KhachHangs;
            CustomersList = customer;
        }
    }
}
