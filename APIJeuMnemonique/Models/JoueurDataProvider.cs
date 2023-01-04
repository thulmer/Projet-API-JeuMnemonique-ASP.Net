using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Ajax.Utilities;

namespace APIJeuMnemonique.Models
{
    public class JoueurDataProvider
    {
        private static string connectionString = "server=localhost;user=root;password=root;database=jeumnemonique;";

        private static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            Console.WriteLine(hashed);
            Console.WriteLine(Convert.ToBase64String(salt));
            Console.WriteLine(Convert.ToBase64String(salt) + ":" + hashed);

            return Convert.ToBase64String(salt) + ":" + hashed;
        }

        private static bool verifierHashedPassword(string password, string hash, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed == hash;
        }

        public static List<Joueur> GetJoueurs() {

            List<Joueur> resultats = new List<Joueur>();
            Joueur joueur;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "joueur";
            cmd.CommandType = CommandType.TableDirect;

            DbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                joueur = new Joueur();

                joueur.Identifiant = "" + dr["identifiant"];
                joueur.Courriel = "" + dr["courriel"];

                resultats.Add(joueur);
            }
            cnx.Close();
            return resultats;
        }
        public static Joueur GetByCourriel(string courriel)
        {
            Joueur joueur;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();


            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "SELECT * FROM joueur WHERE courriel=@courriel";

            MySqlParameter identifiantChercheParam = new MySqlParameter("@courriel", MySqlDbType.VarChar);
            identifiantChercheParam.Value = courriel;
            cmd.Parameters.Add(identifiantChercheParam);
            cmd.CommandType = CommandType.TableDirect;
            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                joueur = new Joueur();

                joueur.Identifiant = "" + dr["identifiant"];
                joueur.Courriel = "" + dr["courriel"];

                return joueur;
            }
            cnx.Close();
            return null;
        }

        public static List<Joueur> FindByIdentifiant(string identifiant)
        {
            List<Joueur> resultats = new List<Joueur>();
            Joueur joueur;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();


            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "SELECT * FROM joueur WHERE identifiant LIKE @identifiant";

            MySqlParameter identifiantChercheParam = new MySqlParameter("@identifiant", MySqlDbType.VarChar);
            identifiantChercheParam.Value = "%" + identifiant + "%";
            cmd.Parameters.Add(identifiantChercheParam);


            cmd.CommandType = CommandType.TableDirect;
            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                joueur = new Joueur();

                joueur.Identifiant = "" + dr["identifiant"];
                joueur.Courriel = "" + dr["courriel"];

                resultats.Add(joueur);
            }
            cnx.Close();
            return resultats;
        }

        public static bool Connexion(Joueur joueur)
        {

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM joueur WHERE courriel=@courriel";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "courriel",
                DbType = System.Data.DbType.String,
                Value = joueur.Courriel
            };
            cmd.Parameters.Add(param);

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var hashedSaltedMdp = "" + dr["motDePasse"];
                var hashedMdp = hashedSaltedMdp.Substring(hashedSaltedMdp.IndexOf(":")+1);
                var salt = hashedSaltedMdp.Substring(0,hashedSaltedMdp.IndexOf(":"));
                Console.WriteLine(hashedSaltedMdp);
                Console.WriteLine(hashedMdp);
                Console.WriteLine(salt);

                return verifierHashedPassword(joueur.MotDePasse, hashedMdp, salt);
            }
            cnx.Close();
            return false;
        }

        public static bool GetByIdentifiantEtCourriel(string courriel, string identifiant)
        {
            Joueur joueur;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT * FROM joueur WHERE courriel=@courriel AND identifiant=@identifiant";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "courriel",
                DbType = System.Data.DbType.String,
                Value = courriel
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "identifiant",
                DbType = System.Data.DbType.String,
                Value = identifiant
            };
            cmd.Parameters.Add(param);

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                joueur = new Joueur();

                joueur.Identifiant = "" + dr["identifiant"];
                joueur.Courriel = "" + dr["courriel"];

                return true;
            }
            cnx.Close();
            return false;
        }

        public static bool AjouterJoueur(Joueur joueur)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = $"INSERT INTO joueur (identifiant,courriel,motDePasse) VALUES(@identifiant, @courriel, @motDePasse)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "identifiant",
                DbType = System.Data.DbType.String,
                Value = joueur.Identifiant
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "courriel",
                DbType = System.Data.DbType.String,
                Value = joueur.Courriel
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "motDePasse",
                DbType = System.Data.DbType.String,
                Value = HashPassword(joueur.MotDePasse)
            };
            cmd.Parameters.Add(param);


            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }

        public static bool SupprimerJoueur(string courriel)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"DELETE FROM joueur WHERE courriel='{courriel}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }
        public static bool ModifierJoueur(Joueur joueur)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"UPDATE joueur SET identifiant='{joueur.Identifiant}', courriel='{joueur.Courriel}', motDePasse='{HashPassword(joueur.MotDePasse)}' WHERE courriel='{joueur.Courriel}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }

    }
}