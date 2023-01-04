using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace APIJeuMnemonique.Models
{
    [DataContract]
    public class Groupe
    {
        public Groupe() {
            Membres = new List<Joueur>();
        }
        public Groupe(int groupeId, string nomGroupe, Joueur proprietaire, List<Joueur> membres)
        {
            GroupeId = groupeId;
            NomGroupe = nomGroupe;
            Proprietaire = proprietaire;
            Membres = membres;
        }

        public Groupe(int groupeId, string nomGroupe, Joueur proprietaire)
        {
            GroupeId = groupeId;
            NomGroupe = nomGroupe;
            Proprietaire = proprietaire;
            Membres = new List<Joueur>() { proprietaire };
        }

        [DataMember]
        public int GroupeId { get; set; }
        [DataMember]
        public string NomGroupe { get; set; }
        [DataMember]
        public Joueur Proprietaire { get; set; }
        [DataMember]
        public List<Joueur> Membres { get; set; }

        public bool Add(Joueur joueur)
        {
            this.Membres.Add(joueur);
            return true;
        }


    }
}