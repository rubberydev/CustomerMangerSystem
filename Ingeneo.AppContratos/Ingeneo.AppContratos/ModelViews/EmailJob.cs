using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Quartz;
using System.IO;
using System.Net;
using System.Net.Mail;
using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Configuration;
using Ingeneo.AppContratos.Controllers;
using System.Web.Hosting;
using System.Globalization;
using Microsoft.AspNet.Identity;

namespace Ingeneo.AppContratos.ModelViews
{
    public class EmailJob : IJob
    {
        private static GestorContratosDbContext db = new GestorContratosDbContext();
        private static ApplicationDbContext rs = new ApplicationDbContext();
        public static DateTime FirstN = DateTime.Today.AddDays(10);
        public static DateTime SecN = DateTime.Today.AddDays(5);
        public UserView user = new UserView();        

        public void Execute(IJobExecutionContext context)
        {
            var contratosN = db.Contrato.ToList();
            var prorrogas = db.Prorroga.ToList();
            var polizas = db.Poliza.ToList();

            string listaCorreos = "";
            var roles = rs.Roles.Include(m => m.Users).Where(m => m.Name == "Admin").First();
            var cantidad = roles.Users.Count;
            var counter = 1;
            foreach (var user in roles.Users)
            {
                var id = user.UserId;
                var userEmail = rs.Users.Find(id).Email;

                if (cantidad != counter)
                {
                    listaCorreos = listaCorreos + userEmail + ",";
                }
                else
                {
                    listaCorreos = listaCorreos + userEmail;
                }
                counter = counter + 1;
            }

            // Enviar Email 10 dias antes de que termine un contrato
            foreach (var contrato in contratosN)
            {
                if (contrato.FechaFinContrato.HasValue)
                {
                    var DateN = contrato.FechaFinContrato.Value;
                    if (FirstN.Year == DateN.Year && FirstN.Month == DateN.Month && FirstN.Day == DateN.Day)
                    {
                        var contratoNView = new Notification
                        {
                            CodigoContrato = contrato.CodigoContrato,
                            NombreCliente = contrato.Cliente.NombreCliente,
                            FechaInicio = contrato.FechaInicioContrato,
                            FechaFin = contrato.FechaFinContrato,
                        };
                        using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                        {
                            message.Subject = "Hola, Faltan 10 dias para que termine el siguiente Contrato: ";
                            message.Body = "Nombre del Cliente: " + contratoNView.NombreCliente + "\n" +
                                "Nombre del Contrato: " + contratoNView.CodigoContrato + "\n" +
                                "Fecha Inicio del Contrato: " + contratoNView.FechaInicio + "\n" +
                                "Fecha Fin del Contrato: " + contratoNView.FechaFin;
                            using (SmtpClient client = new SmtpClient
                            {
                                Host = "smtpout.secureserver.net",
                                Port = 25,
                                Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                            })
                            {
                                client.Send(message);
                            }
                        }
                    }
                }
            }
            //Enviar Email 10 dias antes de que termine una Prorroga            
            foreach (var prorroga in prorrogas)
            {
                if (prorroga.FechaFinProrroga.HasValue)
                {
                    var DateN = prorroga.FechaFinProrroga.Value;
                    if (FirstN.Year == DateN.Year && FirstN.Month == DateN.Month && FirstN.Day == DateN.Day)
                    {
                        var proView = new Notification
                        {
                            CodigoContrato = prorroga.Contrato.CodigoContrato,
                            FechaInicio = prorroga.FechaInicioProrroga,
                            FechaFin = prorroga.FechaFinProrroga
                        };
                        using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                        {
                            message.Subject = "Hola, Faltan 10 dias para que termine la siguiente Prorroga: ";
                            message.Body = "Nombre del Contrato al que pertenece la Prorroga: " + proView.CodigoContrato + "\n" +
                            "Fecha Inicio de la Prorroga: " + proView.FechaInicio + "\n" +
                            "Fecha Fin de la Prorroga: " + proView.FechaFin;                
                            using (SmtpClient client = new SmtpClient
                            {
                                Host = "smtpout.secureserver.net",
                                Port = 25,
                                Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                            })
                            {
                                client.Send(message);
                            }
                        }
                    }
                }
            }
            //Enviar Email 10 dias antes de que termine una Poliza 
            foreach (var poliza in polizas)
            {
                if (poliza.FechaFinpoliza != null)
                {
                    var DateN = poliza.FechaFinpoliza;
                    if (FirstN.Year == DateN.Year && FirstN.Month == DateN.Month && FirstN.Day == DateN.Day)
                    {
                        if (poliza.idContrato != null)
                        {
                            var polView = new Notification
                            {
                                NombreAseguradora = poliza.NombreAseguradora,
                                CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaFinpoliza,
                                FechaFin = poliza.FechaFinpoliza
                            };
                            using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                            {
                                message.Subject = "Hola, Faltan 10 dias para que termine la siguiente Poliza: ";
                                message.Body = "Nombre de la Aseguradora: " + polView.NombreAseguradora + "\n" +
                                    "Nombre del Contrato al que pertenece la Poliza: " + polView.CodigoContrato + "\n" +
                                    "Fecha Inicio de la Prorroga: " + polView.FechaInicio + "\n" +
                                    "Fecha Fin de la Prorroga: " + polView.FechaFin;
                                using (SmtpClient client = new SmtpClient
                                {
                                    Host = "smtpout.secureserver.net",
                                    Port = 25,
                                    Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                                })
                                {
                                    client.Send(message);
                                }
                            }
                        }
                        else
                        {
                            var polView = new Notification
                            {
                                NombreAseguradora = poliza.NombreAseguradora,
                                //CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaFinpoliza,
                                FechaFin = poliza.FechaFinpoliza
                            };
                            using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                            {
                                message.Subject = "Hola, Faltan 10 dias para que termine la siguiente Poliza: ";
                                message.Body = "Nombre de la Aseguradora: " + polView.NombreAseguradora + "\n" +
                                    "Nombre del Contrato al que pertenece la Poliza: Esta Poliza pertenece a una Prorroga, para información del contrato asociado ver detalle \n" +
                                    "Fecha Inicio de la Prorroga: " + polView.FechaInicio + "\n" +
                                    "Fecha Fin de la Prorroga: " + polView.FechaFin;
                                using (SmtpClient client = new SmtpClient
                                {
                                    Host = "smtpout.secureserver.net",
                                    Port = 25,
                                    Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                                })
                                {
                                    client.Send(message);
                                }
                            }
                        }

                        
                    }
                }
            }
            //Envio de Email contrato/Prorroga/Polizaa los 5 dias 
            // Enviar Email 5 dias antes de que termine un contrato
            foreach (var contrato in contratosN)
            {
                if (contrato.FechaFinContrato.HasValue)
                {
                    var DateN = contrato.FechaFinContrato.Value;
                    if (SecN.Year == DateN.Year && SecN.Month == DateN.Month && SecN.Day == DateN.Day)
                    {
                        var contratoNView = new Notification
                        {
                            CodigoContrato = contrato.CodigoContrato,
                            NombreCliente = contrato.Cliente.NombreCliente,
                            FechaInicio = contrato.FechaInicioContrato,
                            FechaFin = contrato.FechaFinContrato
                        };
                        using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                        {
                            message.Subject = "Hola, Faltan 5 dias para que termine el siguiente Contrato: ";
                            message.Body = "Nombre del Cliente: " + contratoNView.NombreCliente+ "\n" +
                                "Nombre del Contrato: " + contratoNView.CodigoContrato + "\n" +
                                "Fecha Inicio del Contrato: " + contratoNView.FechaInicio + "\n" +
                                "Fecha Fin del Contrato: " + contratoNView.FechaFin;
                            using (SmtpClient client = new SmtpClient
                            {                                
                                Host = "smtpout.secureserver.net",
                                Port = 25,
                                Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                            })
                            {
                                client.Send(message);
                            }
                        }
                    }
                }
            }
            //Enviar Email 5 dias antes de que termine una Prorroga            
            foreach (var prorroga in prorrogas)
            {
                if (prorroga.FechaFinProrroga.HasValue)
                {
                    var DateN = prorroga.FechaFinProrroga.Value;
                    if (SecN.Year == DateN.Year && SecN.Month == DateN.Month && SecN.Day == DateN.Day)
                    {
                        var proView = new Notification
                        {
                            CodigoContrato = prorroga.Contrato.CodigoContrato,
                            FechaInicio = prorroga.FechaInicioProrroga,
                            FechaFin = prorroga.FechaFinProrroga
                        };
                        using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                        {
                            message.Subject = "Hola, Faltan 5 dias para que termine la siguiente Prorroga: ";
                            message.Body = "Nombre del Contrato al que pertenece la Prorroga: " + proView.CodigoContrato + "\n" +
                            "Fecha Inicio de la Prorroga: " + proView.FechaInicio + "\n" +
                            "Fecha Fin de la Prorroga: " + proView.FechaFin;
                            using (SmtpClient client = new SmtpClient
                            {
                                Host = "smtpout.secureserver.net",
                                Port = 25,
                                Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                            })
                            {
                                client.Send(message);
                            }
                        }
                    }
                }
            }
            //Enviar Email 5 dias antes de que termine una Poliza 
            foreach (var poliza in polizas)
            {
                if (poliza.FechaFinpoliza != null)
                {
                    var DateN = poliza.FechaFinpoliza;
                    if (SecN.Year == DateN.Year && SecN.Month == DateN.Month && SecN.Day == DateN.Day)
                    {
                        if (poliza.idContrato != null)
                        {
                            var polView = new Notification
                            {
                                NombreAseguradora = poliza.NombreAseguradora,
                                CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaFinpoliza,
                                FechaFin = poliza.FechaFinpoliza
                            };
                            using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                            {
                                message.Subject = "Hola, Faltan 5 dias para que termine la siguiente Poliza: ";
                                message.Body = "Nombre de la Aseguradora: " + polView.NombreAseguradora + "\n" +
                                    "Nombre del Contrato al que pertenece la Poliza: " + polView.CodigoContrato + "\n" +
                                    "Fecha Inicio de la Prorroga: " + polView.FechaInicio + "\n" +
                                    "Fecha Fin de la Prorroga: " + polView.FechaFin;
                                using (SmtpClient client = new SmtpClient
                                {
                                    Host = "smtpout.secureserver.net",
                                    Port = 25,
                                    Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                                })
                                {
                                    client.Send(message);
                                }
                            }
                        }
                        else
                        {
                            var polView = new Notification
                            {
                                NombreAseguradora = poliza.NombreAseguradora,
                                //CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaFinpoliza,
                                FechaFin = poliza.FechaFinpoliza
                            };
                            using (var message = new MailMessage("gestion_contratos@ingeneo.com.co", listaCorreos))
                            {
                                message.Subject = "Hola, Faltan 5 dias para que termine la siguiente Poliza: ";
                                message.Body = "Nombre de la Aseguradora: " + polView.NombreAseguradora + "\n" +
                                    "Nombre del Contrato al que pertenece la Poliza: Esta Poliza pertenece a una Prorroga, para información del contrato asociado ver detalle \n" +
                                    "Fecha Inicio de la Prorroga: " + polView.FechaInicio + "\n" +
                                    "Fecha Fin de la Prorroga: " + polView.FechaFin;
                                using (SmtpClient client = new SmtpClient
                                {
                                    Host = "smtpout.secureserver.net",
                                    Port = 25,
                                    Credentials = new NetworkCredential("gestion_contratos@ingeneo.com.co", "GESTION.17")
                                })
                                {
                                    client.Send(message);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}