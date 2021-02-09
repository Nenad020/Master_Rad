using Dna;
using FuseBoxUI.DI.Interfaces;
using FuseBoxUI.ViewModel.Application;
using Microsoft.Extensions.DependencyInjection;

namespace FuseBoxUI.DI
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddApplicationViewModel(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Return the construction for chaining
            return construction;
        }
        
        /// <summary>
        /// Injects the Fasetto Word client application services needed
        /// for the Fasetto Word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddApplicationClientServices(this FrameworkConstruction construction)
        {
            // Bind a UI Manager
            construction.Services.AddTransient<IUIManager, UIManager>();

            // Return the construction for chaining
            return construction;
        }
    }
}
