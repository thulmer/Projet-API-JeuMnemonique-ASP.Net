using APIJeuMnemonique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIJeuMnemonique.Controllers
{
    public class QuestionReponseController : ApiController
    {
        // GET api/<controller>
        [Route("api/QuestionReponse/liste")]
        public IEnumerable<QuestionReponse> GetParListe(int listeId)
        {
            return QuestionReponseDataProvider.GetQuestionReponseParListe(listeId);
        }
        
        // POST api/<controller>
        public bool Post([FromBody] QuestionReponse questionReponse, int liste)
        {
            return QuestionReponseDataProvider.AjouterQuestionReponse(questionReponse, liste);
        }

        // PUT api/<controller>/5
        public bool Put([FromBody] QuestionReponse questionReponse)
        {
            return QuestionReponseDataProvider.ModifierQuestionReponse(questionReponse);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return QuestionReponseDataProvider.SupprimerQuestionReponse(id);
        }
    }
}