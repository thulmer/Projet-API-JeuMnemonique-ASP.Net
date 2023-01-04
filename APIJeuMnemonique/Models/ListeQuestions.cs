using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace APIJeuMnemonique.Models
{
    [DataContract]
    public class ListeQuestions
    {
        public ListeQuestions()
        {
            this.QuestionsReponses = new List<QuestionReponse>();
        }

        public ListeQuestions(int id, string nomListe, Joueur propietaire, List<QuestionReponse> questionsReponses)
        {
            this.ListeQuestionsId = id;
            this.NomListe = nomListe;
            this.Proprietaire = propietaire;
            this.QuestionsReponses = questionsReponses;
        }

        public ListeQuestions(string nomListe, Joueur propietaire, List<QuestionReponse> questionsReponses)
        {
            this.NomListe = nomListe;
            this.Proprietaire = propietaire;
            this.QuestionsReponses = questionsReponses;
        }

        public ListeQuestions(string nomListe, Joueur propietaire)
        {
            this.NomListe = nomListe;
            this.Proprietaire = propietaire;
            this.QuestionsReponses = new List<QuestionReponse>();
        }

        [DataMember]
        public int ListeQuestionsId { get; set; }
        [DataMember]
        public string NomListe { get; set; }
        [DataMember]
        public Joueur Proprietaire { get; set; }
        [DataMember]
        public List<QuestionReponse> QuestionsReponses { get; set; }

        public bool Add(QuestionReponse questionReponse) { 
            this.QuestionsReponses.Add(questionReponse);
            return true;
        }

    }
}