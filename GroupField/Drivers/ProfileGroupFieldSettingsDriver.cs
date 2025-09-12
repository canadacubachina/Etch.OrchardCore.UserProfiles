using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupField.ViewModels;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupField.Drivers
{
    public class ProfileGroupFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<ProfileGroupField>
    {
        #region ContentPartFieldDefinitionDisplayDriver

        #region Edit

        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition, BuildEditorContext context)
        {
            return Initialize<ProfileGroupFieldSettings>("ProfileGroupFieldSettings_Edit", model =>
            {
                var settings = partFieldDefinition.GetSettings<ProfileGroupFieldSettings>();

                model.Hint = settings.Hint;
                model.Multiple = settings.Multiple;
                model.Required = settings.Required;
            })
            .Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
        {
            var model = new ProfileGroupFieldSettings();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                context.Builder.WithSettings(model);
            }

            return Edit(partFieldDefinition, context);
        }

        #endregion Edit

        #endregion ContentPartFieldDefinitionDisplayDriver
    }
}
