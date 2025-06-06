﻿using eBuy.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/UploadFiles")]
    public class UploadFilesController : ApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> UploadFile(HttpRequestMessage Request, string Data, string Proccess)
        {
            clsUpload upload = new clsUpload();
            upload.request = Request;
            upload.Data = Data;
            upload.Proccess = Proccess;
            return await upload.GrabarArchivo(false);
        }

        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Get(string ImageName)
        {
            clsUpload upload = new clsUpload();
            return upload.ConsultarArchivo(ImageName);
        }

        [HttpPut]
        [Authorize]
        public async Task<HttpResponseMessage> UpdateFile(HttpRequestMessage Request, string Data, string Proccess)
        {
            clsUpload upload = new clsUpload();
            upload.request = Request;
            upload.Data = Data;
            upload.Proccess = Proccess;
            return await upload.GrabarArchivo(true);
        }

        [HttpDelete]
        [Authorize]
        public HttpResponseMessage DeleteFile(string ImageName)
        {
            clsUpload upload = new clsUpload();
            return upload.EliminarFoto(ImageName);
        }

        [HttpGet]
        [Route("GetImagesByProductId")]
        [AllowAnonymous]
        public IHttpActionResult GetImagesByProduct(int IdProduct)
        {
            clsUpload upload = new clsUpload();
            var result = upload.GetImagesByProduct(IdProduct);
            return Ok(result);
        }

    }
}