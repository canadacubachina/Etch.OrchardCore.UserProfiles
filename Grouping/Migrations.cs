using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using YesSql.Sql;

namespace Etch.OrchardCore.UserProfiles.Grouping
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

        public async Task<int> Create()
        {
           await _contentDefinitionManager.AlterPartDefinitionAsync("ProfileGroupPart", builder => builder
                .Attachable()
                .WithDescription("Add ability to group user profiles."));

           await SchemaBuilder.CreateMapIndexTableAsync<ProfileGroupPartIndex>(table => { });

          await  SchemaBuilder.CreateMapIndexTableAsync<ProfileGroupedPartIndex>(table => table
                .Column<string>("GroupContentItemId", c => c.WithLength(26))
            );

          await  SchemaBuilder.AlterTableAsync(nameof(ProfileGroupedPartIndex), table => table
                .CreateIndex("IDX_ProfileGroupedPartIndex_GroupContentItemId", "GroupContentItemId")
            );

           await _contentDefinitionManager.AlterTypeDefinitionAsync(Constants.ContentTypeName, type => type
                .WithPart("ProfileGroupedPart")
            );

            return 2;
        }

        public async Task<int> UpdateFrom1()
        {
           await SchemaBuilder.CreateMapIndexTableAsync<ProfileGroupPartIndex>(table => { });
            return 2;
        }

        public async Task<int> UpdateFrom2()
        {
            await _contentDefinitionManager.MigratePartSettingsAsync<ProfileGroupPart, ProfileGroupPartSettings>();
            return 3;
        }

        #endregion
    }
}
