using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CuaHangVangBacDaQuy.models
{
    public class CheckField
    {
        static private Regex regexChecknumber = new Regex("^[0-9]+$");
        static private Regex regexCheckPhoneNumber =  new Regex("^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$");
        static private Regex regexCheckCurrency = new Regex("^/$?[0-9][0-9,]*[0-9]/.?[0-9]{0,2}$/i");
        public static bool CheckNumber(string str)
        {
            if(regexChecknumber.IsMatch(str)) return true;
           
                return false;
            
        }
        public static bool CheckPhone(string str)
        {
            if (regexCheckPhoneNumber.IsMatch(str)) return true;
                return false;
            
        }
        public static bool CheckCurrency(string str)
        {
            if (regexCheckCurrency.IsMatch(str)) return true;
            return false;

        }
    }
}
