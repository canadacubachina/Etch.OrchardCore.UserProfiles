using System;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Settings;
using OrchardCore.ContentManagement.Metadata;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Services
{
    public class SubscriptionLevelService : ISubscriptionLevelService
    {

        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;

        #endregion

        #region Constructor

        public SubscriptionLevelService(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        #endregion

        #region Implementations

        public async Task<SubscriptionLevelPartSettings> GetSettings(SubscriptionLevelPart subscriptionLevelPart)
        {
            var contentTypeDefinition = await _contentDefinitionManager.GetTypeDefinitionAsync(subscriptionLevelPart.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => string.Equals(x.PartDefinition.Name, nameof(SubscriptionLevelPart), StringComparison.Ordinal));
            return contentTypePartDefinition.GetSettings<SubscriptionLevelPartSettings>();
        }

        #endregion
    }

    public interface ISubscriptionLevelService
    {
        Task<SubscriptionLevelPartSettings> GetSettings(SubscriptionLevelPart subscriptionLevelPart);
    }
}
