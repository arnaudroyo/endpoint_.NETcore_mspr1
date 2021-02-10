using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

using System.Text.Json;


namespace ENDPOINT
{
    public class Mysql
    {

        private MySqlConnection connection;
        private String connectionString;
        // Constructeur
        public Mysql()
        {
            this.InitConnexion();
        }

        // Méthode pour initialiser la connexion
        private void InitConnexion()
        {
            //
            // Création de la chaîne de connexion
            this.connectionString = "SERVER=localhost; DATABASE=GoStyle; UID=mspr; PASSWORD=asdf";
            this.connection = new MySqlConnection(connectionString);
        }

        // Méthode pour recup les info du code
        public string getPromo(string code)
        {
            string json="false";
            try
            {
                //string sql = "SHOW TABLES;";*

                string sql = $"SELECT * FROM promotion INNER JOIN codePromo on promotion.numeroPromotion = codePromo.numeroPromotion WHERE codePromo.Qrcode = '{code}';";

                // Créez un objet Command.
                MySqlCommand cmd = new MySqlCommand();

                // Établissez la connexion de la commande.
                cmd.Connection = connection;
                cmd.CommandText = sql;

                connection.Open();

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Code myCode = new Code(int.Parse(reader.GetString(0)), int.Parse(reader.GetString(1)), DateTime.Parse(reader.GetString(3)), int.Parse(reader.GetString(4)), reader.GetString(8));
                            json = System.Text.Json.JsonSerializer.Serialize(myCode); //Instead of use JsonConvert.SerializeObject(x.Action);

                            Console.WriteLine(json);

                        }
                    }
                }
                return json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return json;

            }
            finally
            {
                connection.Close();
            }
        }
    }
}
