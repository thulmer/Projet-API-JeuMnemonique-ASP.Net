using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace APIJeuMnemonique.Models
{
    public class QuestionReponseDataProvider
    {
        private static string connectionString = "server=localhost;user=root;password=root;database=jeumnemonique;";

        public static List<QuestionReponse> GetQuestionReponseParListe(int listeId)
        {

            List<QuestionReponse> resultats = new List<QuestionReponse>();
            QuestionReponse questionReponse;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText =
            $"SELECT * from questionreponse where liste='{listeId}'";
            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                questionReponse = new QuestionReponse();
                questionReponse.QuestionReponseId = (int)dr["id"];
                questionReponse.Question = "" + dr["question"];
                questionReponse.Reponse = "" + dr["reponse"];

                resultats.Add(questionReponse);
            }
            cnx.Close();
            return resultats;
        }

        public static bool AjouterQuestionReponse(QuestionReponse questionReponse, int listeId)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = $"INSERT INTO questionreponse (question,reponse,liste) VALUES(@question, @reponse, @liste)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "question",
                DbType = System.Data.DbType.String,
                Value = questionReponse.Question
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "reponse",
                DbType = System.Data.DbType.String,
                Value = questionReponse.Reponse
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "liste",
                DbType = System.Data.DbType.Int32,
                Value = Convert.ToInt32(listeId)
            };
            cmd.Parameters.Add(param);



            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }

        public static bool SupprimerQuestionReponse(int id)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
           $"DELETE FROM questionreponse WHERE id='{id}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }

        public static bool ModifierQuestionReponse(QuestionReponse questionReponse)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"UPDATE questionreponse SET question='{questionReponse.Question}', reponse='{questionReponse.Reponse}' WHERE id='{questionReponse.QuestionReponseId}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }
    }
}