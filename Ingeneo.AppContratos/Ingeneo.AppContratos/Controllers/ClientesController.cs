using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ingeneo.AppContratos.Models;

namespace Ingeneo.AppContratos.Controllers
{
    public class ClientesController : Controller
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();

        public class CustomAuth : AuthorizeAttribute
        {
            public string RedirectUrl = "/Account/Login";
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult(RedirectUrl);
                    return;
                }
                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    //Descomentar esta linea para usar JS alert enves de redireccionar a otro pagina
                    //filterContext.Result = new ContentResult { ContentType = "text/html", Content = "<script>alert('MAMBRU SE FUE A LA GUERRA!!! :@');window.location.href ='/Home/Index';</script> " };

                    //filterContext.Controller.TempData.Add("RedirectReason", "Acceso Denegado!!! Prenda un porro!");
                    filterContext.Result = new RedirectResult("/Home/Denied");
                    return;
                }
            }
        }
        // GET: Clientes
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Index()
        {
            return View(db.Cliente.OrderBy(x => x.NombreCliente).ToList());
        }

        // GET: Clientes/Details/5
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El detalle del cliente no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'El directorio especificado no existe en el servidor web..'," +
                               "text: 'Se ha quitado el recurso que está buscando, se le ha cambiado el nombre o no está disponible en estos momentos.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        [CustomAuth(Roles = "Monitor, Admin, Analista")]
        public ActionResult Create()
        {
            List<string> ListItems = new List<string>();
            ListItems.Add("Seleccione");
            ListItems.Add("Bolivia");
            ListItems.Add("Chad");
            ListItems.Add("Colombia");            
            ListItems.Add("Congo");
            ListItems.Add("Costa Rica");
            ListItems.Add("El Salvador");
            ListItems.Add("Estados Unidos");
            ListItems.Add("Ghana");
            ListItems.Add("Guatemala");
            ListItems.Add("Honduras");            
            ListItems.Add("Nicaragua");
            ListItems.Add("Paraguay");
            ListItems.Add("Rwanda");
            ListItems.Add("República Democrática del Congo");
            ListItems.Add("Senegal");
            ListItems.Add("Tanzania");          

            SelectList pais = new SelectList(ListItems);

            ViewBag.Pais = pais;
            //ViewData["Countries"] = Pais;
            
            return View();
        }

        public JsonResult States(string Country)
        {
            List<string> StatesList = new List<string>();
            switch (Country)
            {

                case "Colombia":
                    StatesList.Add("Medellin");
                    StatesList.Add("Cali");
                    StatesList.Add("Pereira");
                    StatesList.Add("Bogota");
                    StatesList.Add("Barranquilla");
                    StatesList.Add("Cartagena de Indias");
                    StatesList.Add("Cúcuta");
                    StatesList.Add("Soledad");
                    StatesList.Add("Ibagué");
                    StatesList.Add("Bucaramanga");
                    StatesList.Add("Soacha");
                    StatesList.Add("Santa Marta");
                    StatesList.Add("Villavicencio");
                    StatesList.Add("Bello");
                    StatesList.Add("Valledupar");
                    StatesList.Add("Manizales");
                    StatesList.Add("Buenaventura");
                    StatesList.Add("Pasto");
                    StatesList.Add("Neiva");
                    StatesList.Add("Bucaramanga");
                    break;

                case "Estados Unidos":
                    StatesList.Add("Nueva York");
                    StatesList.Add("Los Angeles");
                    StatesList.Add("Connecticut");
                    StatesList.Add("California");
                    StatesList.Add("Boston");
                    StatesList.Add("Annapolis");
                    StatesList.Add("Chicago");
                    StatesList.Add("Pennsylvania");
                    StatesList.Add("Washington, D.C.");
                    StatesList.Add("Virginia");
                    StatesList.Add("Carolina del Sur");
                    StatesList.Add("Florida");
                    StatesList.Add("Georgia");
                    StatesList.Add("Louisiana");
                    StatesList.Add("Minnesota");
                    StatesList.Add("Missouri");
                    StatesList.Add("Texas");
                    StatesList.Add("New Mexico");
                    StatesList.Add("Utah");
                    StatesList.Add("Arizona");
                    StatesList.Add("Nevada");
                    StatesList.Add("California");
                    break;

                case "Bolivia":
                    StatesList.Add("Achacachi");
                    StatesList.Add("Camiri");
                    StatesList.Add("Challapata");
                    StatesList.Add("Cliza");
                    StatesList.Add("Cobija");
                    StatesList.Add("Cochabamba");
                    StatesList.Add("Cotoca");
                    StatesList.Add("Guayaramerín");
                    StatesList.Add("Huanuni");
                    StatesList.Add("La Paz");
                    StatesList.Add("Llallagua");
                    StatesList.Add("Monteagudo");
                    StatesList.Add("Montero");
                    StatesList.Add("Oruro");
                    StatesList.Add("Patacamaya");
                    StatesList.Add("Potosí");
                    StatesList.Add("Punata");
                    StatesList.Add("Riberalta");
                    StatesList.Add("San Borja");
                    StatesList.Add("San Ignacio de Velasco");
                    StatesList.Add("Santa Cruz de la Sierra");
                    StatesList.Add("Sucre");
                    StatesList.Add("Tarija");
                    StatesList.Add("Trinidad");
                    StatesList.Add("Tupiza");
                    StatesList.Add("Villa Yapacaní");
                    StatesList.Add("Villamontes");
                    StatesList.Add("Villazón");
                    StatesList.Add("Warnes");
                    StatesList.Add("Yacuiba");

                    break;

                case "Costa Rica":
                    StatesList.Add("Alajuela");
                    StatesList.Add("Cartago");
                    StatesList.Add("Cañas");
                    StatesList.Add("Chacarita");
                    StatesList.Add("Curridabat");
                    StatesList.Add("Esparza");
                    StatesList.Add("Guápiles");
                    StatesList.Add("Heredia");
                    StatesList.Add("Liberia");
                    StatesList.Add("Limón");
                    StatesList.Add("Mercedes");
                    StatesList.Add("Nicoya");
                    StatesList.Add("Paraíso");
                    StatesList.Add("Puntarenas");
                    StatesList.Add("Purral");
                    StatesList.Add("Quesada");
                    StatesList.Add("San Diego");
                    StatesList.Add("San Francisco");
                    StatesList.Add("San Isidro");
                    StatesList.Add("San José");
                    StatesList.Add("San Miguel");
                    StatesList.Add("San Pablo");
                    StatesList.Add("San Pedro");
                    StatesList.Add("San Rafael");
                    StatesList.Add("San Vicente");
                    StatesList.Add("San Vicente de Moravia");
                    StatesList.Add("Santa Cruz");
                    StatesList.Add("Siquirres");
                    StatesList.Add("Turrialba");

                    break;

                case "Guatemala":
                    StatesList.Add("Antigua Guatemala");
                    StatesList.Add("Barberena");
                    StatesList.Add("Chichicastenango");
                    StatesList.Add("Chimaltenango");
                    StatesList.Add("Chiquimula");
                    StatesList.Add("Ciudad Guatemala");
                    StatesList.Add("Coatepeque");
                    StatesList.Add("Cobán");
                    StatesList.Add("Escuintla");
                    StatesList.Add("Huehuetenango");
                    StatesList.Add("Jacaltenango");
                    StatesList.Add("Jalapa");
                    StatesList.Add("Jutiapa");
                    StatesList.Add("Mazatenango");
                    StatesList.Add("Mixco");
                    StatesList.Add("Petapa");
                    StatesList.Add("Puerto Barrios");
                    StatesList.Add("Quetzaltenango");
                    StatesList.Add("Retalhuleu");
                    StatesList.Add("San Benito");
                    StatesList.Add("San Francisco El Alto");
                    StatesList.Add("San Juan Sacatepéquez");
                    StatesList.Add("San Miguel Chicaj");
                    StatesList.Add("San Pedro Sacatepéquez");
                    StatesList.Add("Sanarate");
                    StatesList.Add("Santa Lucía Cotzumalguapa");
                    StatesList.Add("Sololá");
                    StatesList.Add("Totonicapán");
                    StatesList.Add("Villa Nueva");
                    StatesList.Add("Zacapa");

                    break;

                case "El Salvador":
                    StatesList.Add("Acajutla");
                    StatesList.Add("Ahuachapán");
                    StatesList.Add("Antiguo Cuscatlán");
                    StatesList.Add("Apopa");
                    StatesList.Add("Chalatenango");
                    StatesList.Add("Chalchuapa");
                    StatesList.Add("Cojutepeque");
                    StatesList.Add("Cuscatancingo");
                    StatesList.Add("Delgado");
                    StatesList.Add("Izalco");
                    StatesList.Add("La Unión");
                    StatesList.Add("Mejicanos");
                    StatesList.Add("Metapán");
                    StatesList.Add("Puerto El Triunfo");
                    StatesList.Add("Quezaltepeque");
                    StatesList.Add("San Francisco");
                    StatesList.Add("San Marcos");
                    StatesList.Add("San Martín");
                    StatesList.Add("San Miquel");
                    StatesList.Add("San Rafael Oriente");
                    StatesList.Add("San Salvador");
                    StatesList.Add("San Vicente");
                    StatesList.Add("Santa Ana");
                    StatesList.Add("Santa Rosa de Lima");
                    StatesList.Add("Santa Tecla");
                    StatesList.Add("Sensuntepeque");
                    StatesList.Add("Sonsonate");
                    StatesList.Add("Soyapango");
                    StatesList.Add("Usulután");
                    StatesList.Add("Zacatecoluca");

                    break;

                case "Nicaragua":
                    StatesList.Add("Bluefields");
                    StatesList.Add("Boaco");
                    StatesList.Add("Camoapa");
                    StatesList.Add("Chichigalpa");
                    StatesList.Add("Chinandega");
                    StatesList.Add("Ciudad Sandino");
                    StatesList.Add("Diriamba");
                    StatesList.Add("El Viejo");
                    StatesList.Add("Estelí");
                    StatesList.Add("Granada");
                    StatesList.Add("Jalapa");
                    StatesList.Add("Jinotega");
                    StatesList.Add("Jinotepe");
                    StatesList.Add("Juigalpa");
                    StatesList.Add("León");
                    StatesList.Add("Managua");
                    StatesList.Add("Masatepe");
                    StatesList.Add("Masaya");
                    StatesList.Add("Matagalpa");
                    StatesList.Add("Nagarote");
                    StatesList.Add("Nandaime");
                    StatesList.Add("Nueva Guinea");
                    StatesList.Add("Ocotal");
                    StatesList.Add("Puerto Cabezas");
                    StatesList.Add("Rivas");
                    StatesList.Add("Río Blanco");
                    StatesList.Add("San Carlos");
                    StatesList.Add("Siuna");
                    StatesList.Add("Somoto");
                    StatesList.Add("Tipitapa");

                    break;

                case "Chad":
                    StatesList.Add("Abéché");
                    StatesList.Add("Adré");
                    StatesList.Add("Am Timan");
                    StatesList.Add("Ati");
                    StatesList.Add("Biltine");
                    StatesList.Add("Bitkine");
                    StatesList.Add("Bokoro");
                    StatesList.Add("Bongor");
                    StatesList.Add("Bébédjia");
                    StatesList.Add("Bénoy");
                    StatesList.Add("Béré");
                    StatesList.Add("Doba");
                    StatesList.Add("Dourbali");
                    StatesList.Add("Faya");
                    StatesList.Add("Guélengdeng");
                    StatesList.Add("Koumra");
                    StatesList.Add("Kyabé");
                    StatesList.Add("Kélo");
                    StatesList.Add("Laï");
                    StatesList.Add("Mao");
                    StatesList.Add("Massaguet");
                    StatesList.Add("Massakory");
                    StatesList.Add("Mboursou Léré");
                    StatesList.Add("Mongo");
                    StatesList.Add("Moundou");
                    StatesList.Add("Moussoro");
                    StatesList.Add("N'Djamena");
                    StatesList.Add("Oum Hadjer");
                    StatesList.Add("Pala");
                    StatesList.Add("Sarh");

                    break;

                case "Tanzania":
                    StatesList.Add("Arusha");
                    StatesList.Add("Babati");
                    StatesList.Add("Bagamoyo");
                    StatesList.Add("Bukoba");
                    StatesList.Add("Buseresere");
                    StatesList.Add("Chake Chake");
                    StatesList.Add("Dar es Salaam");
                    StatesList.Add("Dodoma");
                    StatesList.Add("Iringa");
                    StatesList.Add("Katumba");
                    StatesList.Add("Kigoma");
                    StatesList.Add("Kilosa");
                    StatesList.Add("Lindi");
                    StatesList.Add("Mbeya");
                    StatesList.Add("Merelani");
                    StatesList.Add("Morogoro");
                    StatesList.Add("Moshi");
                    StatesList.Add("Mtwara");
                    StatesList.Add("Musoma");
                    StatesList.Add("Mwanza");
                    StatesList.Add("Nguruka");
                    StatesList.Add("Shinyanga");
                    StatesList.Add("Singida");
                    StatesList.Add("Songea");
                    StatesList.Add("Sumbawanga");
                    StatesList.Add("Tabora");
                    StatesList.Add("Tanga");
                    StatesList.Add("Ushirombo");
                    StatesList.Add("Wete");
                    StatesList.Add("Zanzibar");

                    break;

                case "Ghana":
                    StatesList.Add("Accra");
                    StatesList.Add("Achiaman");
                    StatesList.Add("Axim");
                    StatesList.Add("Bawku");
                    StatesList.Add("Berekum");
                    StatesList.Add("Bolgatanga");
                    StatesList.Add("Cape Coast");
                    StatesList.Add("Dom");
                    StatesList.Add("Gbawe");
                    StatesList.Add("Ho");
                    StatesList.Add("Koforidua");
                    StatesList.Add("Kumasi");
                    StatesList.Add("Madina");
                    StatesList.Add("Navrongo");
                    StatesList.Add("Nkawkaw");
                    StatesList.Add("Nungua");
                    StatesList.Add("Obuasi");
                    StatesList.Add("Oduponkpehe");
                    StatesList.Add("Savelugu");
                    StatesList.Add("Sekondi");
                    StatesList.Add("Sunyani");
                    StatesList.Add("Swedru");
                    StatesList.Add("Tafo");
                    StatesList.Add("Takoradi");
                    StatesList.Add("Tamale");
                    StatesList.Add("Techiman");
                    StatesList.Add("Tema");
                    StatesList.Add("Teshi");
                    StatesList.Add("Wa");
                    StatesList.Add("Winneba");

                    break;

                case "Rwanda":
                    StatesList.Add("Butare");
                    StatesList.Add("Byumba");
                    StatesList.Add("Cyangugu");
                    StatesList.Add("Gisenyi");
                    StatesList.Add("Gitarama");
                    StatesList.Add("Kibungo");
                    StatesList.Add("Kibuye");
                    StatesList.Add("Kigali");
                    StatesList.Add("Nzega");
                    StatesList.Add("Ruhengeri");
                    StatesList.Add("Rwamagana");

                    break;

                case "Senegal":
                    StatesList.Add("Bignona");
                    StatesList.Add("Dakar");
                    StatesList.Add("Dara");
                    StatesList.Add("Diawara");
                    StatesList.Add("Grand Dakar");
                    StatesList.Add("Guinguinéo");
                    StatesList.Add("Joal-Fadiout");
                    StatesList.Add("Kayar");
                    StatesList.Add("Kolda");
                    StatesList.Add("Koungheul");
                    StatesList.Add("Kédougou");
                    StatesList.Add("Mbaké");
                    StatesList.Add("Mékhé");
                    StatesList.Add("Ndibène Dahra");
                    StatesList.Add("Ndioum");
                    StatesList.Add("Nguékokh");
                    StatesList.Add("Nioro du Rip");
                    StatesList.Add("Ouro Sogui");
                    StatesList.Add("Pourham");
                    StatesList.Add("Pout");
                    StatesList.Add("Richard Toll");
                    StatesList.Add("Saint-Louis");
                    StatesList.Add("Sokone");
                    StatesList.Add("Sédhiou");
                    StatesList.Add("Thiès Nones");
                    StatesList.Add("Tionk Essil");
                    StatesList.Add("Tiébo");
                    StatesList.Add("Vélingara");
                    StatesList.Add("Ziguinchor");

                    break;

                case "Congo":
                    StatesList.Add("Boukiéro");
                    StatesList.Add("Brazzaville");
                    StatesList.Add("Djambala");
                    StatesList.Add("Dolisie");
                    StatesList.Add("Ewo");
                    StatesList.Add("Gamboma");
                    StatesList.Add("Impfondo");
                    StatesList.Add("Kayes");
                    StatesList.Add("Kinkala");
                    StatesList.Add("Loandjili");
                    StatesList.Add("Madingou");
                    StatesList.Add("Makoua");
                    StatesList.Add("Mossendjo");
                    StatesList.Add("Ouésso");
                    StatesList.Add("Owando");
                    StatesList.Add("Pointe-Noire");
                    StatesList.Add("Sibiti");
                    StatesList.Add("Sémbé");

                    break;

                case "Honduras":
                    StatesList.Add("Choloma");
                    StatesList.Add("Ciudad Choluteca");
                    StatesList.Add("Comayagua");
                    StatesList.Add("Danlí");
                    StatesList.Add("El Paraíso");
                    StatesList.Add("El Progreso");
                    StatesList.Add("Gracias");
                    StatesList.Add("Intibucá");
                    StatesList.Add("Juticalpa");
                    StatesList.Add("La Ceiba");
                    StatesList.Add("La Entrada");
                    StatesList.Add("La Lima");
                    StatesList.Add("La Paz");
                    StatesList.Add("Marcala");
                    StatesList.Add("Nacaome");
                    StatesList.Add("Nueva Ocotepeque");
                    StatesList.Add("Olanchito");
                    StatesList.Add("Puerto Cortez");
                    StatesList.Add("Puerto Lempira");
                    StatesList.Add("Roatán");
                    StatesList.Add("San Lorenzo");
                    StatesList.Add("San Pedro Sula");
                    StatesList.Add("Santa Bárbara");
                    StatesList.Add("Santa Rosa de Copán");
                    StatesList.Add("Siguatepeque");
                    StatesList.Add("Talanga");
                    StatesList.Add("Tegucigalpa");
                    StatesList.Add("Tela");
                    StatesList.Add("Tocoa");
                    StatesList.Add("Villanueva");      

                    break;

                case "Paraguay":
                    StatesList.Add("Asunción");
                    StatesList.Add("Ayolas");
                    StatesList.Add("Benjamín Aceval");
                    StatesList.Add("Caacupé");
                    StatesList.Add("Caaguazú");
                    StatesList.Add("Capiatá");
                    StatesList.Add("Capitán Bado");
                    StatesList.Add("Ciudad del Este");
                    StatesList.Add("Colonia Mariano Roque Alonso");
                    StatesList.Add("Concepción");
                    StatesList.Add("Coronel Oviedo");
                    StatesList.Add("Encarnación");
                    StatesList.Add("Fernando de la Mora");
                    StatesList.Add("Filadelfia");
                    StatesList.Add("Horqueta");
                    StatesList.Add("Itauguá");
                    StatesList.Add("Lambaré");
                    StatesList.Add("Limpio");
                    StatesList.Add("Nemby");
                    StatesList.Add("Paraguarí");
                    StatesList.Add("Piribebuy");
                    StatesList.Add("Presidente Franco");
                    StatesList.Add("San Antonio");
                    StatesList.Add("San Isidro de Curuguaty");
                    StatesList.Add("San Juan Nepomuceno");
                    StatesList.Add("San Lorenzo");
                    StatesList.Add("San Pedro de Ycuamandiyú");
                    StatesList.Add("Santa Rita");
                    StatesList.Add("Villa Elisa");
                    StatesList.Add("Villarrica");

                    break;

                case "República Democrática del Congo":
                    StatesList.Add("Kinshasa");
                    StatesList.Add("Lubumbashi");
                    StatesList.Add("Mbuji-Mayi");
                    StatesList.Add("Kananga");
                    StatesList.Add("Kisangani");
                    StatesList.Add("Bukavu");
                    StatesList.Add("Tshikapa");                    

                    break;
            }
            return Json(StatesList);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nit,NombreCliente,Telefono,Direccion,Email,URL,Pais,Ciudad")] Cliente cliente)
            {
            if (ModelState.IsValid)
            {
                try
                {
                    var RepeatCust = db.Cliente.AsNoTracking().Where(x => x.Nit == cliente.Nit).FirstOrDefault();

                    if (RepeatCust == null)
                    {
                        db.Cliente.Add(cliente);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else {
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'El cliente que deseas registrar ya existe en la base de datos.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/Clientes/Index')});</script>");

                    }
                
                }
                catch (Exception) {
                    string hh = Url.Content("~/Content/sweetalert2.min.css");
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'por aca se revienta'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Aceptar!'}).then(function() " +
                                   "{swal(''," +
                                   "''," +
                                   "'success', window.location.href='/Clientes/Index')});</script>");

                }
            }
            List<string> ListItems = new List<string>();
            ListItems.Add("Seleccione");
            ListItems.Add("Colombia");
            ListItems.Add("Estados Unidos");
            ListItems.Add("Bolivia");
            ListItems.Add("Costa Rica");
            ListItems.Add("Guatemala");
            ListItems.Add("El Salvador");
            ListItems.Add("Nicaragua");
            ListItems.Add("Chad");
            ListItems.Add("Tanzania");
            ListItems.Add("Ghana");
            ListItems.Add("Rwanda");
            ListItems.Add("Senegal");
            ListItems.Add("Congo");

            SelectList pais = new SelectList(ListItems);

            ViewBag.Pais = pais;
            return View(cliente);
        }


        // GET: Clientes/Edit/5
        [CustomAuth(Roles = "Monitor, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El cliente que deseas editar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'El directorio especificado no existe en el servidor web..'," +
                               "text: 'Se ha quitado el recurso que está buscando, se le ha cambiado el nombre o no está disponible en estos momentos.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal('Hecho!'," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }

            List<string> ListItems = new List<string>();
            ListItems.Add("Seleccione");
            ListItems.Add("Colombia");            
            ListItems.Add("Estados Unidos");
            ListItems.Add("Bolivia");
            ListItems.Add("Costa Rica");
            ListItems.Add("Guatemala");
            ListItems.Add("El Salvador");
            ListItems.Add("Nicaragua");
            ListItems.Add("Chad");
            ListItems.Add("Tanzania");
            ListItems.Add("Ghana");
            ListItems.Add("Rwanda");
            ListItems.Add("Senegal");
            ListItems.Add("Congo");

            SelectList pais = new SelectList(ListItems);
            ViewBag.Pais = pais;
            
            return View(cliente);
        }
       
        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nit,NombreCliente,Telefono,Direccion,Email,URL,Pais,Ciudad")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try {
                    var RepeatCust = db.Cliente.AsNoTracking().Where(x => x.Nit == cliente.Nit).FirstOrDefault();

                    if (RepeatCust == null || RepeatCust.id == cliente.id) { 
                        db.Entry(cliente).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'El cliente que deseas registrar ya existe en la base de datos.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/Clientes/Index')});</script>");

                    }
                }
                catch (Exception){

                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'El cliente que deseas registrar ya existe en la base de datos.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/Clientes/Index')});</script>");

                    }
            }
            List<string> ListItems = new List<string>();
            ListItems.Add("Seleccione");
            ListItems.Add("Colombia");
            ListItems.Add("Estados Unidos");
            ListItems.Add("Bolivia");
            ListItems.Add("Costa Rica");
            ListItems.Add("Guatemala");
            ListItems.Add("El Salvador");
            ListItems.Add("Nicaragua");
            ListItems.Add("Chad");
            ListItems.Add("Tanzania");
            ListItems.Add("Ghana");
            ListItems.Add("Rwanda");
            ListItems.Add("Senegal");
            ListItems.Add("Congo");

            SelectList pais = new SelectList(ListItems);
            ViewBag.Pais = pais;
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El cliente que desea eliminar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'El directorio especificado no existe en el servidor web..'," +
                               "text: 'Se ha quitado el recurso que está buscando, se le ha cambiado el nombre o no está disponible en estos momentos.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'No puedes realizar esta acción..'," +
                               "text: 'El cliente que desea eliminar tiene uno o varios contratos con Ingeneo S.A.S , no puedes eliminar este registro sin antes eliminar sus dependencias.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
