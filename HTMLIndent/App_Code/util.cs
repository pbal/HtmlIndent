using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for util
/// </summary>
public class util
{
    public static IEnumerable<char> GetCapitalAlphabet()
    {
        for (char c = 'A'; c <= 'Z'; c++)
        {
            yield return c;
        }
    }

    public static IEnumerable<char> GetLowerAlphabet()
    {
        for (char c = 'a'; c <= 'z'; c++)
        {
            yield return c;
        }
    }

    public static void DBNull(string input, string ip, string page, bool css, bool script,
        bool compress, int spaces)
    {
        
        try
        {
            using (var conn = new MySqlConnection(
                                 System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
                var da = new MySqlCommand("usp_insertForHTML", conn);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.Add(new MySqlParameter("ip", MySqlDbType.VarChar) { Value = ip });
                da.Parameters.Add(new MySqlParameter("input", MySqlDbType.Text) { Value = input });
                da.Parameters.Add(new MySqlParameter("page", MySqlDbType.VarChar) { Value = page });
                da.Parameters.Add(new MySqlParameter("css", MySqlDbType.Bit) { Value = css });
                da.Parameters.Add(new MySqlParameter("script", MySqlDbType.Bit) { Value = script });
                da.Parameters.Add(new MySqlParameter("compress", MySqlDbType.Bit) { Value = compress });
                da.Parameters.Add(new MySqlParameter("spaces", MySqlDbType.Int32) { Value = spaces });
                conn.Open();
                var i = da.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public static string GetTagName(StringBuilder r, bool bClosingTag)
    {
        var ar = r.ToString().Split(' ', '=', '\r');
        return !bClosingTag ? ar[0].Trim('<', '>', ' ') : ar[ar.Length - 1].TrimEnd('>').TrimStart('<', '/', '\\');
    }
    public static string AddSpace(int spaceCount, int indentLevel)
    {
        var tabLength = string.Empty;
        for (int i = 0; i < spaceCount * indentLevel; i++)
        {
            tabLength += " ";
        }
        return tabLength;
    }
}
