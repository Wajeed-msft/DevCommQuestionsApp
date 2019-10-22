// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;

namespace Microsoft.DevCommQuestionsTracker
{
    public static class CardResponseExtentions
    {
        public static MessagingExtensionActionResponse ToTaskModuleResponse(this AdaptiveCard card)
        {
            return new MessagingExtensionActionResponse
            {
                Task = new TaskModuleContinueResponse
                {
                    Value = new TaskModuleTaskInfo()
                    {
                        Card = new Attachment
                        {
                            Content = card,
                            ContentType = AdaptiveCard.ContentType,
                        },
                        Height = 500,
                        Width = 600,
                        Title = "Track new question",
                    },
                },
            };
        }

        public static MessagingExtensionActionResponse ToComposeExtensionResultResponse(this AdaptiveCard card)
        {
            return new MessagingExtensionActionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    Type = "result",
                    AttachmentLayout = "list",
                    Attachments = new List<MessagingExtensionAttachment>
                    {
                        new MessagingExtensionAttachment
                        {
                            Content = card,
                            ContentType = AdaptiveCard.ContentType,
                        },
                    },
                },
            };
        }

        public static MessagingExtensionActionResponse ToMessagingExtensionBotMessagePreviewResponse(this AdaptiveCard card)
        {
            return new MessagingExtensionActionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    Type = "botMessagePreview",
                    ActivityPreview = MessageFactory.Attachment(new Attachment
                    {
                        Content = card,
                        ContentType = AdaptiveCard.ContentType,
                    }) as Activity,
                },
            };
        }
    }
}
