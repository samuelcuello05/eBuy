using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace eBuy.Clases
{
    public class clsUpload
    {
        public HttpRequestMessage request { get; set; }

        public string Data { get; set; }

        public string Proccess { get; set; }

        public async Task<HttpResponseMessage> GrabarArchivo(bool Actualizar)
        {
            string RptaError = "";
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Files");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await request.Content.ReadAsMultipartAsync(provider);
                List<string> Archivos = new List<string>();
                foreach (MultipartFileData file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.FileName;
                    if (filename.StartsWith("\"") && filename.EndsWith("\""))
                    {
                        filename = filename.Trim('"');
                    }
                    if (filename.Contains(@"/") || filename.Contains(@"\"))
                    {
                        filename = Path.GetFileName(filename);
                    }
                    if (File.Exists(Path.Combine(root, filename)))
                    {
                        if (Actualizar)
                        {
                            //Se borra el original
                            File.Delete(Path.Combine(root, filename));
                            //Se crea el nuevo archivo con el mismo nombre
                            File.Move(file.LocalFileName, Path.Combine(root, filename));
                            //No se debe agregar en la base de datos, porque ya existe
                        }
                        else
                        {
                            //No se pueden tener archivos con el mismo nombre, es decir las imagenes tienen que tener nombres unicos
                            //Si el archivo existe, se borra el archivo temporal que se subio
                            File.Delete(file.LocalFileName);
                            //Se da una respuesta de error
                            RptaError += $"File: {filename} already exists";//"El archivo: " + filename + " ya existe";
                                                                             //return request.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo ya existe");
                        }
                    }
                    else
                    {
                        Archivos.Add(filename);

                        //Se renombra el archivo
                        File.Move(file.LocalFileName, Path.Combine(root, filename));
                    }

                }
                if (Archivos.Count > 0)
                {
                    //Envia a grabar la informacion de las imagenes
                    string Respuesta = ProcesarArchivos(Archivos);
                    //Se da una respuesta de exito
                    return request.CreateResponse(HttpStatusCode.OK, "File successfully updated");
                }
                else
                {
                    if (Actualizar)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, "File successfully updated");
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.Conflict, "File(s) already exist(s)");
                    }
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error uploading image " + ex.Message);
            }
        }


        public HttpResponseMessage ConsultarArchivo(string NombreFoto)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Files");
                string Archivo = Path.Combine(Ruta, NombreFoto);

                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open, FileAccess.Read);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = NombreFoto;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "File  not found");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error searching file " + ex.Message);
            }
        }

        private string ProcesarArchivos(List<string> Archivos)
        {
            switch (Proccess.ToUpper())
            {
                case "PRODUCT":
                    clsProductImage ProductImages = new clsProductImage();
                    ProductImages.IdProduct = Data; //Debe venir la información que se procesa en la base de datos, para nuestro caso, el código de la foto
                    ProductImages.Archives = Archivos;
                    return ProductImages.SaveImages();
                default:
                    return "Invalid proccess";
            }
        }


        public HttpResponseMessage EliminarFoto(string nombreImagen)
        {
            try
            {
                string ruta = HttpContext.Current.Server.MapPath("~/Files");
                string rutaArchivo = Path.Combine(ruta, nombreImagen);
                clsProductImage clsProductIma = new clsProductImage();
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                    string resp = clsProductIma.DeleteImage(nombreImagen);

                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent("Image deleted correctly. " + resp)
                    };
                    return response;
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("Image not found.")
                    };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error deleting image: " + ex.Message)
                };
            }

        }
    }
}