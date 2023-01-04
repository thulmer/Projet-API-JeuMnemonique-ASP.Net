using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace APIJeuMnemonique.Models
{
    [DataContract]
    public class QuestionReponse
    {
        public QuestionReponse()
        {

        }
        public QuestionReponse(int id, string question, string reponse, ListeQuestions liste)
        {
            this.QuestionReponseId = id;
            this.Question = question;
            this.Reponse = reponse;
        }

        public QuestionReponse(string question, string reponse, ListeQuestions liste)
        {
            this.Question = question;
            this.Reponse = reponse;
        }

        [DataMember]
        public int QuestionReponseId { get; set; }
        [DataMember]
        public string Question { get; set; }
        [DataMember]
        public string Reponse { get; set; }

    }
}