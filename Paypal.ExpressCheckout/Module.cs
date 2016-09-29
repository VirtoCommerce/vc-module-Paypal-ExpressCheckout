using Microsoft.Practices.Unity;
using Paypal.ExpressCheckout.Managers;
using System;
using System.Net;
using VirtoCommerce.Domain.Payment.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Settings;

namespace Paypal.ExpressCheckout
{
    public class Module : ModuleBase
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        #region IModule Members

        public override void PostInitialize()
        {
            var settings = _container.Resolve<ISettingsManager>().GetModuleSettings("Paypal.ExpressCheckout");

            Func<PaypalExpressCheckoutPaymentMethod> paypalBankCardsExpressCheckoutPaymentMethodFactory = () => new PaypalExpressCheckoutPaymentMethod
            {
                Name = "Paypal Express Checkout",
                Description = "PayPal express checkout integration",
                LogoUrl = "https://raw.githubusercontent.com/VirtoCommerce/vc-module-Paypal-ExpressCheckout/master/Paypal.ExpressCheckout/Content/paypal_2014_logo.png",
                Settings = settings
            };

            _container.Resolve<IPaymentMethodsService>().RegisterPaymentMethod(paypalBankCardsExpressCheckoutPaymentMethodFactory);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        #endregion
    }
}
