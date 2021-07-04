using System.Diagnostics;

namespace Bunnings.Website.Test.WebCommon
{
    public static class SeleniumUtils
    {
        public static void KillWebDrivers()
        {
            var processes = Process.GetProcesses();
            foreach (var p in processes)
            {
                var npName = p.ProcessName.ToLower();
                if (npName.Equals("chromedriver") ||
                    npName.Equals("geckodriver") ||
                    npName.Equals("iedriverserver"))
                //npName.Equals("chrome"))
                {
                    p.Kill();
                }
            }
        }

        public static void KillChromeProcesses()
        {
            var processes = Process.GetProcesses();
            foreach (var p in processes)
            {
                var npName = p.ProcessName.ToLower();
                if (npName.Equals("chrome"))
                {
                    p.Kill();
                }
            }
        }
    }
}
