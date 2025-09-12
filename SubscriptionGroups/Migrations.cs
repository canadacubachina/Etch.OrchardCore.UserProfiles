using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups
{
    public class Migrations : DataMigration
    {
        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;

        #endregion

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        #endregion

        #region Migrations

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync("SubscriptionGroupPart", builder => builder
                .WithDescription("Properties for subscription groups."));

            _contentDefinitionManager.AlterTypeDefinitionAsync(Constants.ContentSubscriptionGroupTypeName, type => type
                .WithPart("TitlePart")
                .WithPart("SubscriptionGroupPart")
                .Creatable()
                .Listable()
            );

            _contentDefinitionManager.AlterPartDefinitionAsync("SubscriptionGroupSelectPart", builder => builder
                .Attachable()
                .WithDescription("Add ability to add group select to subscription content types.")
            );

            _contentDefinitionManager.AlterPartDefinitionAsync("SubscriptionGroupAccessPart", builder => builder
                .Attachable()
                .WithDescription("Add ability to add subscription group access to content types.")
            );

            return 1;
        }

        #endregion
    }
}
