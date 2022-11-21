using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.XuLyNghiepVu
{
    internal class QuanLyMonAn
    {
        public static List<List<string>> getAllBienTheMonAn()
        {
            string query = "SELECT * FROM BienTheMonAn";
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }

        public static List<List<string>> getCongThucBienTheMonAn(string ID)
        {
            string query = $"SELECT CT.BTMA_ID,BTMA.BTMA_Ten, NL.NL_Ten, CT.CT_SoLuong, NL.NL_DonViTinh " +
                            $"FROM CongThuc as CT, NguyenLieu as NL, BienTheMonAn as BTMA " +
                            $"WHERE CT.NL_ID = NL.NL_ID AND CT.BTMA_ID = BTMA.BTMA_ID AND CT.BTMA_ID = {ID}";
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }
    }
}
