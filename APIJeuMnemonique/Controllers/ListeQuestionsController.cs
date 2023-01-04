using APIJeuMnemonique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIJeuMnemonique.Controllers
{
    public class ListeQuestionsController : ApiController
    {
        // GET api/<controller>
        [Route("api/ListeQuestions/joueur")]
        public IEnumerable<ListeQuestions> GetParJoueur(string courriel)
        {
            return ListeQuestionsDataProvider.GetListesParJoueur(courriel);
        }

        [Route("api/ListeQuestions/nom/{nom}/joueur")]
        public ListeQuestions GetParJoueurEtNom(string courriel, string nom)
        {
            return ListeQuestionsDataProvider.GetByJoueurEtNom(courriel, nom);
        }

        [Route("api/ListeQuestions/id/{id}/joueur")]
        public ListeQuestions GetParJoueurEtId(string courriel, int id)
        {
            return ListeQuestionsDataProvider.GetByJoueurEtId(courriel, id);
        }


        // POST api/<controller>
        public bool Post([FromBody] ListeQuestions listeQuestions)
        {
            return ListeQuestionsDataProvider.AjouterListeQuestions(listeQuestions);
        }

        // PUT api/<controller>/5
        public bool Put([FromBody] ListeQuestions listeQuestions)
        {
            return ListeQuestionsDataProvider.ModifierListeQuestions(listeQuestions);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return ListeQuestionsDataProvider.SupprimerListeQuestions(id);

        }
    }
}