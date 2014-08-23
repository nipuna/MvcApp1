using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BusinessLogic;
using System.Web.Script.Serialization;
using System.Text;
using BusinessLogic;

namespace Bentley.Controllers
{
    public class PhonesController : Controller
    {
        #region Private variables
        private CwiAPI.CwiAPIService apiService = new CwiAPI.CwiAPIService();
        JavaScriptSerializer sr = new JavaScriptSerializer();
        #endregion

        #region Index default method
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [SessionExpireFilter]
        public ActionResult Index()
        {
            try
            {

                if (Session["loginStatus"] != null && Convert.ToBoolean(Session["loginStatus"]) == true)
                {

                    phoneDetails phonesSelected = getPhoneDetails();
                    if (phonesSelected != null)
                    {
                        List<drpDetails> brandsForDpDwn = new List<drpDetails>();
                        List<drpDetails> modelsForDpDwn = new List<drpDetails>();
                        brandsForDpDwn.Add(new drpDetails(0, "All", false));
                        foreach (var brand in phonesSelected.brands)
                        {
                            brandsForDpDwn.Add(new drpDetails(brand.Id, brand.Name.ToString(), false));
                        }
                        var items = new SelectList(brandsForDpDwn, "ID", "Name", 0);
                        ViewData["brands"] = items;
                        Session["allModels"] = phonesSelected.models;
                        #region Configuring top feature results
                        List<topFeatureResults> resltToRemove = phonesSelected.topFeatureResults.Where(rslt => rslt.TopFeatureName.ToLower() == "enhanced audio quality").ToList();
                        foreach (var rslt in resltToRemove)
                        {
                            phonesSelected.topFeatureResults.Remove(rslt);
                        }
                        ViewData["topFeatureResults"] = sr.Serialize(phonesSelected.topFeatureResults);
                        
                        #endregion
                        Session["topFeatureResults"] = phonesSelected.topFeatureResults;
                        foreach (var model in phonesSelected.models)
                        {
                            modelsForDpDwn.Add(new drpDetails(model.TestInstanceId, model.Name.ToString(), false));
                        }
                        var itemsModels = new SelectList(modelsForDpDwn, "ID", "Name", 0);
                        ViewData["models"] = itemsModels;
                        setVehicleFeautes();
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Vehicles");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Errors");
                throw;
            }
        }

        #endregion

        #region Renders the More Info view
        /// <summary>
        /// Renders the More Info view
        /// </summary>
        /// <param name="testInstanceId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public ViewResult moreinfo(Int32 testInstanceId)
        {
            try
            {

                //phoneModels[] allModels = (getPhoneDetails()).models;
                phoneModels[] allModels = (phoneModels[])(Session["allModels"]);

                if (allModels != null)
                {
                    phoneModels filteredModels;
                    filteredModels = allModels.Where(model => model.TestInstanceId == testInstanceId).First();

                    string topFeatureResults = apiService.getTopFeatureResults(testInstanceId);
                    string topFeatureResultsHtml = getTopFeatureResultsHtml(topFeatureResults);

                    string tmainFeaturesResultsHtml = getMainFeatureResults(testInstanceId);
                    
                    ViewData["testInstanceId"] = testInstanceId;
                    ViewData["phonename"] = filteredModels.BrandName + " " + filteredModels.Name;
                    ViewData["phonephoto"] = filteredModels.Photo;
                    var items = getPhoneSoftwareVersionsLst(filteredModels);
                    ViewData["PhoneSoftwareVersions"] = items;
                    ViewData["topFeatureResults"] = topFeatureResultsHtml;
                    ViewData["mainfeatures"] = tmainFeaturesResultsHtml;
                    setVehicleFeautes();
                    return View();
                }
                else
                {
                    Response.Redirect(@"\Errors\Index");
                    return View("Index", "Errors");
                }

            }
            catch (Exception ex)
            {
                Response.Redirect(@"\Errors\Index");
                return View("Index", "Errors"); 
                //throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Renders the Additional features view
        /// <summary>
        /// Renders the Additional features view
        /// </summary>
        /// <param name="testInstanceId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public ViewResult additionalFeautres(Int32 testInstanceId)
        {
            try
            {

                //phoneModels[] allModels = (getPhoneDetails()).models;
                phoneModels[] allModels = (phoneModels[])(Session["allModels"]);

                if (allModels != null)
                {
                    phoneModels filteredModels;
                    filteredModels = allModels.Where(model => model.TestInstanceId == testInstanceId).First();

                    string topFeatureResults = apiService.getTopFeatureResults(testInstanceId);
                    string topFeatureResultsHtml = getTopFeatureResultsHtml(topFeatureResults);
                    string tAddFeaturesResultsHtml = getAddFeatureResults(testInstanceId);
                    ViewData["testInstanceId"] = testInstanceId;
                    ViewData["phonename"] = filteredModels.BrandName + " " + filteredModels.Name;
                    ViewData["phonephoto"] = filteredModels.Photo;
                    var items = getPhoneSoftwareVersionsLst(filteredModels);
                    ViewData["PhoneSoftwareVersions"] = items;
                    ViewData["topFeatureResults"] = topFeatureResultsHtml;
                    ViewData["addfeatures"] = tAddFeaturesResultsHtml;
                    setVehicleFeautes();
                    return View();
                }
                else
                {
                    Response.Redirect(@"\Errors\Index");
                    return View("Index", "Errors");
                }

            }
            catch (Exception ex)
            {
                Response.Redirect(@"\Errors\Index");
                return View("Index", "Errors");
                //throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Renders the Quick Start Guide view
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInstanceId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public ViewResult quickStartGuide(Int32 testInstanceId)
        {
            try
            {

                //phoneModels[] allModels = (getPhoneDetails()).models;
                phoneModels[] allModels = (phoneModels[])(Session["allModels"]);

                if (allModels != null)
                {
                    phoneModels filteredModels;
                    filteredModels = allModels.Where(model => model.TestInstanceId == testInstanceId).First();

                    ViewData["testInstanceId"] = testInstanceId;
                    ViewData["phonename"] = filteredModels.BrandName + " " + filteredModels.Name;
                    ViewData["phonephoto"] = filteredModels.Photo;
                    var items = getPhoneSoftwareVersionsLst(filteredModels);
                    ViewData["PhoneSoftwareVersions"] = items;
                    ViewData["Conclusion"] = Session["Conclusion"];
                    string quickGuides = apiService.getQuickGuides(filteredModels.RelatedSystemID , filteredModels.PhoneID );
                    string quickGuidesHtml = getQuickGuidesHtml(quickGuides);
                    ViewData["quickGuidesResults"] = quickGuidesHtml;
                    setVehicleFeautes();
                    return View();
                }
                else
                {
                    Response.Redirect(@"\Errors\Index");
                    return View("Index", "Errors");
                }

            }
            catch (Exception ex)
            {
                Response.Redirect(@"\Errors\Index");
                return View("Index", "Errors");
                //throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Renders the Phone Software view
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testInstanceId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public ViewResult phoneSoftware(Int32 testInstanceId)
        {

            try
            {

                //phoneModels[] allModels = (getPhoneDetails()).models;
                phoneModels[] allModels = (phoneModels[])(Session["allModels"]);

                if (allModels != null)
                {
                    phoneModels filteredModels;
                    filteredModels = allModels.Where(model => model.TestInstanceId == testInstanceId).First();

                    ViewData["testInstanceId"] = testInstanceId;
                    ViewData["phonename"] = filteredModels.BrandName + " " + filteredModels.Name;
                    ViewData["phonephoto"] = filteredModels.Photo;
                    var items = getPhoneSoftwareVersionsLst(filteredModels);
                    ViewData["PhoneSoftwareVersions"] = items;
                    ViewData["Conclusion"] = Session["Conclusion"];
                    #region generating HTML
                    string quickGuidesHtml = "";
                    string[] groupingCss = { "smallText", "vehicleText" };
                    string[] imageStyles = { "style=\"border-width: 0px; height: 23px; width: 23px;\"", "style=\"border-width: 0px;\"" };
                    string vehicleImage = "";
                    ViewData["VehicleName"] = Session["VehicleName"].ToString();
                    string vehicleName = (Session["VehicleName"].ToString().Substring(0, Session["VehicleName"].ToString().IndexOf('-') - 1)).Trim();
                    ViewData["VehiclePhoto"] = vehicleImage = @"/images/Vehicles/icons/" + vehicleName + ".gif";

                    string[] imagesPath = { ViewData["phonephoto"].ToString(), vehicleImage };

                    string quickGuides = apiService.getQuickGuides(filteredModels.RelatedSystemID, filteredModels.PhoneID);
                    quickGuidesHtml = quickGuidesHtml + "<table style=\"width: 424px;\">";
                    quickGuidesHtml = quickGuidesHtml + "<tbody>";
                    quickGuidesHtml = quickGuidesHtml + "<tr>";
                    quickGuidesHtml = quickGuidesHtml + "<td valign=\"bottom\" style=\"padding-bottom: 4px;font-family:Arial,Helvetica,sans-serif;font-size:14px;font-weight:bold;\" colspan=\"2\">";

                    quickGuidesHtml = quickGuidesHtml + "<span class=\"title\" style=\"font-family:Arial,Helvetica,sans-serif;font-size:14px;font-weight:bold;\" id=\"lblSection\">";
                    quickGuidesHtml = quickGuidesHtml + "Software Version";
                    quickGuidesHtml = quickGuidesHtml + "</span>";
                    quickGuidesHtml = quickGuidesHtml + "</td>";
                    List<QuickGuides> objQuickGuides = (List<QuickGuides>)(sr.Deserialize<List<QuickGuides>>(quickGuides));
                    List<QuickGuides> resultsForSection = objQuickGuides.Where(quickGuide => quickGuide.Section.ToLower() == "software version").ToList();
                    List<string> grouptypes = resultsForSection.Select(quickGuide => quickGuide.GuideType ).Distinct().ToList();
                    foreach (var groupType in grouptypes)
                    {
                        int grouping = groupType == "phone" ? 1 : 2;
                        List<QuickGuides> groupResult = resultsForSection.Where(quickGuide => quickGuide.GuideType == groupType).ToList();
                        int groupResultCount = 1;
                        foreach (var mainResult in groupResult)
                        {
                            quickGuidesHtml = quickGuidesHtml + "<table width=\"424\">";
                            quickGuidesHtml = quickGuidesHtml + "<tbody>";
                            quickGuidesHtml = quickGuidesHtml + "<tr>";
                            quickGuidesHtml = quickGuidesHtml + "<td valign=\"middle\" height=\"24\" align=\"center\" width=\"50\">";
                            quickGuidesHtml = quickGuidesHtml + "<span class=\"" + groupingCss[grouping - 1] + "\" />";
                            if (groupResultCount == 1)
                            {
                                quickGuidesHtml = quickGuidesHtml + "<img " + imageStyles[grouping - 1] + " src=\"" + imagesPath[grouping - 1] + "\" />";
                            }
                            quickGuidesHtml = quickGuidesHtml + "</td>";
                            quickGuidesHtml = quickGuidesHtml + "<td valign=\"middle\" width=\"380\" style=\"margin-right:5px;\" >";
                            quickGuidesHtml = quickGuidesHtml + "<span class=\"" + groupingCss[grouping - 1] + "\" style=\"font-size:11px;color:#444444\" >";
                            quickGuidesHtml = quickGuidesHtml + mainResult.quickGuideText;
                            quickGuidesHtml = quickGuidesHtml + "</span>";
                            quickGuidesHtml = quickGuidesHtml + "</td>";
                            quickGuidesHtml = quickGuidesHtml + "</tr>";
                            quickGuidesHtml = quickGuidesHtml + "</tbody>";
                            quickGuidesHtml = quickGuidesHtml + "</table>";
                            groupResultCount++;
                        }

                    }
                    #endregion
                    ViewData["quickGuidesResults"] = quickGuidesHtml;
                    setVehicleFeautes();
                    return View();
                }
                else
                {
                    Response.Redirect(@"\Errors\Index");
                    return View("Index", "Errors");
                }

            }
            catch (Exception ex)
            {
                Response.Redirect(@"\Errors\Index");
                return View("Index", "Errors");
                //throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Custom methods

        #region getPhoneDetails
        /// <summary>
        /// get all phone details for systems
        /// </summary>
        /// <returns></returns>
        private phoneDetails getPhoneDetails()
        {
            try
            {
                string systems = apiService.getSystemsForModel(Convert.ToInt32(Session["Models"]), Session["ModelYears"].ToString());
                //string systems = "[{\"Id\":106}]";
                systemDetails[] systemIds = (systemDetails[])(sr.Deserialize<systemDetails[]>(systems));
                string ids = "";
                foreach (var systemId in systemIds)
                {
                    ids = ids + "," + systemId.Id;
                }
                if (ids != "")
                {
                    ids = ids.Substring(1, ids.Length - 1);
                    string strPhoneDetails = apiService.getDistinctPhoneDetailsAsPerDeviceType(ids);
                    phoneDetails phonesSelected = new phoneDetails();
                    if (!String.IsNullOrEmpty(strPhoneDetails))
                    {
                        phonesSelected = (phoneDetails)(sr.Deserialize<phoneDetails>(strPhoneDetails));
                    }
                    else
                    {
                        Session["error"] = "No Phones for this system. Please try agian.";
                    }

                    return phonesSelected;
                }
                else
                {
                    Session["error"] = "Sorry. The vehicle's system cannot be identified. Please try agian.";
                    return null;
                }

            }
            catch (Exception e)
            {
                Response.Redirect(@"\Errors\Index");
                throw;
            }
        }
        #endregion

        #region get Brand Models
        [SessionExpireFilter]
        public string getBrandModels(Int32 brandId)
        {
            try
            {
                phoneModels[] filteredModels;
                phoneModels[] allModels = (phoneModels[])(Session["allModels"]);
                if (brandId == 0)
                {
                    filteredModels = allModels;
                }
                else
                {
                    filteredModels = allModels.Where(model => model.BrandId == brandId).ToArray();
                }
                string models = sr.Serialize(filteredModels);
                return models;
            }
            catch (Exception)
            {
                Response.Redirect(@"\Errors\Index");
                throw;
            }
        }
        #endregion

        #region get Models photos
        public string getModelPhotos()
        {
            //phoneModels[] filteredModels;
            //phoneModels[] allModels = (phoneModels[])(Session["allModels"]);
            //filteredModels = allModels.Where(model => model.BrandId == brandId).ToArray();
            //string models = sr.Serialize(filteredModels);
            return "";

        }
        #endregion

        #region get Top Feature Results for a TestInstance
        public string getTopFeatureResults(Int32 testInstanceId)
        {
            string topFeatureResults = apiService.getTopFeatureResults(testInstanceId);
            return topFeatureResults;
        }
        #endregion

        #region get Top Feature Results HTML for a TestInstance
        [SessionExpireFilter]
        public string getTopFeatureResultsHTML(Int32 testInstanceId)
        {
            try
            {
                List<topFeatureResults> featureResults;
                topFeatureResults[] allModels = (topFeatureResults[])(Session["topFeatureResults"]);

                featureResults = allModels.Where(model => model.TestInstanceId == testInstanceId).ToList();
                string conclusion = "";
                ViewData["Conclusion"] = conclusion = featureResults[0].Conclusion.ToUpper() == "PARTIAL FUNCTION" ? "Partially Compatible" : featureResults[0].Conclusion;
                Session["Conclusion"] = conclusion;
                List<topFeatureResults> resltToRemove = featureResults.Where(fetRslt => fetRslt.TopFeatureName.ToLower() == "enhanced audio quality").ToList();
                if (resltToRemove.Count > 0)
                {
                    featureResults.Remove(resltToRemove.First());
                }
                string stopFeatureResults = sr.Serialize(featureResults);
                #region topfeaturesHtml
                string resultsHtml = "";

                resultsHtml = resultsHtml + "<tr>";
                resultsHtml = resultsHtml + "<td id=\"tdMainFeature\" colspan=\"3\" class=\"compat\" >";
                resultsHtml = resultsHtml + conclusion;
                resultsHtml = resultsHtml + "</td>";
                resultsHtml = resultsHtml + "</tr>";
                resultsHtml = resultsHtml + "<tr>";
                resultsHtml = resultsHtml + "<td colspan=\"3\" >";
                resultsHtml = resultsHtml + "<hr />";
                resultsHtml = resultsHtml + "</td>";
                resultsHtml = resultsHtml + "</tr>";
                resultsHtml = resultsHtml + "<tr>";
                resultsHtml = resultsHtml + "<td align=\"center\" valign=\"top\" >";
                resultsHtml = resultsHtml + "<div id=\"tdFeature1\" style=\"height: 20px; padding-top: 10px\"> ";
                resultsHtml = resultsHtml + (featureResults.Count > 0 ? featureResults[0].TopFeatureName : ""); //"Hands-Free Call";
                resultsHtml = resultsHtml + "</div>";
                resultsHtml = resultsHtml + "<div id=\"tdResult1\" style=\"clear: both; padding-top: 10px; color: Black\" >";
                resultsHtml = resultsHtml + getImage(featureResults.Count > 0 ? featureResults[0].TopFeatureResult : ""); //"<img id=\"imgResult1\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" /></div>";
                resultsHtml = resultsHtml + "</td>";
                resultsHtml = resultsHtml + "<td align=\"center\" valign=\"top\" >";
                resultsHtml = resultsHtml + "<div id=\"tdFeature2\" style=\"height: 20px; padding-top: 10px\" >";
                resultsHtml = resultsHtml + (featureResults.Count > 1 ? featureResults[1].TopFeatureName : ""); //"Phonebook Use";
                resultsHtml = resultsHtml + "</div>";
                resultsHtml = resultsHtml + "<div id=\"tdResult2\" style=\"clear: both; padding-top: 10px; color: Black\" >";
                resultsHtml = resultsHtml + getImage(featureResults.Count > 1 ? featureResults[1].TopFeatureResult : ""); //"<img id=\"imgResult2\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
                resultsHtml = resultsHtml + "</div>";
                resultsHtml = resultsHtml + "</td>";
                resultsHtml = resultsHtml + "<td align=\"center\" valign=\"top\" >";
                resultsHtml = resultsHtml + "<div id=\"tdFeature3\" style=\"height: 20px; padding-top: 10px\" >";
                resultsHtml = resultsHtml + (featureResults.Count > 2 ? featureResults[2].TopFeatureName : ""); //"Auto-Connection";
                resultsHtml = resultsHtml + "</div>";
                resultsHtml = resultsHtml + "<div id=\"tdResult3\" style=\"clear: both; padding-top: 10px; color: Black\" >";
                resultsHtml = resultsHtml + getImage(featureResults.Count > 2 ? featureResults[2].TopFeatureResult : ""); //"<img id=\"imgResult3\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
                resultsHtml = resultsHtml + "</div>";
                resultsHtml = resultsHtml + "</td>";
                resultsHtml = resultsHtml + "</tr>";
                resultsHtml = resultsHtml + "<tr>";
                resultsHtml = resultsHtml + "<td colspan=\"3\">";
                resultsHtml = resultsHtml + "<hr />";
                resultsHtml = resultsHtml + "</td>";
                resultsHtml = resultsHtml + "</tr>";
                //resultsHtml = resultsHtml + "<tr>";
                //resultsHtml = resultsHtml + "<td align=\"center\" valign=\"top\">";
                //resultsHtml = resultsHtml + "<div id=\"tdFeature4\" style=\"height: 20px; padding-top: 10px\">";
                //resultsHtml = resultsHtml + (featureResults.Count > 3 ? featureResults[3].TopFeatureName : ""); //"Conference Call";
                //resultsHtml = resultsHtml + "</div>";
                //resultsHtml = resultsHtml + "<div id=\"tdResult4\" style=\"clear: both; padding-top: 10px; color: Black\">";
                //resultsHtml = resultsHtml + getImage(featureResults.Count > 3 ? featureResults[3].TopFeatureResult : ""); //"<img id=\"imgResult4\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
                //resultsHtml = resultsHtml + "</div>";
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" valign=\"top\">";
                //resultsHtml = resultsHtml + "<div id=\"tdFeature5\" style=\"height: 20px; padding-top: 10px\">";
                //resultsHtml = resultsHtml + (featureResults.Count > 4 ? featureResults[4].TopFeatureName : ""); //"Network Info";
                //resultsHtml = resultsHtml + "</div>";
                //resultsHtml = resultsHtml + "<div id=\"tdResult5\" style=\"clear: both; padding-top: 10px; color: Black\">";
                //resultsHtml = resultsHtml + getImage(featureResults.Count > 4 ? featureResults[4].TopFeatureResult : ""); //"<img id=\"imgResult5\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
                //resultsHtml = resultsHtml + "</div>";
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" valign=\"top\">";
                //resultsHtml = resultsHtml + "<div id=\"tdFeature6\" style=\"height: 20px; padding-top: 10px\">";
                //resultsHtml = resultsHtml + (featureResults.Count > 5 ? featureResults[5].TopFeatureName : ""); //"Profile";
                //resultsHtml = resultsHtml + "</div>";
                //resultsHtml = resultsHtml + "<div id=\"tdResult6\" style=\"clear: both; padding-top: 10px; color: Black\">";
                //resultsHtml = resultsHtml + (featureResults.Count > 5 ? featureResults[5].TopFeatureResult : ""); //"HFP";
                //resultsHtml = resultsHtml + "</div>";
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "</tr>";
                //resultsHtml = resultsHtml + "<tr>";
                //resultsHtml = resultsHtml + "<td colspan=\"3\">";
                //resultsHtml = resultsHtml + "<hr />";
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "</tr>";

                #region Old Html
                //resultsHtml = resultsHtml + "<table style=\"text-align: center; height: 92px;\" cellpadding=\"0\" cellspacing=\"0\" width=\"710\">";
                //resultsHtml = resultsHtml + "<tbody>";
                //resultsHtml = resultsHtml + "<tr style=\"height: 25px;\">";
                //resultsHtml = resultsHtml + "<td id=\"tdMainFeature\" colspan=\"6\" style=\"text-align: center; font-weight: bold;font-size: small; padding-top: 5px;\">";
                //resultsHtml = resultsHtml + conclusion;
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "</tr>";
                //resultsHtml = resultsHtml + "<tr style=\"height: 20px;\">";
                //resultsHtml = resultsHtml + "<td align=\"center\" id=\"tdFeature1\" style=\"width: 142px; padding-top: 10px; font-size: 11px; font-weight: bold; letter-spacing: 0px; vertical-align: top;\">";
                //resultsHtml = resultsHtml + featureResults[0].TopFeatureName;
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" id=\"tdFeature2\" style=\"width: 142px; padding-top: 10px; font-size: 11px; font-weight: bold; letter-spacing: 0px; vertical-align: top;\">";
                //resultsHtml = resultsHtml + featureResults[1].TopFeatureName;                
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" id=\"tdFeature3\" style=\"width: 142px; padding-top: 10px; font-size: 11px;font-weight: bold; letter-spacing: 0px; vertical-align: top;\">";
                //resultsHtml = resultsHtml + featureResults[2].TopFeatureName;                
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" id=\"tdFeature4\" style=\"width: 141px; padding-top: 10px; font-size: 11px;font-weight: bold; letter-spacing: 0px; vertical-align: top;\">";
                //resultsHtml = resultsHtml + featureResults[3].TopFeatureName;                
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" id=\"tdFeature5\" style=\"width: 141px; padding-top: 10px; font-size: 11px;font-weight: bold; letter-spacing: 0px; vertical-align: top;\">";
                //resultsHtml = resultsHtml + featureResults[4].TopFeatureName;                
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "</tr>";
                //resultsHtml = resultsHtml + "<tr style=\"height: 30px;\">";
                //resultsHtml = resultsHtml + "<td align=\"center\" style=\"width: 142px; padding-bottom: 5px;\" id=\"tdResult1\">";
                //resultsHtml = resultsHtml + getImage(featureResults[0].TopFeatureResult);
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" style=\"width: 142px; padding-bottom: 5px;\" id=\"tdResult2\">";
                //resultsHtml = resultsHtml + getImage(featureResults[1].TopFeatureResult);
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" style=\"width: 142px; padding-bottom: 5px;\" id=\"tdResult3\">";
                //resultsHtml = resultsHtml + getImage(featureResults[2].TopFeatureResult);
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" style=\"width: 141px; padding-bottom: 5px;\" id=\"tdResult4\">";
                //resultsHtml = resultsHtml + getImage(featureResults[3].TopFeatureResult);
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "<td align=\"center\" style=\"width: 141px; padding-bottom: 5px;\" id=\"tdResult5\">";
                //resultsHtml = resultsHtml + getImage(featureResults[4].TopFeatureResult);
                //resultsHtml = resultsHtml + "</td>";
                //resultsHtml = resultsHtml + "</tr>";
                //resultsHtml = resultsHtml + "</tbody>";
                //resultsHtml = resultsHtml + "</table>";
                #endregion

                #endregion
                string commentsHtml = "";
                #region Comments html
                if (conclusion.ToLower() != "not recommended")
                {
                    Random rnd = new Random();
                    string href = "/Phones/moreinfo" + "?testInstanceId=" + testInstanceId + "&" + rnd.Next();
                    commentsHtml = commentsHtml + "<div style=\"text-align: center; width: 710px;\">";
                    commentsHtml = commentsHtml + "<a href=\"" + href + "\" id=\"aMoreInfo\" >";
                    commentsHtml = commentsHtml + "<img style=\"border: 0px;\" src=\"/images/anchBtn.jpg\" alt=\"\">";
                    commentsHtml = commentsHtml + "</a>";
                    commentsHtml = commentsHtml + "</div>";
                }
                else
                {
                    commentsHtml = commentsHtml + "<div id=\"divComm\" style=\"text-align: left;font-size:11px;letter-spacing:0px;\">";
                    commentsHtml = commentsHtml + "<div style=\"width: 690px;\" id=\"finalComment\">";
                    commentsHtml = commentsHtml + featureResults[0].CommentText;
                    commentsHtml = commentsHtml + "</div>";
                    commentsHtml = commentsHtml + "</div>";
                }
                #endregion

                stopFeatureResults = conclusion + "|" + resultsHtml + "|" +  commentsHtml;
                return stopFeatureResults;

            }
            catch (Exception)
            {
                Response.Redirect(@"\Errors\Index");
                throw;
            } 


        }
        #endregion

        #region get Main Feature Results
        public string getMainFeatureResults(Int32 testInstanceId)
        {
            ViewData["testInstanceId"] = testInstanceId;
            string mainFeatureResults = apiService.getMainFeatureResults(testInstanceId);
            string comments = apiService.getComments(testInstanceId);
            string finalComment = apiService.getFinalComments(testInstanceId);
            string tmainFeaturesResultsHtml = getFeatureResultsHtml(mainFeatureResults + "|" + comments + "|" + finalComment);
            return tmainFeaturesResultsHtml;
        }
        #endregion

        #region get Add Feature Results
        public string getAddFeatureResults(Int32 testInstanceId)
        {
            ViewData["testInstanceId"] = testInstanceId;
            string addFeatureResults = apiService.getAdditionalFeatureResults(testInstanceId);
            string comments = apiService.getComments(testInstanceId);
            string finalComment = apiService.getFinalComments(testInstanceId);
            string tAddFeaturesResultsHtml = getFeatureResultsHtml(addFeatureResults + "|" + comments + "|" + finalComment);
            return tAddFeaturesResultsHtml;
        }
        #endregion

        #region get Html for Top Feature Results
        /// <summary>
        /// get Html for Top Feature Results 
        /// </summary>
        /// <param name="topfeatures">JQuery string with all the topfeature results</param>
        /// <returns></returns>
        public string getTopFeatureResultsHtml(string topfeatures)
        {
            List<topFeatureResults> featureResults = (List<topFeatureResults>)(sr.Deserialize<List<topFeatureResults>>(topfeatures));
            string conclusion = "";
            ViewData["Conclusion"] = conclusion = featureResults[0].Conclusion.ToUpper() == "PARTIAL FUNCTION" ? "PARTIALLY COMPATIBLE" : featureResults[0].Conclusion.ToUpper();
            Session["Conclusion"] = conclusion;
            List<topFeatureResults> resltToRemove = featureResults.Where(fetRslt => fetRslt.TopFeatureName.ToLower() == "enhanced audio quality").ToList();
            if (resltToRemove.Count > 0)
            {
                featureResults.Remove(resltToRemove.First());
            }

            #region Creating HTML
            string htmlForTopFetures = "";
            htmlForTopFetures = htmlForTopFetures + "<tr>";
            htmlForTopFetures = htmlForTopFetures + "<td colspan=\"3\" class=\"compat\" >";
            htmlForTopFetures = htmlForTopFetures + conclusion;
            htmlForTopFetures = htmlForTopFetures + "</td>";
            htmlForTopFetures = htmlForTopFetures + "</tr>";
            htmlForTopFetures = htmlForTopFetures + "<tr>";
            htmlForTopFetures = htmlForTopFetures + "<td colspan=\"3\" >";
            htmlForTopFetures = htmlForTopFetures + "<hr />";
            htmlForTopFetures = htmlForTopFetures + "</td>";
            htmlForTopFetures = htmlForTopFetures + "</tr>";
            htmlForTopFetures = htmlForTopFetures + "<tr>";
            htmlForTopFetures = htmlForTopFetures + "<td align=\"center\" valign=\"top\" >";
            htmlForTopFetures = htmlForTopFetures + "<div style=\"height: 20px; padding-top: 10px\"> ";
            htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 0 ? featureResults[0].TopFeatureName : ""); //"Hands-Free Call";
            htmlForTopFetures = htmlForTopFetures + "</div>";
            htmlForTopFetures = htmlForTopFetures + "<div style=\"clear: both; padding-top: 10px; color: Black\" >";
            htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 0 ? featureResults[0].TopFeatureResult : ""); //"<img id=\"imgResult1\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" /></div>";
            htmlForTopFetures = htmlForTopFetures + "</td>";
            htmlForTopFetures = htmlForTopFetures + "<td align=\"center\" valign=\"top\" >";
            htmlForTopFetures = htmlForTopFetures + "<div style=\"height: 20px; padding-top: 10px\" >";
            htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 1 ? featureResults[1].TopFeatureName : ""); //"Phonebook Use";
            htmlForTopFetures = htmlForTopFetures + "</div>";
            htmlForTopFetures = htmlForTopFetures + "<div style=\"clear: both; padding-top: 10px; color: Black\" >";
            htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 1 ? featureResults[1].TopFeatureResult : ""); //"<img id=\"imgResult2\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
            htmlForTopFetures = htmlForTopFetures + "</div>";
            htmlForTopFetures = htmlForTopFetures + "</td>";
            htmlForTopFetures = htmlForTopFetures + "<td align=\"center\" valign=\"top\" >";
            htmlForTopFetures = htmlForTopFetures + "<div style=\"height: 20px; padding-top: 10px\" >";
            htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 2 ? featureResults[2].TopFeatureName : ""); //"Auto-Connection";
            htmlForTopFetures = htmlForTopFetures + "</div>";
            htmlForTopFetures = htmlForTopFetures + "<div style=\"clear: both; padding-top: 10px; color: Black\" >";
            htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 2 ? featureResults[2].TopFeatureResult : ""); //"<img id=\"imgResult3\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
            htmlForTopFetures = htmlForTopFetures + "</div>";
            htmlForTopFetures = htmlForTopFetures + "</td>";
            htmlForTopFetures = htmlForTopFetures + "</tr>";
            htmlForTopFetures = htmlForTopFetures + "<tr>";
            htmlForTopFetures = htmlForTopFetures + "<td colspan=\"3\">";
            htmlForTopFetures = htmlForTopFetures + "<hr />";
            htmlForTopFetures = htmlForTopFetures + "</td>";
            htmlForTopFetures = htmlForTopFetures + "</tr>";
            //htmlForTopFetures = htmlForTopFetures + "<tr>";
            //htmlForTopFetures = htmlForTopFetures + "<td align=\"center\" valign=\"top\">";
            //htmlForTopFetures = htmlForTopFetures + "<div style=\"height: 20px; padding-top: 10px\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 3 ? featureResults[3].TopFeatureName : ""); //"Conference Call";
            //htmlForTopFetures = htmlForTopFetures + "</div>";
            //htmlForTopFetures = htmlForTopFetures + "<div style=\"clear: both; padding-top: 10px; color: Black\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 3 ? featureResults[3].TopFeatureResult : ""); //"<img id=\"imgResult4\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
            //htmlForTopFetures = htmlForTopFetures + "</div>";
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td align=\"center\" valign=\"top\">";
            //htmlForTopFetures = htmlForTopFetures + "<div style=\"height: 20px; padding-top: 10px\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 4 ? featureResults[4].TopFeatureName : ""); //"Network Info";
            //htmlForTopFetures = htmlForTopFetures + "</div>";
            //htmlForTopFetures = htmlForTopFetures + "<div style=\"clear: both; padding-top: 10px; color: Black\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 4 ? featureResults[4].TopFeatureResult : ""); //"<img id=\"imgResult5\" src=\"../../images/greenTick.gif\" style=\"border-width: 0px;\" />";
            //htmlForTopFetures = htmlForTopFetures + "</div>";
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td align=\"center\" valign=\"top\">";
            //htmlForTopFetures = htmlForTopFetures + "<div style=\"height: 20px; padding-top: 10px\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 5 ? featureResults[5].TopFeatureName : ""); //"Profile";
            //htmlForTopFetures = htmlForTopFetures + "</div>";
            //htmlForTopFetures = htmlForTopFetures + "<div style=\"clear: both; padding-top: 10px; color: Black\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 5 ? featureResults[5].TopFeatureResult : ""); //"HFP";
            //htmlForTopFetures = htmlForTopFetures + "</div>";
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "</tr>";
            //htmlForTopFetures = htmlForTopFetures + "<tr>";
            //htmlForTopFetures = htmlForTopFetures + "<td colspan=\"3\">";
            //htmlForTopFetures = htmlForTopFetures + "<hr />";
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "</tr>";

            #region Old Html
            //htmlForTopFetures = htmlForTopFetures + "<tbody>";
            //htmlForTopFetures = htmlForTopFetures + "<tr style=\"border-bottom:3px solid #979797;height:26px;\">";
            //htmlForTopFetures = htmlForTopFetures + "<td colspan=\"3\" style=\"font-size:small;font-weight:bold;border-bottom:1px solid #979797;\" >";
            //htmlForTopFetures = htmlForTopFetures + conclusion;
            //htmlForTopFetures = htmlForTopFetures + "</td></tr>";
            //htmlForTopFetures = htmlForTopFetures + "<tr style=\"height:26px;\">";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 0 ? featureResults[0].TopFeatureName : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 1 ? featureResults[1].TopFeatureName : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 2 ? featureResults[2].TopFeatureName : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "</tr>";
            //htmlForTopFetures = htmlForTopFetures + "<tr style=\"border-bottom:3px solid #979797;height:26px;\">";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;border-bottom:1px solid #979797;\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 0 ? featureResults[0].TopFeatureResult : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;border-bottom:1px solid #979797;\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 1 ? featureResults[1].TopFeatureResult : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;border-bottom:1px solid #979797;\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 2 ? featureResults[2].TopFeatureResult : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "</tr>";
            //htmlForTopFetures = htmlForTopFetures + "<tr style=\"height:26px;\">";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 3 ? featureResults[3].TopFeatureName : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 4 ? featureResults[4].TopFeatureName : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + (featureResults.Count > 5 ? featureResults[5].TopFeatureName : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "</tr>";
            //htmlForTopFetures = htmlForTopFetures + "<tr style=\"border-bottom:3px solid #979797;height:26px;\">";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 3 ? featureResults[3].TopFeatureResult : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 4 ? featureResults[4].TopFeatureResult : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "<td style=\"padding:4px 0px 4px;\">";
            //htmlForTopFetures = htmlForTopFetures + getImage(featureResults.Count > 5 ? featureResults[5].TopFeatureResult : "");
            //htmlForTopFetures = htmlForTopFetures + "</td>";
            //htmlForTopFetures = htmlForTopFetures + "</tr>";
            //htmlForTopFetures = htmlForTopFetures + "</tbody>";
            #endregion

            #endregion
            htmlForTopFetures = htmlForTopFetures.Replace("'", "");

            return htmlForTopFetures;

        }
        #endregion

        #region get the image for a result
        /// <summary>
        /// to get the image for a result
        /// </summary>
        /// <param name="r">Result for which images is required</param>
        /// <returns></returns>
        public string getImage(string r)
        {
            var h = "";
            switch (r.ToUpper())
            {
                case "YES" : 
                    h= "<img src=\"/images/greenTick.gif\" />";
                    break;
                case "NO":
                    h = "<img src=\"/images/redCross.gif\" />";
                    break;
                case "1":
                    h = "<img src=\"/images/greenTick.gif\" />";
                    break;
                case "0":
                    h = "<img src=\"/images/redCross.gif\" />";
                    break;
                default:
                    h = r;
                    break;
            }    
            
            return h;
        }

        #endregion

        #region get Html for Main Feature Results
        /// <summary>
        /// get Html for Main Feature Results 
        /// </summary>
        /// <param name="topfeatures">JQuery string with all the Main feature results</param>
        /// <returns></returns>
        public string getFeatureResultsHtml(string results)
        {
            List<FeatureResults> objFeaturesResult = (List<FeatureResults>)(sr.Deserialize<List<FeatureResults>>(results.Split('|')[0]));
            List<commnets> objComments = (List<commnets>)(sr.Deserialize<List<commnets>>(results.Split('|')[1]));
            string featureResultsHtml = "";
            #region final Comment
            string finalComment = results.Split('|')[2];
            if (!string.IsNullOrEmpty(finalComment))
            {

                featureResultsHtml = featureResultsHtml + "<div id=\"divComment\" style=\"font-weight:bold;font-size:14px;padding:20px 15px 2px 15px\">";
                featureResultsHtml = featureResultsHtml + "Comments";
                featureResultsHtml = featureResultsHtml + "</div>";
                featureResultsHtml = featureResultsHtml + "<div id=\"divComment\" style=\"letter-spacing:0px;font-size:11px;padding:5px 15px 2px 15px\">";
                featureResultsHtml = featureResultsHtml + finalComment;
                featureResultsHtml = featureResultsHtml + "</div>";

            }
            #endregion     
            #region Creating HTML
            List<Int32> sectionNumbers = objFeaturesResult.Select(result => result.SectionNumber).Distinct().ToList();
            foreach (var sectionNumber in sectionNumbers)
            {
                List<FeatureResults> resultsForSection = objFeaturesResult.Where(result => result.SectionNumber == sectionNumber).ToList();
                featureResultsHtml = featureResultsHtml + "<div id=\"divHeading\" style=\"font-weight:bold;font-size:14px;padding:20px 0px 2px 15px\">";
                featureResultsHtml = featureResultsHtml + resultsForSection[0].SectionName;
                featureResultsHtml = featureResultsHtml + "</div>";
                featureResultsHtml = featureResultsHtml + "<div>";
                featureResultsHtml = featureResultsHtml + "<table style=\"width:100%;font-size:11px;border-collapse:collapse;\" >";
                bool bgColorSet = false;
                foreach (var mainResult in resultsForSection)
                {
                    if (!bgColorSet)
                    {
                        //featureResultsHtml = featureResultsHtml + "<tr style=\"width:100%;background-color:#EDEDED;height:20px;\" >";
                        featureResultsHtml = featureResultsHtml + "<tr>";
                        bgColorSet = true;
                    }
                    else
                    {
                        featureResultsHtml = featureResultsHtml + "<tr>";
                        bgColorSet = false;
                    }

                    featureResultsHtml = featureResultsHtml + "<td style=\"width:2%\" >";
                    featureResultsHtml = featureResultsHtml + getImage(mainResult.Passed.ToString());
                    featureResultsHtml = featureResultsHtml + "</td>";
                    featureResultsHtml = featureResultsHtml + "<td style=\"width:98%\" >";
                    featureResultsHtml = featureResultsHtml + mainResult.FeatureName;
                    featureResultsHtml = featureResultsHtml + "</td>";
                    featureResultsHtml = featureResultsHtml + "</tr>";
                }
                #region Add comments
                List<commnets> commentsForSection = objComments.Where(commnt => commnt.SectionNumber == sectionNumber).ToList();
                if (commentsForSection.Count > 0)
                {
                    featureResultsHtml = featureResultsHtml + "<tr>";
                    featureResultsHtml = featureResultsHtml + "<td style=\"width:2%\" >";
                    featureResultsHtml = featureResultsHtml + "</td>";
                    featureResultsHtml = featureResultsHtml + "<td style=\"letter-spacing:0px;font-size: 11px;width:100%\" >";
                    featureResultsHtml = featureResultsHtml + "<br />";
                    string sComment = "";
                    int iConut = 1;
                    foreach (var oCommnt in commentsForSection)
                    {
                        sComment = sComment + oCommnt.comment;
                        if (iConut <= commentsForSection.Count)
                        {
                            sComment = sComment + "<br />"; 
                        }
                        iConut++;
                    }
                    featureResultsHtml = featureResultsHtml + sComment;
                    featureResultsHtml = featureResultsHtml + "</td>";
                    featureResultsHtml = featureResultsHtml + "</tr>";
                }
                #endregion
                featureResultsHtml = featureResultsHtml + "</table>";
                featureResultsHtml = featureResultsHtml + "</div>";
            }
            #endregion
            featureResultsHtml = featureResultsHtml.Replace("'", "");

            return featureResultsHtml;
        }
        #endregion

        #region getPhoneSoftwareVersionsLst
        /// <summary>
        /// creates the slect list to fill the drop down for Software Verions
        /// </summary>
        /// <param name="filteredModels"> phone for which the software versions are required</param>
        /// <returns></returns>
        private static SelectList getPhoneSoftwareVersionsLst(phoneModels filteredModels)
        {
            List<drpDetails> drpPhoneSoftwares = new List<drpDetails>();
            string[] phoneSoftwares = filteredModels.PhoneSoftwareVersion.Split(',');
            foreach (var software in phoneSoftwares)
            {
                drpPhoneSoftwares.Add(new drpDetails(Convert.ToInt32(software.Split('|')[0]), software.Split('|')[1], false));
            }
            var items = new SelectList(drpPhoneSoftwares, "ID", "Name", 0);
            return items;
        }
        #endregion

        #region setVehicleFeautes
        /// <summary>
        /// 
        /// </summary>
        private void setVehicleFeautes()
        {

            ViewData["VehicleName"] = Session["VehicleName"].ToString();
            string vehicleName = (Session["VehicleName"].ToString().Substring(0, Session["VehicleName"].ToString().IndexOf('-') - 1)).Trim();
            ViewData["VehiclePhoto"] = @"/images/Vehicles/" + vehicleName.Replace(" ", "") + ".jpg";
        }

        #endregion

        #region Returns HTML for the quick guides
        /// <summary>
        /// returns HTML for the quick guides
        /// </summary>
        /// <param name="quickGuides"></param>
        /// <returns></returns>
        public string getQuickGuidesHtml(string quickGuides)
        {
            List<QuickGuides> objFeaturesResult = (List<QuickGuides>)(sr.Deserialize<List<QuickGuides>>(quickGuides));
            string quickGuidesHtml = "";
            int sectionCount = 1;
            string[] groupingCss = { "smallText", "vehicleText" };
            string[] imageStyles = { "style=\"border-width: 0px; height: 23px; width: 23px;\"" , "style=\"border-width: 0px;\"" };
            string vehicleImage = "";
            ViewData["VehicleName"] = Session["VehicleName"].ToString();
            string vehicleName = (Session["VehicleName"].ToString().Substring(0, Session["VehicleName"].ToString().IndexOf('-') - 1)).Trim();
            ViewData["VehiclePhoto"] = vehicleImage = @"/images/Vehicles/icons/" + vehicleName.Replace(" " , "") + ".gif";

            string[] imagesPath = { ViewData["phonephoto"].ToString(), vehicleImage };

            quickGuidesHtml = quickGuidesHtml + "<div id=\"detailedResults\" style=\"float:left;width: 100%;padding:10px 0px 0px 0px\">";
            #region Creating HTML
            List<string> sections = objFeaturesResult.Select(quickGuide => quickGuide.Section ).Distinct().ToList();
            foreach (var section in sections)
            {
                List<QuickGuides> resultsForSection = objFeaturesResult.Where(quickGuide => quickGuide.Section == section).ToList();
                #region Section header
                quickGuidesHtml = quickGuidesHtml + "<table cellspacing=\"0\" cellpadding=\"0\" style=\"width:424px;border-collapse:collapse;\" >";
                quickGuidesHtml = quickGuidesHtml + "<tr>";
                quickGuidesHtml = quickGuidesHtml + "<td>";
                if (sectionCount != 1)
                {
                    quickGuidesHtml = quickGuidesHtml + "<br />";
                    quickGuidesHtml = quickGuidesHtml + "<hr />";
                    quickGuidesHtml = quickGuidesHtml + "<br />";
                }
                quickGuidesHtml = quickGuidesHtml + "<table style=\"width: 424px;\">";
                quickGuidesHtml = quickGuidesHtml + "<tbody>";
                quickGuidesHtml = quickGuidesHtml + "<tr>";
                quickGuidesHtml = quickGuidesHtml + "<td valign=\"bottom\" style=\"padding-bottom: 4px;\" colspan=\"2\">";

                quickGuidesHtml = quickGuidesHtml + "<span class=\"title\" id=\"lblSection\">";
                quickGuidesHtml = quickGuidesHtml + section;
                quickGuidesHtml = quickGuidesHtml + "</span>";
                quickGuidesHtml = quickGuidesHtml + "</td>";
                if (sectionCount == 1)
                {
                    quickGuidesHtml = quickGuidesHtml + "<td align=\"right\" style=\"font-size: 0.7em;font-size-adjust:none;font-style:normal;font-variant:normal;font-weight:normal;line-height:1;letter-spacing: 0px;\">";
                    quickGuidesHtml = quickGuidesHtml + "Handset instructions are next to";
                    quickGuidesHtml = quickGuidesHtml + "<img align=\"middle\" style=\"border-width: 0px; height: 23px; width: 23px;\" src=\"" + imagesPath[0] + "\" id=\"imgPhone\" />";

                    quickGuidesHtml = quickGuidesHtml + "<br />";
                    quickGuidesHtml = quickGuidesHtml + "and vehicle instructions are next to ";
                    quickGuidesHtml = quickGuidesHtml + "<img align=\"middle\" style=\"border-width: 0px;\" src=\"" + imagesPath[1] + "\" id=\"imgSystem\" />";
                    quickGuidesHtml = quickGuidesHtml + "</td>";
                }
                else
                {
                    quickGuidesHtml = quickGuidesHtml + "<td align=\"right\" style=\"font-size: 0.7em;\">";
                    quickGuidesHtml = quickGuidesHtml + "<br />";
                    quickGuidesHtml = quickGuidesHtml + "</td>";
                }
                quickGuidesHtml = quickGuidesHtml + "</tr>";
                quickGuidesHtml = quickGuidesHtml + "</tbody>";
                quickGuidesHtml = quickGuidesHtml + "</table>";
                quickGuidesHtml = quickGuidesHtml + "</td>";
                quickGuidesHtml = quickGuidesHtml + "</tr>";
                quickGuidesHtml = quickGuidesHtml + "</table>";
                #endregion
                #region quickGuideText

                List<string> grouptypes = resultsForSection.Select(quickGuide => quickGuide.GuideType ).Distinct().ToList();
                quickGuidesHtml = quickGuidesHtml + "<table width=\"424\">";
                quickGuidesHtml = quickGuidesHtml + "<tbody>"; 
                foreach (var groupType in grouptypes)
                {
                    int grouping = groupType == "phone" ? 1 : 2;
                    List<QuickGuides> groupResult = resultsForSection.Where(quickGuide => quickGuide.GuideType == groupType).ToList();
                    int groupResultCount = 1;
                    
                    foreach (var mainResult in groupResult)
                    {
                        quickGuidesHtml = quickGuidesHtml + "<tr>";
                        quickGuidesHtml = quickGuidesHtml + "<td valign=\"middle\" height=\"24\" align=\"center\" width=\"50\">";
                        quickGuidesHtml = quickGuidesHtml + "<span class=\"" + groupingCss[grouping - 1] + "\" />";
                        if (groupResultCount == 1)
                        {
                            quickGuidesHtml = quickGuidesHtml + "<img " + imageStyles[grouping - 1] + " src=\"" + imagesPath[grouping - 1] + "\" />";
                        }
                        quickGuidesHtml = quickGuidesHtml + "</td>";
                        quickGuidesHtml = quickGuidesHtml + "<td valign=\"middle\" width=\"380\" style=\"padding-left:5px;\" >";
                        quickGuidesHtml = quickGuidesHtml + "<span class=\"" + groupingCss[grouping - 1] + "\" >";
                        quickGuidesHtml = quickGuidesHtml + mainResult.quickGuideText;
                        quickGuidesHtml = quickGuidesHtml + "</span>";
                        quickGuidesHtml = quickGuidesHtml + "</td>";
                        quickGuidesHtml = quickGuidesHtml + "</tr>";
                        groupResultCount++;
                    }
                }
                quickGuidesHtml = quickGuidesHtml + "</tbody>";
                quickGuidesHtml = quickGuidesHtml + "</table>";
                #endregion
                sectionCount++;
            }

            #endregion
            quickGuidesHtml = quickGuidesHtml + "</div>";

            quickGuidesHtml = quickGuidesHtml.Replace("'", "");

            return quickGuidesHtml;
        }
        #endregion

        #endregion

    }
}
