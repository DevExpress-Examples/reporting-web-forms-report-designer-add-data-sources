using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using DevExpress.XtraReports.Web.Extensions;

namespace WebApplication1
{
    public class ReportStorageWebExtension1 : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
    {
        readonly string reportDirectory;
        const string FileExtension = ".repx";
        public ReportStorageWebExtension1(string reportDirectory)
        {
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            this.reportDirectory = reportDirectory;
        }
        public override bool CanSetData(string url)
        {
            return !url.Contains("readonly");
        }
        public override bool IsValidUrl(string url)
        {
            return true;
        }

        public override byte[] GetData(string url)
        {
            try
            {
                if (Directory.EnumerateFiles(reportDirectory).Select(Path.GetFileNameWithoutExtension).Contains(url))
                {
                    return File.ReadAllBytes(Path.Combine(reportDirectory, url + FileExtension));
                }
                throw new FaultException(new FaultReason(string.Format("Could not find report '{0}'.", url)), new FaultCode("Server"), "GetData");
            }
            catch (Exception)
            {
                throw new FaultException(new FaultReason(string.Format("Could not find report '{0}'.", url)), new FaultCode("Server"), "GetData");
            }
        }

        public override Dictionary<string, string> GetUrls()
        {
            return Directory.GetFiles(reportDirectory, "*" + FileExtension)
                         .Select(Path.GetFileNameWithoutExtension)
                         .ToDictionary<string, string>(x => x);
        }

        public override void SetData(DevExpress.XtraReports.UI.XtraReport report, string url)
        {
            report.SaveLayoutToXml(Path.Combine(reportDirectory, url + FileExtension));
        }

        public override string SetNewData(DevExpress.XtraReports.UI.XtraReport report, string defaultUrl)
        {
            SetData(report, defaultUrl);
            return defaultUrl;
        }
    }
}
