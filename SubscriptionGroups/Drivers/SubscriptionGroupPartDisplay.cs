using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{
    public class SubscriptionGroupPartDisplay : ContentPartDisplayDriver<SubscriptionGroupPart>
    {
        #region Overrides

        public override IDisplayResult Edit(SubscriptionGroupPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionGroupPartEditViewModel>("SubscriptionGroupPart_Edit", model =>
            {
                model.Identifier = part.Identifier;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionGroupPart part, UpdatePartEditorContext context)
        {
            var model = new SubscriptionGroupPartEditViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix)) {
                part.Identifier = model.Identifier;
            }

            return Edit(part, context);
        }

        #endregion

    }
}
