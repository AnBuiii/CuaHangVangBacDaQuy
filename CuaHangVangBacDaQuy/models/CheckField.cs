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
        public static bool checkNumber(string str)
        {
            if(regexChecknumber.IsMatch(str)) return true;
            else
            {
                MessageBox.Show("Vui lòng chỉ nhập số!");
                return false;
            }
        }
    }
}
