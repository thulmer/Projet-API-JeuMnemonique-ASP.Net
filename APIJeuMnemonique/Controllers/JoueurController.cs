using APIJeuMnemonique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIJeuMnemonique.Controllers
{
    public class JoueurController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Joueur> Get()
        {
            return JoueurDataProvider.GetJoueurs();
        }

        // GET api/<controller>/courriel?courriel=
        [Route("api/Joueur/courriel")]

        public Joueur GetParCourriel(string courriel)
        {
            return JoueurDataProvider.GetByCourriel(courriel);
        }

        // GET api/<controller>/identifiant
        [Route("api/Joueur/identifiant/{identifiant}")]

        public List<Joueur> GetParId(string identifiant)
        {
            return JoueurDataProvider.FindByIdentifiant(identifiant);
        }

        // GET api/<controller>/courriel/{courriel}/identifiant/{identifiant}
        [Route("api/Joueur/courriel/{courriel}/identifiant/{identifiant}")]
        public bool GetParIdentifiantEtCourriel(string courriel, string identifiant)
        {
            return JoueurDataProvider.GetByIdentifiantEtCourriel(courriel, identifiant);
        }


        // POST api/<controller>/connexion
        [HttpPost]
        [Route("api/Joueur/connexion")]
        public bool Connexion([FromBody] Joueur joueur)
        {
            return JoueurDataProvider.Connexion(joueur);
        }

        // POST api/<controller>
        public bool Post([FromBody] Joueur joueur)
        {
            return JoueurDataProvider.AjouterJoueur(joueur);
        }

        // PUT api/<controller>/5
        public bool Put([FromBody] Joueur joueur)
        {
            return JoueurDataProvider.ModifierJoueur(joueur);
        }

        // DELETE api/<controller>/5
        public bool Delete(string courriel)
        {
            return JoueurDataProvider.SupprimerJoueur(courriel);

        }
    }
}