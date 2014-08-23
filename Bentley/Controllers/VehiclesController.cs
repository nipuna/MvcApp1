using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BusinessLogic;
using System.Web.Script.Serialization;
using System.Text;

namespace Bentley.Controllers
{
    public class VehiclesController : Controller
    {
        #region Private variables
        private CwiAPI.CwiAPIService apiService = new CwiAPI.CwiAPIService();
        JavaScriptSerializer sr = new JavaScriptSerializer(new SimpleTypeResolver());
        #endregion

        #region Index renders default view 
        /// <summary>
        /// Index default method
        /// </summary>
        /// <returns>View </returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                string reqdInfo = apiService.authenticateRequestWithBrand("bentleycwi", "bentleycwi!");
                reqdDetails det = (reqdDetails)(sr.Deserialize<reqdDetails>(reqdInfo));
                HttpCookie ckUserInfo = new HttpCookie("userInfo");
                ckUserInfo.Values["authStats"] = det.AuthenticationStatus.ToString();
                ckUserInfo.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(ckUserInfo);
                Session["loginStatus"] = det.AuthenticationStatus;
                if (det.AuthenticationStatus == true)
                {
                    HttpCookie ckBrandId = new HttpCookie("brandId");
                    ckBrandId.Value = det.brandId.ToString();
                    ckBrandId.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(ckBrandId);
                    string Vehicles = apiService.getAllVehicleDetailsWithBrand(Convert.ToInt32(det.brandId));
                    //List<detail> allVehicles = (List<detail>)(sr.Deserialize<List<detail>>(Vehicles)); 
                    detail[] allVehicles = (detail[])(sr.Deserialize<detail[]>(Vehicles));
                    List<drpDetails> vehiclesForDpDwn = new List<drpDetails>();
                    foreach (var _region in allVehicles)
                    {
                        vehiclesForDpDwn.Add(new drpDetails(_region.Id, _region.Name.ToString(), false));
                    }
                    var items = new SelectList(vehiclesForDpDwn, "ID", "Name", 0);
                    ViewData["models"] = items;
                    #region Years dropdown
                    List<drpDetails> yearsForDpDwn = new List<drpDetails>();
                    yearsForDpDwn.Add(new drpDetails(0, "Any", false));
                    var yearItems = new SelectList(yearsForDpDwn, "ID", "Name", 0);
                    ViewData["modelYears"] = yearItems;
                    #endregion
                    Session["vehicles"] = allVehicles;
                    return View();
                }
                else
                {
                    Session["error"] = "Unauthorised Access";
                    return RedirectToAction("Index", "Errors");
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region Index hanles post request for default View
        /// <summary>
        /// Default method fro rendering the UI
        /// </summary>
        /// <returns></returns>
        [HttpPost, SessionExpireFilter]
        public ActionResult Index(string Models, string ModelYears)
        {
            try
            {

                if (Convert.ToInt32(Models) == 0 || Convert.ToInt32(ModelYears) == 0)
                {
                    return RedirectToAction("index", "Vehicles");
                }
                else if (Convert.ToInt32(Models) != 0 && Convert.ToInt32(ModelYears) != 0)
                {
                    Session["Models"] = Convert.ToInt32(Models);
                    Session["ModelYears"] = ModelYears;
                    string Name = ((detail[])(Session["vehicles"])).Where(veh => veh.Id == Convert.ToInt32(Models)).First().Name;
                    Session["VehicleName"] = " " + Name + " - " + ModelYears + "MY";
                    Random random = new Random();
                    return RedirectToAction(@"index", "phones");
                    //return RedirectToAction(@"index?" + random.Next(), "phones");
                }
                return RedirectToAction("index", "Vehicles");

            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                return RedirectToAction("Index", "Errors");
                throw;
            }
        }
        #endregion

        #region get Model Years
        /// <summary>
        /// get Model Years
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public string getModelYears(Int32 modelId)
        {
            string reqdInfo = "";
            string vehicleYears = apiService.getModelYears(modelId);
            string[] years = (string[])(sr.Deserialize<string[]>(vehicleYears));
            StringBuilder Years = new StringBuilder();
            foreach (var year in years)
            {
                Years.Append(year + ",");
            }
            reqdInfo = Years.ToString();
            //Session["VehicleName"] = " ";
            return reqdInfo;
        }
        #endregion

    }
}
