using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{
    public class SubscriptionPartDisplay : ContentPartDisplayDriver<SubscriptionPart>
    {

        #region Overrides

        public override IDisplayResult Edit(SubscriptionPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionPartEditViewModel>("SubscriptionPart_Edit", model =>
            {
                model.Identifier = part.Identifier;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionPart part, UpdatePartEditorContext context)
        {
            var model = new SubscriptionPartEditViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix)) {
                part.Identifier = model.Identifier;
            }

            return Edit(part, context);
        }

        #endregion

    }
}
