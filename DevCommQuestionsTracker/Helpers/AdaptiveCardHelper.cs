﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using AdaptiveCards;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.DevCommQuestionsTracker
{
    public static class AdaptiveCardHelper
    {
        public static SubmitExampleData ToSubmitExampleData(this MessagingExtensionAction action)
        {
            var activityPreview = action.BotActivityPreview[0];
            var attachmentContent = activityPreview.Attachments[0].Content;
            var previewedCard = JsonConvert.DeserializeObject<AdaptiveCard>(attachmentContent.ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            string userText = (previewedCard.Body[1] as AdaptiveTextBlock).Text;
            var choiceSet = previewedCard.Body[3] as AdaptiveChoiceSetInput;

            return new SubmitExampleData()
            {
                Question = userText,
                MultiSelect = choiceSet.IsMultiSelect ? "true" : "false",
                Option1 = choiceSet.Choices[0].Title,
                Option2 = choiceSet.Choices[1].Title,
                Option3 = choiceSet.Choices[2].Title,
            };
        }

        public static MessagingExtensionActionResponse CreateTaskModuleAdaptiveCardResponse(string userText = null, bool isMultiSelect = true, string option1 = null, string option2 = null, string option3 = null)
        {
            return new AdaptiveCard()
            {
                Body = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock("This is an Adaptive Card within a Task Module")
                    {
                        Weight = AdaptiveTextWeight.Bolder,
                    },
                    new AdaptiveTextBlock("Enter text for Question:"),
                    new AdaptiveTextInput() { Id = "Question", Placeholder = "Question text here", Value = userText },
                    new AdaptiveTextBlock("Options for Question:"),
                    new AdaptiveTextBlock("Is Multi-Select:"),
                    new AdaptiveChoiceSetInput()
                    {
                        Type = AdaptiveChoiceSetInput.TypeName,
                        Id = "MultiSelect",
                        Value = isMultiSelect ? "true" : "false",
                        IsMultiSelect = false,
                        Choices = new List<AdaptiveChoice>()
                        {
                            new AdaptiveChoice() { Title = "True", Value = "true" },
                            new AdaptiveChoice() { Title = "False", Value = "false" },
                        },
                    },
                    new AdaptiveTextInput() { Id = "Option1", Placeholder = "Option 1 here", Value = option1 },
                    new AdaptiveTextInput() { Id = "Option2", Placeholder = "Option 2 here", Value = option2 },
                    new AdaptiveTextInput() { Id = "Option3", Placeholder = "Option 3 here", Value = option3 },
                },
                Actions = new List<AdaptiveAction>()
                {
                    new AdaptiveSubmitAction
                    {
                        Type = AdaptiveSubmitAction.TypeName,
                        Title = "Submit",
                        Data = new JObject { { "submitLocation", "messagingExtensionFetchTask" } },
                    },
                },
            }.ToTaskModuleResponse();
        }

        public static AdaptiveCard ToAdaptiveCard(this SubmitExampleData data)
        {
            return new AdaptiveCard()
            {
                Body = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock("Adaptive Card from Task Module") { Weight = AdaptiveTextWeight.Bolder },
                    new AdaptiveTextBlock($"{ data.Question }") { Id = "Question" },
                    new AdaptiveTextInput() { Id = "Answer", Placeholder = "Answer here..." },
                    new AdaptiveChoiceSetInput()
                    {
                        Type = AdaptiveChoiceSetInput.TypeName,
                        Id = "Choices",
                        IsMultiSelect = bool.Parse(data.MultiSelect),
                        Choices = new List<AdaptiveChoice>()
                        {
                            new AdaptiveChoice() { Title = data.Option1, Value = data.Option1 },
                            new AdaptiveChoice() { Title = data.Option2, Value = data.Option2 },
                            new AdaptiveChoice() { Title = data.Option3, Value = data.Option3 },
                        },
                    },
                },
                Actions = new List<AdaptiveAction>()
                {
                    new AdaptiveSubmitAction
                    {
                        Type = AdaptiveSubmitAction.TypeName,
                        Title = "Submit",
                        Data = new JObject { { "submitLocation", "messagingExtensionSubmit" } },
                    },
                },
            };
        }
    }
}
