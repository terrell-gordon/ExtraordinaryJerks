using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Orchard.Themes.Services;

namespace Downplay.Orchard.LayoutSelector.Services
{
    public class DefaultLayoutAlternatesProvider : ILayoutAlternatesProvider
    {
        private readonly ISiteThemeService _siteThemeService;
        public DefaultLayoutAlternatesProvider(ISiteThemeService siteThemeService)
        {
            _siteThemeService = siteThemeService;
        }


        public IEnumerable<string> GetLayouts()
        {
            List<string> t = new List<string>();

            foreach (string template in Directory.EnumerateFiles(HttpContext.Current.Server.MapPath(
                        string.Format("~/Themes/{0}/Views/", _siteThemeService.GetCurrentThemeName())), "*.cshtml"))
            {
                var f = new FileInfo(template);

                if (f.Name.StartsWith("Layout"))
                {
                    string fname = f.Name.Replace("Layout", "").Replace("-", "").Replace(".cshtml", "");

                    if (fname.Length > 0)
                        t.Add(fname);
                    else
                        t.Add("Default");
                }
            }

            return t;
        }
    }
}