using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class About : System.Web.UI.Page
{
    public const string emailNotification = "goldwort@gmail.com";
    string con = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = GetComments();
            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();
        }
        catch (Exception)
        {
        }
    }



    private DataSet GetComments()
    {
        var ds = new DataSet();
        string con = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();


        using (MySqlConnection conn = new MySqlConnection(con))
        {
            MySqlDataAdapter da = new MySqlDataAdapter("uspGetHtmlComments", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);
        }

        return ds;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        Mail.SendMe(emailNotification, "", "", "Pure HTML Comments Email: ", txtcmtEmail.Text + "<hr>Name: " + txtName.Text +
            "<hr>Comments: " + txtreview.Text);

        using (MySqlConnection conn = new MySqlConnection(con))
        {
            var da = new MySqlCommand("uspInsertComments", conn);
            da.CommandType = CommandType.StoredProcedure;
            da.Parameters.Add(new MySqlParameter("email", MySqlDbType.VarChar) { Value = txtcmtEmail.Text });
            da.Parameters.Add(new MySqlParameter("comments", MySqlDbType.VarChar) { Value = txtreview.Text });
            da.Parameters.Add(new MySqlParameter("name", MySqlDbType.VarChar) { Value = txtName.Text });
            conn.Open();
            var i = da.ExecuteNonQuery();
        }
        Response.Redirect("index.aspx");
    }
    private void Page_Error(object sender, EventArgs e)
    {
        // Get last error from the server
        Exception exc = Server.GetLastError();
        Mail.SendMe(emailNotification, "", "", "Pure HTML Comments Error", exc.Message + "<br><br><hr>" +
            txtcmtEmail.Text + "Name: " + txtName.Text +
            "<hr>Comments: " + txtreview.Text + "<hr><br>" + exc.StackTrace);
    }
}
