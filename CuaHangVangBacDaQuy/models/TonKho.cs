using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.models
{
    public class TonKho
    {
        public SanPham SanPham { get; set; }
        public int Stt { get; set; }
        public int TonDau { get; set; }
        public int Mua { get; set; }
        public int Ban { get; set; }
        public int TonCuoi { get; set; }
    }
}
