// <copyright file="UserMicrosoftAppCredentials.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.CompanyCommunicator.Common.Services.CommonBot
{
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A user Microsoft app credentials object.
    /// </summary>
    public class UserMicrosoftAppCredentials : MicrosoftAppCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMicrosoftAppCredentials"/> class.
        /// </summary>
        /// <param name="botOptions">The bot options.</param>
        public UserMicrosoftAppCredentials(IOptions<BotOptions> botOptions)
            : base(
                  appId: botOptions.Value.UserAppId,
                  password: botOptions.Value.UserAppPassword)
        {
        }
    }
}
