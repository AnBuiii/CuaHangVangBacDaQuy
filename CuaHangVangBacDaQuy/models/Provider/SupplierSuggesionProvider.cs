using AutoCompleteTextBox.Editors;
using CuaHangVangBacDaQuy.viewmodels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CuaHangVangBacDaQuy.models.Provider
{

    public enum MatchKind
    {
        StartsWith,
        Contains,
        EndsWith,
        Exact,
    }

    public class SupplierSuggesionProvider : BaseViewModel, ISuggestionProvider
    {

       
        public ObservableCollection<NhaCungCap> SupplierList { get; private set; }

       
       private bool _allowEmptyFilter;
        private bool _ignoreCase;
        private string _lastFilter;
        private int _maxSuggestionCount;
        private MatchKind _matchKind;
        private StringComparison _comparison;
       
        private Func<string, string, bool> _matchPredicate;



       
        public bool AllowEmptyFilter
        {

            get { return _allowEmptyFilter; }
            set { SetPropertyChanged(ref _allowEmptyFilter, value); }
        }

        public bool IgnoreCase
        {
            get { return _ignoreCase; }
            set
            {

                SetPropertyChanged(ref _ignoreCase, value);
                _comparison = value ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            }
        }

        public string LastFilter
        {
            get { return _lastFilter; }
            set { SetPropertyChanged(ref _lastFilter, value); }
        }

        public int MaxSuggestionCount
        {
            get { return _maxSuggestionCount; }
            set { SetPropertyChanged(ref _maxSuggestionCount, value); }
        }

        public MatchKind MatchKind
        {
            get { return _matchKind; }
            set
            {
                if (SetPropertyChanged(ref _matchKind, value))
                {
                    switch (value)
                    {
                        case MatchKind.StartsWith:
                            _matchPredicate = StartsWith;
                            break;
                        case MatchKind.EndsWith:
                            _matchPredicate = EndsWith;
                            break;
                        case MatchKind.Exact:
                            _matchPredicate = Exact;
                            break;
                        case MatchKind.Contains:
                        default:
                            _matchPredicate = Contains;
                            break;
                    }
                }
            }
        }

        public IEnumerable GetSuggestions(string filter)
        {
            LastFilter = filter;
            if (string.IsNullOrWhiteSpace(filter))
            {
                if (!AllowEmptyFilter)
                    return null;

                return SupplierList
                        .Take(MaxSuggestionCount)
                        .ToList();
            }

            return
                SupplierList
                    .Where(x => _matchPredicate(x.TenNCC, filter)|| _matchPredicate(x.SoDT, filter))
                    .Take(MaxSuggestionCount)
                    .ToList();
        }

        private bool Contains(string source, string value)
        {
            if (source == null || value == null) return false;
            return source.IndexOf(value, _comparison) > -1;
        }

        private bool EndsWith(string source, string value)
        {
            if (source == null || value == null) return false;
            return source.EndsWith(value, _comparison);
        }

        private bool Exact(string source, string value)
        {
            return string.Equals(source, value, _comparison);
        }

        private bool StartsWith(string source, string value)
        {
            if (source == null || value == null) return false;
            return source.StartsWith(value, _comparison);
        }

        public SupplierSuggesionProvider()
        {
            IgnoreCase = true;
            MatchKind = MatchKind.Contains;
            MaxSuggestionCount = 10;
            SupplierList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);
           
          
        }
      
    }
}
