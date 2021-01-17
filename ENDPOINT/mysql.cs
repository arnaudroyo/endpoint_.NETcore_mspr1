using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

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
            // Création de la chaîne de connexion
            this.connectionString = "SERVER=217.69.0.125; DATABASE=GoStyle; UID=mspr; PASSWORD=asdf";
            this.connection = new MySqlConnection(connectionString);
        }

        // Méthode pour ajouter un contact
        public string SelectTest(string code)
        {
            string res = "";
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
                            res += $"Promotion n°{reader.GetString(0)} ; Reduction de {reader.GetString(1)}% pour un minimum d'achat de {reader.GetString(1)} euros jusqu'au {reader.GetString(3)}.";
                            Console.WriteLine(res);
                        }
                    }
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return res;

            }
            finally
            {
                connection.Close();
            }
        }
    }
}
