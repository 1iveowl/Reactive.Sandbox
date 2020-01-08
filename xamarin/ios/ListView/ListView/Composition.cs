using System;
using DryIoc;
using ReactiveUI;
using Sextant;
using Sextant.Abstractions;
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
            _container = new Container(rules => rules.WithoutThrowOnRegisteringDisposableTransient());

            var navigationViewController = new NavigationViewController(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current);
            _container.RegisterInstance<IView>(navigationViewController, serviceKey: nameof(NavigationViewController));
            _container
                .RegisterInstance<IParameterViewStackService>(new ParameterViewStackService(navigationViewController));
            _container.Register<IViewModelFactory, DefaultViewModelFactory>();

            _container.Register<IViewFor<ListViewModel>, ListController>();
            _container.Register<ListViewModel>();
            _container.RegisterInstance<ItemDataService>(new ItemDataService());
            
            _container.UseDryIocDependencyResolver();
        }

        public static UIViewController StartPage()
        {
            _container.Resolve<IParameterViewStackService>().PushPage<ListViewModel>().Subscribe();
            
            return _container.Resolve<IView>() as UINavigationController;
        }
    }
}