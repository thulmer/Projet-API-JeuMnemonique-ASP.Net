using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;
using Microsoft.Ajax.Utilities;

namespace APIJeuMnemonique.Models
{
    public class ListeQuestionsDataProvider
    {
        private static string connectionString = "server=localhost;user=root;password=root;database=jeumnemonique;";

        public static List<ListeQuestions> GetListesParJoueur(string courriel)
        {
            List<ListeQuestions> resultats = new List<ListeQuestions>();
            ListeQuestions listeQuestions = null;
            QuestionReponse questionReponse = null;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText =
                $"SELECT DISTINCT l.id, l.nomListe, l.proprietaire, qr.id AS idQr, qr.question, qr.reponse " +
                $"FROM listedequestions AS l " +
                $"LEFT JOIN questionreponse AS qr " +
                $"ON qr.liste = l.id " +
                $"INNER JOIN joueur " +
                $"ON joueur.courriel = l.proprietaire AND joueur.courriel = '{courriel}'";

            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listeQuestions = new ListeQuestions();
                listeQuestions.ListeQuestionsId = (int)dr["id"];
                listeQuestions.NomListe = "" + dr["nomListe"];
                listeQuestions.Proprietaire = new Joueur();
                listeQuestions.Proprietaire.Courriel = courriel;
                if (!dr.IsDBNull(dr.GetOrdinal("idQr")))
                {
                    questionReponse = new QuestionReponse();
                    questionReponse.QuestionReponseId = (int)dr["idQr"];
                    questionReponse.Question = "" + dr["question"];
                    questionReponse.Reponse = "" + dr["reponse"];
                    listeQuestions.Add(questionReponse);
                }
                resultats.Add(listeQuestions);
            }
            while (dr.Read())
            {
                if (listeQuestions.NomListe == "" + dr["nomListe"] && !dr.IsDBNull(dr.GetOrdinal("idQr")))
                {
                    questionReponse = new QuestionReponse();
                    questionReponse.QuestionReponseId = (int)dr["idQr"];
                    questionReponse.Question = "" + dr["question"];
                    questionReponse.Reponse = "" + dr["reponse"];
                    listeQuestions.Add(questionReponse);
                }
                else
                {
                    listeQuestions = new ListeQuestions();
                    listeQuestions.ListeQuestionsId = (int)dr["id"];
                    listeQuestions.NomListe = "" + dr["nomListe"];
                    listeQuestions.Proprietaire = new Joueur();
                    listeQuestions.Proprietaire.Courriel = courriel;
                    if (!dr.IsDBNull(dr.GetOrdinal("idQr")))
                    {
                        questionReponse = new QuestionReponse();
                        questionReponse.QuestionReponseId = (int)dr["idQr"];
                        questionReponse.Question = "" + dr["question"];
                        questionReponse.Reponse = "" + dr["reponse"];
                        listeQuestions.Add(questionReponse);
                    }
                    resultats.Add(listeQuestions);
                }
            }
            cnx.Close();
            return resultats;
        }

        public static ListeQuestions GetByJoueurEtNom(string courriel, String nom)
        {
            ListeQuestions listeQuestions = null;
            QuestionReponse questionReponse = null;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = $"SELECT DISTINCT l.id, l.nomListe, l.proprietaire, qr.id AS idQr, qr.question, qr.reponse " +
                $"FROM listedequestions AS l " +
                $"LEFT JOIN questionreponse AS qr " +
                $"ON qr.liste = l.id " +
                $"INNER JOIN joueur " +
                $"ON joueur.courriel = l.proprietaire AND joueur.courriel = '{courriel}' " +
                $"WHERE l.nomListe = '{nom}';";
            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listeQuestions = new ListeQuestions();
                listeQuestions.ListeQuestionsId = (int)dr["id"];
                listeQuestions.NomListe = "" + dr["nomListe"];
                listeQuestions.Proprietaire = new Joueur();
                listeQuestions.Proprietaire.Courriel = courriel;
                if (!dr.IsDBNull(dr.GetOrdinal("idQr")))
                {
                    questionReponse = new QuestionReponse();
                    questionReponse.QuestionReponseId = (int)dr["idQr"];
                    questionReponse.Question = "" + dr["question"];
                    questionReponse.Reponse = "" + dr["reponse"];
                    listeQuestions.Add(questionReponse);
                }
            }
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("idQr")))
                {
                    questionReponse = new QuestionReponse();
                    questionReponse.QuestionReponseId = (int)dr["idQr"];
                    questionReponse.Question = "" + dr["question"];
                    questionReponse.Reponse = "" + dr["reponse"];
                    listeQuestions.Add(questionReponse);
                }
            }
            cnx.Close();
            return listeQuestions;
        }

        public static ListeQuestions GetByJoueurEtId(string courriel, int id)
        {
            ListeQuestions listeQuestions = null;
            QuestionReponse questionReponse = null;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = $"SELECT DISTINCT l.id, l.nomListe, l.proprietaire, qr.id AS idQr, qr.question, qr.reponse " +
                $"FROM listedequestions AS l " +
                $"LEFT JOIN questionreponse AS qr " +
                $"ON qr.liste = l.id " +
                $"INNER JOIN joueur " +
                $"ON joueur.courriel = l.proprietaire AND joueur.courriel = '{courriel}' " +
                $"WHERE l.id = '{id}';";
            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listeQuestions = new ListeQuestions();
                listeQuestions.ListeQuestionsId = (int)dr["id"];
                listeQuestions.NomListe = "" + dr["nomListe"];
                listeQuestions.Proprietaire = new Joueur();
                listeQuestions.Proprietaire.Courriel = courriel;
                if (!dr.IsDBNull(dr.GetOrdinal("idQr")))
                {
                    questionReponse = new QuestionReponse();
                    questionReponse.QuestionReponseId = (int)dr["idQr"];
                    questionReponse.Question = "" + dr["question"];
                    questionReponse.Reponse = "" + dr["reponse"];
                    listeQuestions.Add(questionReponse);
                }
            }
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("idQr")))
                {
                    questionReponse = new QuestionReponse();
                    questionReponse.QuestionReponseId = (int)dr["idQr"];
                    questionReponse.Question = "" + dr["question"];
                    questionReponse.Reponse = "" + dr["reponse"];
                    listeQuestions.Add(questionReponse);
                }
            }
            cnx.Close();
            return listeQuestions;
        }

        public static bool AjouterListeQuestions(ListeQuestions liste)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();


            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = $"INSERT INTO listedequestions (nomListe,proprietaire) VALUES(@nomListe, @proprietaire)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "nomListe",
                DbType = System.Data.DbType.String,
                Value = liste.NomListe
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "proprietaire",
                DbType = System.Data.DbType.String,
                Value = liste.Proprietaire.Courriel
            };
            cmd.Parameters.Add(param);
        
            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;

            cnx.Close();
            return res;
        }

        public static bool SupprimerListeQuestions(int id)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
           $"DELETE FROM listedequestions WHERE id='{id}'";
           // $"DELETE listedequestions, questionreponse FROM listedequestions INNER JOIN questionreponse ON listedequestions.id = questionreponse.liste and listedequestions.id = '{id}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }

        public static bool ModifierListeQuestions(ListeQuestions listeQuestions)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"UPDATE listedequestions SET nomListe='{listeQuestions.NomListe}', proprietaire='{listeQuestions.Proprietaire.Courriel}' WHERE id='{listeQuestions.ListeQuestionsId}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }
    }
}