using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginPrismExample.Infrastructure;
using LoginPrismExample.Login.Views;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace LoginPrismExample.Modules
{
    [ModuleExport(typeof(AppModule))]
    public class AppModule : IModule
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public AppModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(AppRegions.ShellRegion, typeof(LoginView));
        }
    }
}
