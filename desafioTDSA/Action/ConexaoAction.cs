using MySql.Data.MySqlClient;
using desafioTDSA.Log;
using System;
using System.Data;

namespace desafioTDSA.Action
{
    class Conn
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Contrutor
        public Conn()
        {
            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                string connectionString;

                server = "sandaliaria.com.br";
                database = "sandalia_loja";
                uid = "sandalia_userGer";
                password = "S@nda#2017";
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; convert zero datetime=true";

                connection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao gerar string de conexão --- {ex}");
            }
        }

        //Abrir Conexão
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                switch (ex.Number)
                {
                    case 0:
                        RegistraLog.Log($"Erro ao conecatr com o banco de dados --- {ex}");
                        break;

                    case 1045:
                        RegistraLog.Log($"Erro 1045 --- {ex}");
                        break;
                }
                return false;
            }
        }

        //Fechar Conexão
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                RegistraLog.Log($"Erro ao fechar conexão --- {ex}");
                return false;
            }
        }

        //Metodo para ação ativa
        public void Sql(String SQLq)
        {
            try
            {
                string query = SQLq;

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao executar comando MySQL --- Query: {SQLq} --- {ex}");
            }

        }

        //Metodo para ação passiva
        public DataTable SqlDataTable(String SQLq)
        {
            string query = SQLq;
            DataTable dTable = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 99999;

                MySqlDataAdapter adap = new MySqlDataAdapter(query, connection);
                adap.Fill(dTable);

                this.CloseConnection();
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao retornar dados para o DataTable --- Query: {SQLq} --- {ex}");
            }
            return dTable;
        }

    }
}