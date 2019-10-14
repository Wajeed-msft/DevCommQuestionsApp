// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using AdaptiveCards;
using DevCommQuestionsTracker.Models;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using static DevCommQuestionsTracker.Helpers.Common;

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

        public static MessagingExtensionActionResponse CreateTaskModuleAdaptiveCardResponse(ITurnContext<IInvokeActivity> turnContext,string userText = null, bool isMultiSelect = true, string option1 = null, string option2 = null, string option3 = null)
        {
            var details = JsonConvert.DeserializeObject<Value>(turnContext.Activity.Value.ToString());
            //var tilte = JsonConvert.DeserializeObject<Content>(details.messagePayload.body.content.ToString());
            var module = "Bots,Tabs,Adaptive Card,Connector,Message Extension,Webhooks,Teams client,Teams feature issue,Third party app,Teams feature support,Third Party Apps,Teams feature request,Calling and Meeting,Trello,TeamsUI,Teams Framework,User Presence API, Authentication,GraphAPI,VSTS Apps,Actionable messages,Activity Feed,App Distribution,App Store, App Studio, Coortana Integration,DeepLink,Doc-Bug,Doc-Suggestion,Microsoft Apps,Microsoft Flow, Mobile Client, Notification Only Bots,Powershell,OtherFramework,QnA Maker,SHarePoint,TaskModule,Sideloading,Skype For business,TeamsBotSDK,TeamsJSSDK";
            var assignedTo = "Wajeed Shaikh (Bangalore),Gousia Begum(Bangalore),Trinetra Kumar(Bangalore),Abhijit Jodhbhavi(Bangalore),Subhashish Pani(Bangalore)";
            var questiontype = "Development,TeamsProduct,MicrosoftBuildApps";
            var questionsubtype = "Bug,FeatureAsk,DocumentationGap,Support_Investigation,DocumentationLinkProvided";
            var moduleList = new List<AdaptiveChoice>();
            var assignedToList = new List<AdaptiveChoice>();
            var questionTypeList = new List<AdaptiveChoice>();
            var questionSubTypeList = new List<AdaptiveChoice>();
            foreach (var item in module.Split(","))
            {
                moduleList.Add(new AdaptiveChoice() { Title = item, Value = item });
            }
            foreach (var item in assignedTo.Split(","))
            {
                assignedToList.Add(new AdaptiveChoice() { Title = item, Value = item });
            }
            foreach (var item in questiontype.Split(","))
            {
                questionTypeList.Add(new AdaptiveChoice() { Title = item, Value = item });
            }
            foreach (var item in questionsubtype.Split(","))
            {
                questionSubTypeList.Add(new AdaptiveChoice() { Title = item, Value = item });
            }
            var forumChoices = new List<AdaptiveChoice>();
            forumChoices.Add(new AdaptiveChoice() { Title = Forum.Email.ToString(), Value = Forum.Email.ToString() });
            forumChoices.Add(new AdaptiveChoice() { Title = Forum.Github.ToString(), Value = Forum.Github.ToString() });
            forumChoices.Add(new AdaptiveChoice() { Title = Forum.StackOverflow.ToString(), Value = Forum.StackOverflow.ToString() });
            forumChoices.Add(new AdaptiveChoice() { Title = Forum.TechCommunity.ToString(), Value = Forum.TechCommunity.ToString() });
            var StatusChoices = new List<AdaptiveChoice>();
            StatusChoices.Add(new AdaptiveChoice() { Title = Status.TBD.ToString(), Value = Status.TBD.ToString() });
            StatusChoices.Add(new AdaptiveChoice() { Title = Status.Done.ToString(), Value = Status.Done.ToString() });
            StatusChoices.Add(new AdaptiveChoice() { Title = Status.InProgress.ToString(), Value = Status.InProgress.ToString() });
            StatusChoices.Add(new AdaptiveChoice() { Title = Status.WaitingForEngineering.ToString(), Value = Status.WaitingForEngineering.ToString() });
            StatusChoices.Add(new AdaptiveChoice() { Title = Status.WaitingForUser.ToString(), Value = Status.WaitingForUser.ToString() });

            return new AdaptiveCard()
            {
                Body = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock("Enter the details of question and click on submit")
                    {
                        Weight = AdaptiveTextWeight.Bolder,
                    },
                    //Question title column
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Question Title",
                                        Id="title",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="turnCont",
                                        Id="txtplc",
                                        MaxLines=3
                                    }
                                }
                            }
                        }
                    },
                    //Posted Date- Add value here
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Posted Date",
                                        Id="posteddate",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                   new AdaptiveDateInput()
                                   {
                                       Id="postedDateplc",
                                       Placeholder=" Select Date",

                                   }
                                }
                            }
                        }
                    },
                     //ResolvedDate column- Add value here
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Resolved Date",
                                        Id="resolveddate",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveDateInput()
                                    {
                                       Id="resolvedDateplc",
                                       Placeholder=" Select Date",

                                    }
                                }
                            }
                        }
                    },
                    //Type Column- Add QuestionType list here
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Question Type",
                                        Id="type",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="types",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(questionTypeList),
                                        Style=AdaptiveChoiceInputStyle.Compact
                                    }
                                }
                            }
                        }
                    },
                    //SubType columm- Add Subtype list values.
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Question Subtype",
                                        Id="subtype",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="types",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(questionSubTypeList),
                                        Style=AdaptiveChoiceInputStyle.Compact
                                    }
                                }
                            }
                        }
                    },
                   //Forum column
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Forum",
                                        Id="forum",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="foruminput",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(forumChoices),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                    }
                                }
                            }
                        }
                    },
                    //Status Column
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Status",
                                        Id="status",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="statusinput",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(StatusChoices),
                                        Style=AdaptiveChoiceInputStyle.Compact
                                        
                                    }
                                }
                            }
                        }
                    },
                    //Module column- Pick from appsettings.json
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Module",
                                        Id="module",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="moduleinput",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(moduleList),
                                        Style=AdaptiveChoiceInputStyle.Compact
                                    }
                                }
                            }
                        }
                    },
                    //AssignedTo column
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Assigned To",
                                        Id="assignedto",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="assignedto",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(assignedToList),
                                        Style=AdaptiveChoiceInputStyle.Compact
                                    }
                                }
                            }
                        }
                    },
                    //Comment column,
                    new AdaptiveColumnSet()
                    {
                        Columns=new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text="Comments",
                                        Id="comment",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextInput()
                                    {
                                        Placeholder="Eter comments",
                                        Id="commentsplc",
                                        IsMultiline=true

                                    }
                                }
                            }
                        }
                    }
                   

                    //new AdaptiveTextBlock("Enter text for Question:"),
                    //new AdaptiveTextInput() { Id = "Question", Placeholder = "Question text here", Value = userText },
                    //new AdaptiveTextBlock("Options for Question:"),
                    //new AdaptiveTextBlock("Is Multi-Select:"),
                    //new AdaptiveChoiceSetInput()
                    //{
                    //    Type = AdaptiveChoiceSetInput.TypeName,
                    //    Id = "MultiSelect",
                    //    Value = isMultiSelect ? "true" : "false",
                    //    IsMultiSelect = false,
                    //    Choices = new List<AdaptiveChoice>()
                    //    {
                    //        new AdaptiveChoice() { Title = "True", Value = "true" },
                    //        new AdaptiveChoice() { Title = "False", Value = "false" },
                    //    },
                    //},
                    //new AdaptiveTextInput() { Id = "Option1", Placeholder = "Option 1 here", Value = option1 },
                    //new AdaptiveTextInput() { Id = "Option2", Placeholder = "Option 2 here", Value = option2 },
                    //new AdaptiveTextInput() { Id = "Option3", Placeholder = "Option 3 here", Value = option3 },
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
