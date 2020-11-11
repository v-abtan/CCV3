// <copyright file="UserDataRepositoryExtensions.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.CompanyCommunicator.Repositories.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.Bot.Schema;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Repositories.UserData;

    /// <summary>
    /// Extensions for the repository of the user data stored in the table storage.
    /// </summary>
    public static class UserDataRepositoryExtensions
    {
        /// <summary>
        /// Add personal data in Table Storage.
        /// </summary>
        /// <param name="userDataRepository">The user data repository.</param>
        /// <param name="activity">Bot conversation update activity instance.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        public static async Task SaveUserDataAsync(
            this IUserDataRepository userDataRepository,
            IConversationUpdateActivity activity)
        {
            var userDataEntity = UserDataRepositoryExtensions.ParseData(activity, UserDataTableNames.UserDataPartition);
            if (userDataEntity != null)
            {
                await userDataRepository.InsertOrMergeAsync(userDataEntity);
            }
        }

        /// <summary>
        /// Remove personal data in table storage.
        /// </summary>
        /// <param name="userDataRepository">The user data repository.</param>
        /// <param name="activity">Bot conversation update activity instance.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        public static async Task RemoveUserDataAsync(
            this IUserDataRepository userDataRepository,
            IConversationUpdateActivity activity)
        {
            var userDataEntity = UserDataRepositoryExtensions.ParseData(activity, UserDataTableNames.UserDataPartition);
            if (userDataEntity != null)
            {
                var found = await userDataRepository.GetAsync(UserDataTableNames.UserDataPartition, userDataEntity.AadId);
                if (found != null)
                {
                    await userDataRepository.DeleteAsync(found);
                }
            }
        }

        /// <summary>
        /// Add personal data in Table Storage.
        /// </summary>
        /// <param name="userDataRepository">The user data repository.</param>
        /// <param name="activity">Bot conversation update activity instance.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        public static async Task SaveAuthorDataAsync(
            this IUserDataRepository userDataRepository,
            IConversationUpdateActivity activity)
        {
            var userDataEntity = UserDataRepositoryExtensions.ParseData(activity, UserDataTableNames.AuthorDataPartition);
            if (userDataEntity != null)
            {
                await userDataRepository.InsertOrMergeAsync(userDataEntity);
            }
        }

        /// <summary>
        /// Remove personal data in table storage.
        /// </summary>
        /// <param name="userDataRepository">The user data repository.</param>
        /// <param name="activity">Bot conversation update activity instance.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        public static async Task RemoveAuthorDataAsync(
            this IUserDataRepository userDataRepository,
            IConversationUpdateActivity activity)
        {
            var userDataEntity = UserDataRepositoryExtensions.ParseData(activity, UserDataTableNames.AuthorDataPartition);
            if (userDataEntity != null)
            {
                var found = await userDataRepository.GetAsync(UserDataTableNames.AuthorDataPartition, userDataEntity.AadId);
                if (found != null)
                {
                    await userDataRepository.DeleteAsync(found);
                }
            }
        }

        private static UserDataEntity ParseData(IConversationUpdateActivity activity, string partitionKey)
        {
            var rowKey = activity?.From?.AadObjectId;
            if (rowKey != null)
            {
                var userDataEntity = new UserDataEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = activity?.From?.AadObjectId,
                    AadId = activity?.From?.AadObjectId,
                    UserId = activity?.From?.Id,
                    ConversationId = activity?.Conversation?.Id,
                    ServiceUrl = activity?.ServiceUrl,
                    TenantId = activity?.Conversation?.TenantId,
                };

                return userDataEntity;
            }

            return null;
        }
    }
}
