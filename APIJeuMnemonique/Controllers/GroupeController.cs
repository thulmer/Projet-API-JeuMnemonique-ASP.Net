using APIJeuMnemonique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIJeuMnemonique.Controllers
{
    public class GroupeController : ApiController
    {
        //Gestion des membres du groupe-------------------------------------------------------

        // GET api/<controller>/membres
        [Route("api/Groupe/membres")]
        public IEnumerable<Joueur> GetMembres(int groupeId)
        {
            return GroupeDataProvider.GetMembresGroupe(groupeId);
        }

        // POST api/<controller>/membre
        [HttpPost]
        [Route("api/Groupe/membre")]
        public bool AjouterMembreAuGroupe(string courriel, int groupeId)
        {
            return GroupeDataProvider.AjouterMembreAuGroupe(courriel, groupeId);
        }

        // DELETE api/<controller>/membre
        [Route("api/Groupe/membre")]
        public bool Delete(string courriel, int groupeId)
        {
            return GroupeDataProvider.SupprimerMembreDuGroupe(courriel,groupeId);
        }


        //Gestion des listes partagées du groupe-------------------------------------------------------

        // GET api/<controller>/listes
        [Route("api/Groupe/listes")]
        public IEnumerable<ListeQuestions> GetListesPartagees(int groupeId)
        {
            return GroupeDataProvider.GetListesPartageesDuGroupe(groupeId);
        }

        // POST api/<controller>/liste
        [HttpPost]
        [Route("api/Groupe/liste")]
        public bool AjouterUneListePartagee(int listeId, int groupeId)
        {
            return GroupeDataProvider.PartagerUneListeAuGroupe(listeId, groupeId);
        }

        // DELETE api/<controller>/liste
        [Route("api/Groupe/liste")]
        public bool Delete(int listeId, int groupeId)
        {
            return GroupeDataProvider.SupprimerUneListePartagee(listeId, groupeId);
        }


        //Gestion du groupe-------------------------------------------------------
        
        // GET api/<controller>
        public IEnumerable<Groupe> GetGroupesParJoueur(string courriel)
        {
            return GroupeDataProvider.GetGroupesParJoueur(courriel);
        }

        // POST api/<controller>
        public bool Post([FromBody]Groupe groupe)
        {
            return GroupeDataProvider.AjouterGroupe(groupe);
        }

        // PUT api/<controller>/{groupe}
        public bool Put([FromBody] Groupe groupe)
        {
            return GroupeDataProvider.ModifierGroupe(groupe);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return GroupeDataProvider.SupprimerGroupe(id);
        }
    }
}