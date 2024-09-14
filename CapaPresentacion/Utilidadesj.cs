﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public class Utilidadesj
    {
        #region "PATRON SINGLETON"
        public static Utilidadesj _instancia = null;

        private Utilidadesj()
        {

        }

        public static Utilidadesj GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new Utilidadesj();
            }
            return _instancia;
        }
        #endregion

        public string UploadPhotoA(MemoryStream stream, string folder)
        {
            string rutaa = "";

            try
            {
                stream.Position = 0;

                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                var fullPath = $"{folder}{file}";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), file);

                // Guardar la imagen en el sistema de archivos
                File.WriteAllBytes(path, stream.ToArray());

                // Verificar si el archivo fue guardado correctamente
                if (File.Exists(path))
                {
                    rutaa = fullPath;
                }
            }
            catch (IOException)
            {
                // Registrar el error en un logger si es necesario
                // Logger.LogError(ioEx.Message);
                rutaa = "";  // Asegura que devuelva una cadena vacía en caso de error de E/S
            }
            catch (Exception)
            {
                // Registrar el error pero continuar el flujo
                // Puedes usar un logger si es necesario
                // Logger.LogError(ex.Message);
                rutaa = "";  // Asegura que devuelva una cadena vacía en caso de error
            }

            return rutaa;
        }

        public bool EnviarDetalleVenta(int IdVenta)
        {
            try
            {

                EVenta oVenta = NVenta.GetInstance().ObtenerDetalleVenta(IdVenta);
                if (oVenta == null)
                {
                    return false;
                }
                var from = "joseluisdelta1@gmail.com";
                var name = "GREEN FOREST";
                var smtps = "smtp.gmail.com";
                var port = 587;
                var password = "kgvkzaagdkvfutiy";
                var correo = new MailMessage
                {
                    From = new MailAddress(from, name)
                };
                correo.To.Add(oVenta.Cliente.Correo);
                correo.Subject = "Detalle Compra";

                string cuerpo = $@"
                <html>
                <head>
                    <style>
                        .contenedor {{
                            width: 100%;
                            max-width: 900px;
                            margin: 0 auto;
                            padding: 10px;
                            margin-bottom: 10px;
                            box-sizing: border-box;
                        }}

                        body {{
                            font-family: Arial, Helvetica, sans-serif;
                        }}

                        p.title {{
                            font-weight: bold;
                        }}

                        p.title2 {{
                            font-weight: bold;
                            color: #03A99F;
                            font-size: 20px;
                        }}

                        p.text {{
                            font-size: 15px;
                            font-weight: 100;
                            color: #858585;
                        }}

                        p {{
                            margin: 0px;
                        }}

                        table {{
                            width: 100%;
                            border-collapse: separate;
                            border-spacing: 4px;
                        }}

                        table.tbproductos thead tr th {{
                            background-color: #03A99F;
                            padding: 10px;
                            font-size: 15px;
                            color: white;
                        }}

                        table.tbproductos tbody tr td {{
                            padding: 10px;
                        }}

                        .td-item {{
                            border-bottom: 2px solid #E8E8E8 !important;
                        }}

                        .item {{
                            font-size: 15px;
                            font-weight: 100;
                            color: #757575;
                        }}

                        .item-2 {{
                            font-size: 15px;
                            font-weight: bold;
                            color: #757575;
                        }}

                        .item-3 {{
                            font-size: 15px;
                            font-weight: bold;
                            background-color: #03A99F;
                            color: white;
                            text-align: center;
                        }}
                    </style>
                </head>
                <body>
                    <div class='contenedor'>
                        <table style='width: 100%'>
                            <tr>
                                <td>
                                    <img src='https://asociacion-001-site1.ktempurl.com/Imagenes/sinimagen.png' style='width: 120px; height: 120px' />
                                </td>
                                <td style='text-align: right'>
                                    <table style='margin-right: 0; margin-left: auto'>
                                        <tr>
                                            <td><p class='title2'>NÚMERO VENTA</p></td>
                                        </tr>
                                        <tr>
                                            <td><span>{oVenta.Codigo}</span></td>
                                        </tr>
                                        <tr>
                                            <td><span>{oVenta.FechaRegistro}</span></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style='width: 100%'>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td><p class='title'>GREEN FOREST S.A.</p></td>
                                        </tr>
                                        <tr>
                                            <td><p class='text'>Dirección: Avenida Ejército Nacional</p></td>
                                        </tr>
                                        <tr>
                                            <td><p class='text'>Correo: info@greenforest.com.bo</p></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style='text-align: right'>
                                    <table style='margin-right: 0; margin-left: auto'>
                                        <tr>
                                            <td><p class='title'>CLIENTE</p></td>
                                        </tr>
                                        <tr>
                                            <td><p class='text'>{oVenta.Cliente.RazonSocial}</p></td>
                                        </tr>
                                        <tr>
                                            <td><p class='text'>{oVenta.Cliente.Ruc}</p></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class='tbproductos' style='width: 100%'>
                            <thead>
                                <tr>
                                    <th class='tbth'>Producto</th>
                                    <th class='tbth' style='width: 130px'>Cantidad</th>
                                    <th class='tbth' style='width: 130px'>Precio</th>
                                    <th class='tbth' style='width: 130px'>Total</th>
                                </tr>
                            </thead>
                            <tbody>";

                                foreach (var detalle in oVenta.ListaDetalleVenta)
                                {
                                    cuerpo += $@"
                                <tr>
                                    <td class='td-item'><p class='item'>{detalle.NombreProducto}</p></td>
                                    <td class='td-item'><p class='item'>{detalle.Cantidad}</p></td>
                                    <td class='td-item'><p class='item'>{detalle.PrecioUnidad:0.00}</p></td>
                                    <td class='td-item' style='background-color: #EDF6F9'><p class='item'>{detalle.ImporteTotal:0.00}</p></td>
                                </tr>";
                                }

                                cuerpo += $@"
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan='2'></td>
                                    <td class='td-item'><p class='item-2'>Cantidad Total</p></td>
                                    <td class='item-3'><p>{oVenta.ListaDetalleVenta.Sum(x => x.Cantidad)} Items</p></td>
                                </tr>
                                <tr>
                                    <td colspan='2'></td>
                                    <td class='td-item'><p class='item-2'>Total</p></td>
                                    <td class='item-3'><p>{oVenta.TotalCosto:0.00} /Bs.</p></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </body>
                </html>";


                correo.Body = cuerpo;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient
                {
                    Host = smtps,
                    Port = port,
                    Credentials = new NetworkCredential(from, password),
                    EnableSsl = true
                };

                
                smtp.Send(correo);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}