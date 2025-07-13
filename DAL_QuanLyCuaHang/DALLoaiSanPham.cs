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
    public class DALLoaiSanPham
    {
            public List<LoaiSanPham> SelectBySql(string sql, Dictionary<string, object> parameters = null, CommandType cmdType = CommandType.Text)
            {
                var list = new List<LoaiSanPham>();

                try
                {
                    var args = parameters != null ?
                        parameters.Values.ToList() :
                        new List<object>();

                    using (var reader = DBUtil.Query(sql, args, cmdType))
                    {
                        while (reader.Read())
                        {
                            var entity = new LoaiSanPham
                            {
                                MaLoai = reader["MaLoai"].ToString(),
                                TenLoai = reader["TenLoai"].ToString()
                            };
                            list.Add(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error executing SQL query: " + ex.Message, ex);
                }

                return list;
            }

            public List<LoaiSanPham> SelectAll()
            {
                const string sql = "SELECT MaLoai, TenLoai FROM LoaiSanPham";
                return SelectBySql(sql);
            }

            public LoaiSanPham SelectById(string maLoai)
            {
                const string sql = "SELECT MaLoai, TenLoai FROM LoaiSanPham WHERE MaLoai = @0";
                var parameters = new Dictionary<string, object> { { "@0", maLoai } };
                var results = SelectBySql(sql, parameters);
                return results.FirstOrDefault();
            }

            public string GenerateMaLoaiSanPham()
            {
                const string prefix = "LSP";
                const string sql = "SELECT MAX(MaLoai) FROM LoaiSanPham WHERE MaLoai LIKE @0";

                var parameters = new Dictionary<string, object>
        {
            { "@0", $"{prefix}%" }
        };

                try
                {
                    var result = DBUtil.ScalarQuery(sql, parameters.Values.ToList());
                    if (result != null && result != DBNull.Value && result.ToString().StartsWith(prefix))
                    {
                        string maxCode = result.ToString();
                        int number = int.Parse(maxCode.Substring(prefix.Length));
                        return $"{prefix}{(number + 1):D3}";
                    }
                    return $"{prefix}001";
                }
                catch (Exception ex)
                {
                    throw new Exception("Error generating new product category code: " + ex.Message, ex);
                }
            }

            public int Insert(LoaiSanPham loaiSP)
            {
                const string sql = @"INSERT INTO LoaiSanPham (MaLoai, TenLoai) 
                           VALUES (@0, @1)";

                var parameters = new Dictionary<string, object>
        {
            { "@0", loaiSP.MaLoai },
            { "@1", loaiSP.TenLoai }
        };

                try
                {
                    DBUtil.Update(sql, parameters.Values.ToList());
                    return 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inserting product category: " + ex.Message, ex);
                }
            }

            public int Update(LoaiSanPham loaiSP)
            {
                const string sql = @"UPDATE LoaiSanPham 
                           SET TenLoai = @0
                           WHERE MaLoai = @1";

                var parameters = new Dictionary<string, object>
        {
            { "@0", loaiSP.TenLoai },
            { "@1", loaiSP.MaLoai }
        };

                try
                {
                    DBUtil.Update(sql, parameters.Values.ToList());
                    return 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating product category: " + ex.Message, ex);
                }
            }

            public int Delete(string maLoai)
            {
                const string sql = "DELETE FROM LoaiSanPham WHERE MaLoai = @0";
                var parameters = new Dictionary<string, object> { { "@0", maLoai } };

                try
                {
                    DBUtil.Update(sql, parameters.Values.ToList());
                    return 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting product category: " + ex.Message, ex);
                }
            }
        }
    }
