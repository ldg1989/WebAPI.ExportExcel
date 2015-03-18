﻿using WebAPIExportExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIExportExcel.Controllers
{
    public class ClienteController : ApiController
    {
        private static IList<Cliente> listCliente = new List<Cliente>();

        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {
            return listCliente;
        }

        [HttpGet]
        public Cliente GetCliente(int idCliente)
        {
            return listCliente.FirstOrDefault(x => x.IdCliente == idCliente);
        }

        [HttpPut]
        public HttpResponseMessage PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (cliente.IdCliente != id || !listCliente.Any(x => x.IdCliente == id))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var clienteOld = listCliente.FirstOrDefault(x => x.IdCliente == id);
            listCliente.Remove(clienteOld);

            listCliente.Add(cliente);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (!listCliente.Any(x => x.IdCliente == cliente.IdCliente))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var clienteOld = listCliente.FirstOrDefault(x => x.IdCliente == cliente.IdCliente);
            listCliente.Remove(clienteOld);

            listCliente.Add(cliente);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cliente);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cliente.IdCliente }));

            return response;
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCliente(int id)
        {
            var cliente = listCliente.Where(x => x.IdCliente == id).FirstOrDefault();

            if (cliente == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            listCliente.Remove(cliente);
            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }
    }
}