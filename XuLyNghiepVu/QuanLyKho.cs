using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang.XuLyNghiepVu
{
    internal class QuanLyKho
    {
        public static List<List<string>> getHangHetHang()
        {
            string query = $"SELECT NL_ID, NL_Ten, NL_NguongToiThieu, NL_Anh " +
                            $"FROM NguyenLieu as NL " +
                            $"WHERE NL_ID NOT IN( SELECT NL.NL_ID " +
                                                $"FROM (SELECT LNL.NL_ID, SUM(LNL.LNL_SoLuong) as NL_TongSoLuong " +
                                                        $"FROM LoNguyenLieu as LNL GROUP BY LNL.NL_ID) as LNL , NguyenLieu as NL " +
                                                        $"WHERE LNL.NL_ID = NL.NL_ID AND NL_TongSoLuong<> 0) AND NL_NgungSuDung = '0'";
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        } 

        public static List<List<string>> getHangDuoiNguongToiThieu()
        {
            string query = $"SELECT NL.NL_ID, NL_Ten, NL_Anh, NL_TongSoLuong, NL_NguongToiThieu " +
                            $"FROM(SELECT LNL.NL_ID, SUM(LNL.LNL_SoLuong) as NL_TongSoLuong " +
                                    $"FROM LoNguyenLieu as LNL GROUP BY LNL.NL_ID) as LNL , NguyenLieu as NL " +
                            $"WHERE LNL.NL_ID = NL.NL_ID AND NL_TongSoLuong > 0 " +
                                                        $"AND NL_TongSoLuong<NL.NL_NguongToiThieu " +
                                                        $"AND NL.NL_NgungSuDung = 0";
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }
         
        public static List<List<string>> getHangHetHanSuDung()
        {
            DateTime datetime = DateTime.Now;
            string today = datetime.ToString("yyyyMMdd");
            string query = $"SELECT LNL.*,  NL_Ten, NL_Anh, LNL.PN_ID " +
                            $"FROM LoNguyenLieu as LNL, NguyenLieu as NL " +
                            $"WHERE LNL.NL_ID = NL.NL_ID AND LNL.LNL_NgayHetHan < '20221119' AND LNL.LNL_SoLuongConLai > 0";
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }

        public static List<List<string>> getHangSapHetHanSuDung()
        {
            DateTime datetime = DateTime.Now.AddDays(1);
            string start = datetime.ToString("yyyyMMdd");
            string end = datetime.AddDays(6).ToString("yyyyMMdd");
            string query = $"SELECT LNL.*,  NL_Ten, NL_Anh, LNL.PN_ID " +
                            $"FROM LoNguyenLieu as LNL, NguyenLieu as NL " +
                            $"WHERE LNL.NL_ID = NL.NL_ID AND LNL.LNL_NgayHetHan > '{start}' AND LNL.LNL_NgayHetHan < '{end}' AND LNL.LNL_SoLuongConLai > 0";
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }

        public static List<List<string>> getAllNguyenLieu(bool conSuDung = true, bool ngungSuDung = false)
        {
            string query = "";
            if(conSuDung && ngungSuDung)
            {
                query = $"SELECT * FROM NguyenLieu";
            } else if (!conSuDung && ngungSuDung)
            {
                query = $"SELECT * FROM NguyenLieu WHERE NL_NgungSuDung = 1";
            } else if (conSuDung && !ngungSuDung)
            {
                query = $"SELECT * FROM NguyenLieu WHERE NL_NgungSuDung = 0";
            } else
            {
                return new List<List<string>>();
            }
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }

        public static List<string> getNguyenLieu(string id, bool conSuDung = true, bool ngungSuDung = false)
        {
            string query = "";
            if (conSuDung && ngungSuDung)
            {
                query = $"SELECT * FROM NguyenLieu WHERE NL_ID = {id}";
            }
            else if (!conSuDung && ngungSuDung)
            {
                query = $"SELECT * FROM NguyenLieu WHERE NL_ID = {id} AND NL_NgungSuDung = 1";
            }
            else if (conSuDung && !ngungSuDung)
            {
                query = $"SELECT * FROM NguyenLieu WHERE NL_ID = {id} AND NL_NgungSuDung = 0";
            }
            else
            {
                return new List<string>();
            }
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);
            List<string> rutGon = result[0];

            return rutGon;
        }

        public static List<List<string>> searchNguyenLieu(string timKiem, bool conSuDung = true, bool ngungSuDung = false)
        {
            string extra = "";
            if (!conSuDung && ngungSuDung)
            {
                extra = $" AND NL_NgungSuDung = 1";
            }
            else if (conSuDung && !ngungSuDung)
            {
                extra = $" AND NL_NgungSuDung = 0";
            }
            else if(!conSuDung && !ngungSuDung)
            {
                return new List<List<string>>();
            }
            string query = $"SELECT * FROM NguyenLieu WHERE NL_ID LIKE '%{timKiem}%' OR NL_Ten LIKE '%{timKiem}%'" + extra;
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }

        public static bool themNguyenLieu(string ten, string dvtinh, string nguongToiThieu, string donGia, string linkAnh = "")
        {
            string query = $"INSERT INTO NguyenLieu (NL_Ten, NL_DonViTinh, NL_NguongToiThieu, NL_DonGia, NL_Anh)\r\nVALUES (N'{ten}', N'{dvtinh}', '{nguongToiThieu}', '{donGia}', '{linkAnh}')";
            bool result = ConnectDB.ConnectDB.InsertData(query);

            return result;
        }

        public static bool suaNguyenLieu(string id, string ten, string dvtinh, string nguongToiThieu, string donGia, string linkAnh)
        {
            string query = $"UPDATE NguyenLieu\r\nSET NL_Ten = N'{ten}', NL_DonViTinh = N'{dvtinh}', NL_NguongToiThieu = '{nguongToiThieu}', NL_DonGia = '{donGia}', NL_Anh = '{linkAnh}'\r\nWHERE NL_ID = {id}";
            bool result = ConnectDB.ConnectDB.UpdateData(query);

            return result;
        }

        public static bool xoaNguyenLieu(string id)
        {
            string query = $"UPDATE NguyenLieu\r\nSET NL_NgungSuDung = 1\r\nWHERE NL_ID = {id}";
            bool result = ConnectDB.ConnectDB.UpdateData(query);

            return result;
        }

        public static bool suDungNguyenLieu(string id)
        {
            string query = $"UPDATE NguyenLieu\r\nSET NL_NgungSuDung = 0\r\nWHERE NL_ID = {id}";
            bool result = ConnectDB.ConnectDB.UpdateData(query);

            return result;
        }

        public static List<List<string>> getAllLoNguyenLieu()
        {
            string query = "SELECT NL.NL_Ten, LNL.* " +
                            "FROM LoNguyenLieu as LNL, NguyenLieu as NL " +
                            "WHERE LNL.NL_ID = NL.NL_ID";
            
            List<List<string>> result = ConnectDB.ConnectDB.ReadData(query);

            return result;
        }
    }
}
