using System;
using DryIoc;
using ReactiveUI;
using Sextant;
using Splat;
using Splat.DryIoc;
using UIKit;

namespace ListView
{
    public class Composition
    {
        private static IContainer _container;

        static Composition()
        {
            _container = new Container();

            var navigationViewController = new NavigationViewController(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current);
            _container.RegisterInstance<IView>(navigationViewController, serviceKey: nameof(NavigationViewController));
            _container.Register<IParameterViewStackService>(Reuse.Singleton);
            _container.Register<ItemDataService>();
            
            _container.UseDryIocDependencyResolver();
        }

        public static UIViewController StartPage()
        {
            _container.Resolve<IParameterViewStackService>().PushPage<ListViewModel>().Subscribe();
            
            return _container.Resolve<IView>() as UINavigationController;
        }
    }
}