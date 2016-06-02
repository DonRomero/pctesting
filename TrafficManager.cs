using System;
using pctesting.DBService;
using Fiddler;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace pctesting
{
    class TrafficManager
    {
        string comp, user;

        public TrafficManager(string user, string comp)
        {
            this.comp = comp;
            this.user = user;
        }

        private void AfterSession(Session sess)
        {
            // Ignore HTTPS connect requests
            if (sess.RequestMethod == "CONNECT")
                return;

            if (sess == null || sess.oRequest == null || sess.oRequest.headers == null|| sess.url.Contains("localhost"))
                return;
            if (sess.fullUrl.Equals("http://www.keva.ru/?cat=ling-themurl"))
                return;
            Regex newReg = new Regex(@"<[\s\S]*body[\s\S]*>[\s\S]*<[\s\S]*>[\s\S]*<\/[\s\S]*body>", RegexOptions.IgnoreCase);
            MatchCollection matches = newReg.Matches(sess.GetResponseBodyAsString());
            if (matches.Count == 0)
                return;
            DBService.DataServiceClient client = new DataServiceClient();
            try
            {
                client.saveTrafficDataToDB(sess.fullUrl.ToLower(), sess.host, sess.oRequest.headers["referer"], (long)DateTime.Now.Ticks / 10000, comp, user);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Возникла ошибка при работе с бозой данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Start()
        {
            InstallCertificate();

            FiddlerApplication.AfterSessionComplete += AfterSession;
            FiddlerApplication.Startup(8888, true, true, true);
        }

        public void Stop()
        {
            FiddlerApplication.AfterSessionComplete -= AfterSession;
            //UninstallCertificate();

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

        public static bool UninstallCertificate()
        {
            if (CertMaker.rootCertExists())
            {
                if (!CertMaker.removeFiddlerGeneratedCerts(true))
                    return false;
            }
            return true;
        }
    }
}
