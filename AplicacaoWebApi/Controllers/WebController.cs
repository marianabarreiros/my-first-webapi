using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AplicacaoDataAccess;

namespace AplicacaoWebApi.Controllers
{
    public class WebController : ApiController
    {
        public IEnumerable<Aplicacoes> Get()
        {
            using (AplicationDBEntities entities = new AplicationDBEntities())
            {
                return entities.Aplicacoes.ToList();
            }
        }

        public IEnumerable<Aplicacoes> Get(String id)
        {
            using (AplicationDBEntities entities = new AplicationDBEntities())
            {
                return entities.Aplicacoes.Where(e => e.Tipo == id).ToList();
                //var entity = entities.Aplicacoes.Where(e => e.Tipo == id).ToList();
                //if(entity != null)
                //{

                //    return Request.CreateResponse(HttpStatusCode.OK, entity);
                //}
                //else
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Aplicação com tipo " + id.ToString() + " não encontrada");
                //}
            }
        }

        public HttpResponseMessage Post([FromBody] Aplicacoes aplicacoes)
        {
            try
            {
                using (AplicationDBEntities entities = new AplicationDBEntities())
                {
                    entities.Aplicacoes.Add(aplicacoes);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, aplicacoes);
                    message.Headers.Location = new Uri(Request.RequestUri + aplicacoes.Tipo.ToString());

                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
