using System;
using pctesting.DBService;
using Fiddler;

namespace pctesting
{
    class TrafficManager
    {
        private UrlCaptureConfiguration CaptureConfiguration { get; set; }
        string comp, user;

        public TrafficManager(string user, string comp)
        {
            CaptureConfiguration = new UrlCaptureConfiguration();  // this usually comes from configuration settings
            this.comp = comp;
            this.user = user;
        }

        private void AfterSession(Session sess)
        {
            // Ignore HTTPS connect requests
            if (sess.RequestMethod == "CONNECT")
                return;

            if (CaptureConfiguration.ProcessId > 0)
            {
                if (sess.LocalProcessID != 0 && sess.LocalProcessID != CaptureConfiguration.ProcessId)
                    return;
            }

            if (!string.IsNullOrEmpty(CaptureConfiguration.CaptureDomain))
            {
                if (sess.hostname.ToLower() != CaptureConfiguration.CaptureDomain.Trim().ToLower())
                    return;
            }

            if (CaptureConfiguration.IgnoreResources)
            {
                string url = sess.fullUrl.ToLower();

                var extensions = CaptureConfiguration.ExtensionFilterExclusions;
                foreach (var ext in extensions)
                {
                    if (url.Contains(ext))
                        return;
                }

                var filters = CaptureConfiguration.UrlFilterExclusions;
                foreach (var urlFilter in filters)
                {
                    if (url.Contains(urlFilter))
                        return;
                }
            }

            if (sess == null || sess.oRequest == null || sess.oRequest.headers == null)
                return;

            string headers = sess.oRequest.headers.ToString();
            if (headers.Contains("Referer"))
                return;
            DBService.DataServiceClient client = new DataServiceClient();
            client.saveTrafficDataToDB(sess.fullUrl.ToLower(), (long)DateTime.Now.Ticks/10000, comp, user);
        }

        public void Start()
        {
            CaptureConfiguration.IgnoreResources = true;
            InstallCertificate();

            FiddlerApplication.AfterSessionComplete += AfterSession;
            FiddlerApplication.Startup(8888, true, true, true);
        }

        public void Stop()
        {
            FiddlerApplication.AfterSessionComplete -= AfterSession;

            if (FiddlerApplication.IsStarted())
                FiddlerApplication.Shutdown();
        }

        public static bool InstallCertificate()
        {
            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                    return false;

                if (!CertMaker.trustRootCert())
                    return false;
            }

            return true;
        }

        //public static bool UninstallCertificate()
        //{
        //    if (CertMaker.rootCertExists())
        //    {
        //        if (!CertMaker.removeFiddlerGeneratedCerts(true))
        //            return false;
        //    }
        //    return true;
        //}
    }
}
