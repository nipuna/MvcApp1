using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{

    public class reqdDetails
    {
        public Boolean AuthenticationStatus { get; set; }
        public int brandId { get; set; }
    }

    public class drpDetails
    {
        public drpDetails(Int32? id, string name, Boolean status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        public int? ID { get; set; }
        public string Name { get; set; }
        public Boolean? Status { get; set; }

    }

    public class detail 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class region
    {
        public int regionId { get; set; }
        public string regionName { get; set; }
    }

    public class systemDetails
    {
        public int Id { get; set; }
    }

    public class phoneDetails
    {
        public phoneBrands[] brands { get; set; }
        public phoneModels[] models { get; set; }
        public List<topFeatureResults> topFeatureResults { get; set; }
    }

    public class phoneBrands
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class phoneModels
    {
        public int RelatedSystemID { get; set; }
        public int PhoneID { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Photo { get; set; }
        public int TestInstanceId { get; set; }
        public string PhoneSoftwareVersion { get; set; }
    }

    public class topFeatureDetails
    {
        public conclOfTopFtrReslts Conclusion { get; set; }
        public phoneModels[] results { get; set; }
    }

    public class topFeatureResults
    {
        public int TestInstanceId { get; set; }
        public int TopFeatureResultId { get; set; }
        public string TopFeatureName { get; set; }
        public int TopFeatureId { get; set; }
        public string TopFeatureResult { get; set; }
        public string Conclusion { get; set; }
        public string CommentText { get; set; }
    }

    public class conclOfTopFtrReslts
    {

        public string Conclusion { get; set; }

    }

    public class FeatureResults
    {
        public int TestInstanceId { get; set; }
        public int SectionNumber { get; set; }
        public string SectionName { get; set; }
        public int? Passed { get; set; }
        public string FeatureName { get; set; }
        public int OrderNumber { get; set; }

    }

    public class QuickGuides
    {
        public string GuideType { get; set; }
        public int DeviceId { get; set; }
        public string Section { get; set; }
        public string quickGuideText { get; set; }
        public int SectionNumber { get; set; }
        public int Grouping { get; set; }
        public int Step { get; set; }
        public bool Discoverable { get; set; }
    }
    
    public class commnets
    {
        public int TestInstanceId { get; set; }
        public int SectionNumber { get; set; }
        public string SectionName { get; set; }
        public string comment { get; set; }
    }

    public class finalComment
    {
        public int TestInstanceId { get; set; }
        public string CommentText { get; set; }
    }

}