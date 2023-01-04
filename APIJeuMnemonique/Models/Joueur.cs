using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace APIJeuMnemonique.Models
{
    [DataContract]
    public class Joueur
    {
        public Joueur() { 
        
        }

        public Joueur(string identifiant, string courriel, string motDePasse)
        {
            this.Identifiant = identifiant;
            this.Courriel = courriel;
            this.MotDePasse = motDePasse;
        }

        public Joueur(string identifiant, string courriel) {
            this.Identifiant = identifiant;
            this.Courriel = courriel;
        }

        [DataMember]
        public string Identifiant { get; set; }
        [DataMember]
        public string Courriel { get; set; }
        [DataMember]
        public string MotDePasse { get; set; }
    }
}