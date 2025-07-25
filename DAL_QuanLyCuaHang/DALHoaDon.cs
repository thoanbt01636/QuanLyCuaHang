using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyCuaHang
{
    public class DALHoaDon
    {
        public string TaoMaTuDong()
        {
            string prefix = "HD";
            string sql = "SELECT MaHD FROM HoaDon";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        string maHD = reader["MaHD"].ToString();
                        if (maHD.StartsWith(prefix) && int.TryParse(maHD.Substring(prefix.Length), out int number))
                        {
                            existingNumbers.Add(number);
                        }
                    }
                }

                int newNumber = 1;
                existingNumbers.Sort();

                foreach (int number in existingNumbers)
                {
                    if (number == newNumber)
                        newNumber++;
                    else if (number > newNumber)
                        break;
                }

                return $"{prefix}{newNumber:D4}";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(HoaDon hd)
        {
            try
            {
                string sql = @"INSERT INTO HoaDon (MaHD, MaNV, NgayLap, TrangThai) 
                                   VALUES (@0, @1, @2, @3)";
                var args = new List<object>
                    {
                        hd.MaHD,
                        hd.MaNV,
                        hd.NgayLap,
                        hd.TrangThai
                    };
                DBUtil.Update(sql, args);
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi thêm hóa đơn: " + e.Message);
            }
        }

        public void Update(HoaDon hd)
        {
            try
            {
                string sql = @"UPDATE HoaDon 
                                   SET MaNV = @0, NgayLap = @1, TrangThai = @2 
                                   WHERE MaHD = @3";
                var args = new List<object>
                    {
                        hd.MaNV,
                        hd.NgayLap,
                        hd.TrangThai,
                        hd.MaHD
                    };
                DBUtil.Update(sql, args);
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi cập nhật hóa đơn: " + e.Message);
            }
        }

        public void Delete(string maHD)
        {
            try
            {
                string sql = "DELETE FROM HoaDon WHERE MaHD = @0";
                var args = new List<object> { maHD };
                DBUtil.Update(sql, args);
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi xóa hóa đơn: " + e.Message);
            }
        }

        public List<HoaDon> SelectAll(string maNV = null)
        {
            var list = new List<HoaDon>();
            try
            {
                string sql = "SELECT MaHD, MaNV, NgayLap, TrangThai FROM HoaDon";
                var args = new List<object>();
                if (!string.IsNullOrEmpty(maNV))
                {
                    sql += " WHERE MaNV = @0";
                    args.Add(maNV);
                }

                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    while (reader.Read())
                    {
                        var hd = new HoaDon
                        {
                            MaHD = reader["MaHD"].ToString(),
                            MaNV = reader["MaNV"].ToString(),
                            NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                            TrangThai = Convert.ToBoolean(reader["TrangThai"])
                        };
                        list.Add(hd);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public HoaDon SelectByID(string maHD)
        {
            try
            {
                string sql = "SELECT MaHD, MaNV, NgayLap, TrangThai FROM HoaDon WHERE MaHD = @0";
                var args = new List<object> { maHD };

                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    if (reader.Read())
                    {
                        var hd = new HoaDon
                        {
                            MaHD = reader["MaHD"].ToString(),
                            MaNV = reader["MaNV"].ToString(),
                            NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                            TrangThai = Convert.ToBoolean(reader["TrangThai"])
                        };
                        return hd;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    }
