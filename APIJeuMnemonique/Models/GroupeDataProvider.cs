using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Web;

namespace APIJeuMnemonique.Models
{
    public class GroupeDataProvider
    {
        private static string connectionString = "server=localhost;user=root;password=root;database=jeumnemonique;";

        //Gestion des membres du groupe-------------------------------------------------------

        public static List<Joueur> GetMembresGroupe(int groupeId)
        {
            List<Joueur> listeMembres = new List<Joueur>();
            Joueur membre = null;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText =
                $"SELECT DISTINCT g.id, g.nomGroupe, g.proprietaireGroupe, j.identifiant AS propIdentifiant, m.membre, joueur.identifiant " +
                $"FROM groupe AS g " +
                $"LEFT JOIN membregroupe AS m " +
                $"ON g.id = m.groupe " +
                $"LEFT JOIN joueur " +
                $"ON joueur.courriel = m.membre " +
                $"LEFT JOIN joueur as j " +
                $"on j.courriel = g.proprietaireGroupe " +
                $"WHERE g.id = '{groupeId}';";

            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                membre = new Joueur();
                membre.Courriel = "" + dr["proprietaireGroupe"];
                membre.Identifiant = "" + dr["propIdentifiant"];
                listeMembres.Add(membre);


                if (!dr.IsDBNull(dr.GetOrdinal("membre")))
                {
                    membre = new Joueur();
                    membre.Courriel = "" + dr["membre"];
                    membre.Identifiant = "" + dr["identifiant"];
                    listeMembres.Add(membre);
                }
            }
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("membre")))
                {
                    membre = new Joueur();
                    membre.Courriel = "" + dr["membre"];
                    membre.Identifiant = "" + dr["identifiant"];
                    listeMembres.Add(membre);
                }
            }
            cnx.Close();
            return listeMembres;
        }

        public static bool AjouterMembreAuGroupe(string courriel, int groupeId)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"INSERT INTO membregroupe (groupe, membre) VALUES('{groupeId}','{courriel}');";

            cmd.Prepare();
            bool res;
            try
            {
                res = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) 
            {
                res = false;
            }
            cnx.Close();
            return res;
        }

        public static bool SupprimerMembreDuGroupe(string courriel, int groupeId)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
           $"DELETE FROM membregroupe WHERE groupe='{groupeId}' AND membre='{courriel}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }


        //Gestion des listes partagées du groupe-------------------------------------------------------

        public static List<ListeQuestions> GetListesPartageesDuGroupe(int groupeId)
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
                $"SELECT lg.listePartagee, l.nomListe, l.proprietaire, qr.id AS idQr, qr.question, qr.reponse " +
                $"FROM liste_groupe as lg " +
                $"INNER JOIN listedequestions as l " +
                $"ON lg.listePartagee = l.id " +
                $"LEFT JOIN questionreponse as qr " +
                $"ON qr.liste = lg.listePartagee " +
                $"WHERE lg.groupe = '{groupeId}';";

            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listeQuestions = new ListeQuestions();
                listeQuestions.ListeQuestionsId = (int)dr["listePartagee"];
                listeQuestions.NomListe = "" + dr["nomListe"];
                listeQuestions.Proprietaire = new Joueur();
                listeQuestions.Proprietaire.Courriel = "" + dr["proprietaire"];
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
                    listeQuestions.ListeQuestionsId = (int)dr["listePartagee"];
                    listeQuestions.NomListe = "" + dr["nomListe"];
                    listeQuestions.Proprietaire = new Joueur();
                    listeQuestions.Proprietaire.Courriel = "" + dr["proprietaire"];
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

        public static bool PartagerUneListeAuGroupe(int listeId, int groupeId)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"INSERT INTO liste_groupe (groupe, listePartagee) VALUES('{groupeId}','{listeId}');";

            cmd.Prepare();
            bool res;
            try
            {
                res = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                res = false;
            }
            cnx.Close();
            return res;
        }

        public static bool SupprimerUneListePartagee(int listeId, int groupeId)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
           $"DELETE FROM liste_groupe WHERE groupe='{groupeId}' AND listePartagee='{listeId}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }


        //Gestion du groupe-------------------------------------------------------

        public static List<Groupe> GetGroupesParJoueur(string courriel)
        {
            List<Groupe> listeGroupes = new List<Groupe>();
            Groupe groupe = null;
            Joueur proprietaire = null;

            DbConnection cnx = new MySqlConnection();
            cnx.ConnectionString = connectionString;
            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText =
                $"SELECT DISTINCT g.id, g.nomGroupe, g.proprietaireGroupe " +
                $"FROM groupe AS g " +
                $"LEFT JOIN membregroupe AS m " +
                $"ON g.id = m.groupe " +
                $"WHERE m.membre = '{courriel}' OR g.proprietaireGroupe = '{courriel}';";

            cmd.Prepare();

            DbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                groupe = new Groupe();
                groupe.GroupeId = (int)dr["id"];
                groupe.NomGroupe = "" + dr["nomGroupe"];
                proprietaire = new Joueur();
                proprietaire.Courriel = "" + dr["proprietaireGroupe"];
                groupe.Proprietaire = proprietaire;
                listeGroupes.Add(groupe);

            }
            while (dr.Read())
            {
                groupe = new Groupe();
                groupe.GroupeId = (int)dr["id"];
                groupe.NomGroupe = "" + dr["nomGroupe"];
                proprietaire = new Joueur();
                proprietaire.Courriel = "" + dr["proprietaireGroupe"];
                groupe.Proprietaire = proprietaire;
                listeGroupes.Add(groupe);
            }
            cnx.Close();
            return listeGroupes ;
        }

        public static bool AjouterGroupe(Groupe groupe)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = cnx.CreateCommand();
            cmd.CommandText = $"INSERT INTO groupe (nomGroupe,proprietaireGroupe) VALUES(@nomGroupe, @proprietaireGroupe)";
            DbParameter param;
            param = new MySqlParameter
            {
                ParameterName = "nomGroupe",
                DbType = System.Data.DbType.String,
                Value = groupe.NomGroupe
            };
            cmd.Parameters.Add(param);
            param = new MySqlParameter
            {
                ParameterName = "proprietaireGroupe",
                DbType = System.Data.DbType.String,
                Value = groupe.Proprietaire.Courriel
            };
            cmd.Parameters.Add(param);

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;

            cnx.Close();
            return res;
        }

        public static bool SupprimerGroupe(int id)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
           $"DELETE FROM groupe WHERE id='{id}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }

        public static bool ModifierGroupe(Groupe groupe)
        {
            DbConnection cnx = new MySqlConnection();

            cnx.ConnectionString = connectionString;

            cnx.Open();

            DbCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;

            cmd.CommandText =
            $"UPDATE groupe SET nomGroupe='{groupe.NomGroupe}' WHERE id='{groupe.GroupeId}'";

            cmd.Prepare();
            bool res = cmd.ExecuteNonQuery() > 0;
            cnx.Close();
            return res;
        }
    }
}