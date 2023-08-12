using System.Data;
using MySql.Data.MySqlClient;

namespace Elite_db.operations;

public partial class InsertNewCostumer
{
    private string _cellular;
    private string _cf;
    private string _email;
    private string _name;
    private string _surname;
    private readonly MySqlConnection _con;

    public InsertNewCostumer(MySqlConnection mySqlConnection)
    {
        InitializeComponent();
        _con = mySqlConnection;
    }

    private void InsertClicked(object sender, EventArgs e)
    {
        _name = Name.Text;
        _surname = Surname.Text;
        _cf = CF.Text;
        _cellular = Cellular.Text;
        _email = Email.Text;

        ConfirmBtn.IsEnabled = true;
    }

    private void ConfirmClicked(object sender, EventArgs e)
    {
        try {
            _con.Open();
            var insertQuery =
                "INSERT INTO CLIENTE(Data_Scadenza, Nome, Cognome, CF, Cellulare_Personale, Mail_Personale) " +
                "VALUES (date_add(curdate(), INTERVAL 1 YEAR), @name, @surname, @cf, @cellular, @email)";
            var cmd = new MySqlCommand(insertQuery, _con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", _name);
            cmd.Parameters.AddWithValue("@surname", _surname);
            cmd.Parameters.AddWithValue("@cf", _cf);
            cmd.Parameters.AddWithValue("@cellular", _cellular);
            cmd.Parameters.AddWithValue("@email", _email);

            if (cmd.ExecuteNonQuery() == 1) {
                ConfirmBtn.Text = "Customer inserted!";
                ConfirmBtn.IsEnabled = false;
            }
        }
        catch {
            ConfirmBtn.Text = "Error, retry!";
        }
        finally {
            _con.Close();
        }
    }
}