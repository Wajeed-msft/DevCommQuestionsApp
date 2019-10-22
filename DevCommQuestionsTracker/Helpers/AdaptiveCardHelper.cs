// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using AdaptiveCards;
using DevCommQuestionsTracker.Helpers;
using DevCommQuestionsTracker.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Microsoft.DevCommQuestionsTracker
{
    public static class AdaptiveCardHelper
    {
        //public static SubmitExampleData ToSubmitExampleData(this MessagingExtensionAction action)
        //{
        //    var activityPreview = action.BotActivityPreview[0];
        //    var attachmentContent = activityPreview.Attachments[0].Content;
        //    var previewedCard = JsonConvert.DeserializeObject<AdaptiveCard>(attachmentContent.ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //    string userText = (previewedCard.Body[1] as AdaptiveTextBlock).Text;
        //    var choiceSet = previewedCard.Body[3] as AdaptiveChoiceSetInput;

        //    return new SubmitExampleData()
        //    {
        //        Question = userText,
        //        MultiSelect = choiceSet.IsMultiSelect ? "true" : "false",
        //        Option1 = choiceSet.Choices[0].Title,
        //        Option2 = choiceSet.Choices[1].Title,
        //        Option3 = choiceSet.Choices[2].Title,
        //    };
        //}

        public static MessagingExtensionActionResponse CreateTaskModuleAdaptiveCardResponse(ITurnContext<IInvokeActivity> turnContext)// //Question question
        {
            //var existingQuestion = question != null; 
            var valuePayload = JsonConvert.DeserializeObject<Value>(turnContext.Activity.Value.ToString());
            var payload = JsonConvert.DeserializeObject<BodyContent>(valuePayload.messagePayload.attachments[0].content.ToString());

            var title = System.Text.RegularExpressions.Regex.Replace(payload.sections[0].title, "<.*?>", string.Empty); ;
            var forum = valuePayload.messagePayload.from.application.displayName.Replace(" ", "");
            var postedDate = valuePayload.messagePayload.createdDateTime.ToShortDateString();


            var module = "Bots,Tabs,Adaptive Card,Connector,Message Extension,Webhooks,Teams client,Teams feature issue,Third party app,Teams feature support,Third Party Apps,Teams feature request,Calling and Meeting,Trello,TeamsUI,Teams Framework,User Presence API, Authentication,GraphAPI,VSTS Apps,Actionable messages,Activity Feed,App Distribution,App Store, App Studio, Coortana Integration,DeepLink,Doc-Bug,Doc-Suggestion,Microsoft Apps,Microsoft Flow, Mobile Client, Notification Only Bots,Powershell,OtherFramework,QnA Maker,SHarePoint,TaskModule,Sideloading,Skype For business,TeamsBotSDK,TeamsJSSDK";
            var assignedTo = "Wajeed Shaikh,Gousia Begum,Trinetra Kumar,Abhijit Jodhbhavi,Subhashish Pani";
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
                questionSubTypeList.Add(new AdaptiveChoice() { Title = item.Replace("_", " "), Value = item });
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
                    new AdaptiveTextBlock(
                        "Enter the details of question and click on submit")
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
                                        Id="lblTitle",
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
                                        Value=title,
                                        Id="Title",
                                        IsMultiline=true

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
                                        Id="lblPostedDate",
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
                                       Id="PostedDate",
                                       Placeholder=" Select Posted Date",
                                       Value = postedDate
                                   }
                                }
                            }
                        }
                    },
                    //  ResolvedDate column- Add value here
                    //new AdaptiveColumnSet()
                    //{
                    //    Columns=new List<AdaptiveColumn>()
                    //    {
                    //        new AdaptiveColumn()
                    //        {
                    //            Items=new List<AdaptiveElement>()
                    //            {
                    //                new AdaptiveTextBlock()
                    //                {
                    //                    Text="Resolved Date",
                    //                    Id="lblResolveddate",
                    //                    Spacing=AdaptiveSpacing.None,
                    //                    Wrap=true
                    //                }
                    //            }
                    //        },
                    //        new AdaptiveColumn()
                    //        {
                    //            Items=new List<AdaptiveElement>()
                    //            {
                    //                new AdaptiveDateInput()
                    //                {
                    //                   Id="ResolvedDate",
                    //                   Placeholder=" Select Resolved Date",

                    //                }
                    //            }
                    //        }
                    //    }
                    //},
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
                                        Id="lblQuestionType",
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
                                        Id="QuestionType",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(questionTypeList),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                        // Value = question?.Type.ToString()
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
                                        Id="lblQuestionSubType",
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
                                        Id="QuestionSubType",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(questionSubTypeList),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                        // Value = question?.SubType.ToString()
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
                                        Id="lblForum",
                                        Spacing=AdaptiveSpacing.None,
                                        Wrap=true,

                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items=new List<AdaptiveElement>()
                                {
                                    new AdaptiveChoiceSetInput()
                                    {
                                        Id="Forum",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(forumChoices),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                        Value = forum
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
                                        Id="lblStatus",
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
                                        Id="Status",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(StatusChoices),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                        // Value = existingQuestion? question?.Type.ToString() : Status.TBD.ToString()

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
                                        Id="lblModule",
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
                                        Id="Module",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(moduleList),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                        // Value = question?.Module
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
                                        Id="lblAssignedTo",
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
                                        Id="AssignedTo",
                                        Spacing=AdaptiveSpacing.None,
                                        Choices=new List<AdaptiveChoice>(assignedToList),
                                        Style=AdaptiveChoiceInputStyle.Compact,
                                        // Value = question?.AssignedTo.ToString()
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
                                        Id="lblComment",
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
                                        Placeholder="Enter comments",
                                        Id="Comments",
                                        IsMultiline=true,
                                        // Value = question?.Comment.ToString()
                                    }
                                }
                            }
                        }
                    }
                },
                Actions = new List<AdaptiveAction>()
                {
                    new AdaptiveSubmitAction
                    {
                        Type = AdaptiveSubmitAction.TypeName,
                        Title = "Submit",
                        Data = new JObject { { "messageId", turnContext.Activity.Conversation.Id } },
                    },
                },
            }.ToTaskModuleResponse();
        }

        //public static AdaptiveCard ToAdaptiveCard(this SubmitExampleData data)
        //{
        //    return new AdaptiveCard()
        //    {
        //        Body = new List<AdaptiveElement>()
        //        {
        //            new AdaptiveTextBlock("Adaptive Card from Task Module") { Weight = AdaptiveTextWeight.Bolder },
        //            new AdaptiveTextBlock($"{ data.Question }") { Id = "Question" },
        //            new AdaptiveTextInput() { Id = "Answer", Placeholder = "Answer here..." },
        //            new AdaptiveChoiceSetInput()
        //            {
        //                Type = AdaptiveChoiceSetInput.TypeName,
        //                Id = "Choices",
        //                IsMultiSelect = bool.Parse(data.MultiSelect),
        //                Choices = new List<AdaptiveChoice>()
        //                {
        //                    new AdaptiveChoice() { Title = data.Option1, Value = data.Option1 },
        //                    new AdaptiveChoice() { Title = data.Option2, Value = data.Option2 },
        //                    new AdaptiveChoice() { Title = data.Option3, Value = data.Option3 },
        //                },
        //            },
        //        },
        //        Actions = new List<AdaptiveAction>()
        //        {
        //            new AdaptiveSubmitAction
        //            {
        //                Type = AdaptiveSubmitAction.TypeName,
        //                Title = "Submit",
        //                Data = new JObject { { "submitLocation", "messagingExtensionSubmit" } },
        //            },
        //        },
        //    };
        //}
    }
}
