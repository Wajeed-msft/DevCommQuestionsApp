using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCommQuestionsTracker.Helpers
{
    public class Common
    {

        public class Rootobject
        {
            public string type { get; set; }
            public string id { get; set; }
            public DateTime timestamp { get; set; }
            public DateTime localTimestamp { get; set; }
            public object localTimezone { get; set; }
            public string serviceUrl { get; set; }
            public string channelId { get; set; }
            public From from { get; set; }
            public Conversation conversation { get; set; }
            public Recipient recipient { get; set; }
            public object textFormat { get; set; }
            public object attachmentLayout { get; set; }
            public object membersAdded { get; set; }
            public object membersRemoved { get; set; }
            public object reactionsAdded { get; set; }
            public object reactionsRemoved { get; set; }
            public object topicName { get; set; }
            public object historyDisclosed { get; set; }
            public string locale { get; set; }
            public object text { get; set; }
            public object speak { get; set; }
            public object inputHint { get; set; }
            public object summary { get; set; }
            public object suggestedActions { get; set; }
            public object attachments { get; set; }
            public Entity[] entities { get; set; }
            public Channeldata channelData { get; set; }
            public object action { get; set; }
            public object replyToId { get; set; }
            public object label { get; set; }
            public object valueType { get; set; }
            public Value value { get; set; }
            public string name { get; set; }
            public object relatesTo { get; set; }
            public object code { get; set; }
            public object expiration { get; set; }
            public object importance { get; set; }
            public object deliveryMode { get; set; }
            public object listenFor { get; set; }
            public object textHighlights { get; set; }
            public object semanticAction { get; set; }
            public object callerId { get; set; }
        }

        public class From
        {
            public string id { get; set; }
            public string name { get; set; }
            public string aadObjectId { get; set; }
            public object role { get; set; }
        }

        public class Conversation
        {
            public bool isGroup { get; set; }
            public string conversationType { get; set; }
            public string id { get; set; }
            public object name { get; set; }
            public object aadObjectId { get; set; }
            public object role { get; set; }
            public string tenantId { get; set; }
        }

        public class Recipient
        {
            public string id { get; set; }
            public string name { get; set; }
            public object aadObjectId { get; set; }
            public object role { get; set; }
        }

        public class Channeldata
        {
            public Channel channel { get; set; }
            public Team team { get; set; }
            public Tenant tenant { get; set; }
            public Source source { get; set; }
        }

        public class Channel
        {
            public string id { get; set; }
        }

        public class Team
        {
            public string id { get; set; }
        }

        public class Tenant
        {
            public string id { get; set; }
        }

        public class Source
        {
            public string name { get; set; }
        }

        public class Value
        {
            public string commandId { get; set; }
            public string commandContext { get; set; }
            public Messagepayload messagePayload { get; set; }
            public Context context { get; set; }
        }

        public class Messagepayload
        {
            public string id { get; set; }
            public object replyToId { get; set; }
            public DateTime createdDateTime { get; set; }
            public object lastModifiedDateTime { get; set; }
            public bool deleted { get; set; }
            public object summary { get; set; }
            public string importance { get; set; }
            public string locale { get; set; }
            public Body body { get; set; }
            public From1 from { get; set; }
            public object[] reactions { get; set; }
            public object[] mentions { get; set; }
            public Attachment[] attachments { get; set; }
        }

        public class Body
        {
            public string contentType { get; set; }
            public string content { get; set; }
        }

        public class From1
        {
            public object device { get; set; }
            public object conversation { get; set; }
            public object user { get; set; }
            public Application application { get; set; }
        }

        public class Application
        {
            public string applicationIdentityType { get; set; }
            public string id { get; set; }
            public string displayName { get; set; }
        }

        public class Attachment
        {
            public string id { get; set; }
            public string contentType { get; set; }
            public object contentUrl { get; set; }
            public object name { get; set; }
            public object thumbnailUrl { get; set; }
            public string content { get; set; }
        }

        public class Context
        {
            public string theme { get; set; }
        }

        public class Entity
        {
            public string type { get; set; }
            public string locale { get; set; }
            public string country { get; set; }
            public string platform { get; set; }
        }

        public class Content
        {
            public string summary { get; set; }
            public string text { get; set; }
            public Section[] sections { get; set; }
        }

        public class Section
        {
            public string text { get; set; }
            public Fact[] facts { get; set; }
            public string title { get; set; }
            public string activityTitle { get; set; }
            public string activitySubtitle { get; set; }
            public string activityText { get; set; }
            public string activityImage { get; set; }
            public Potentialaction[] potentialAction { get; set; }
            public bool markdown { get; set; }
            public bool startGroup { get; set; }
        }

        public class Fact
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public class Potentialaction
        {
            public Input[] inputs { get; set; }
            public Action[] actions { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public bool isPrimaryAction { get; set; }
            public bool hideCardOnInvoke { get; set; }
            public object target { get; set; }
            public string body { get; set; }
        }

        public class Input
        {
            public bool isMultiline { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public bool isRequired { get; set; }
        }

        public class Action
        {
            public bool hideCardOnInvoke { get; set; }
            public string target { get; set; }
            public string body { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public bool isPrimaryAction { get; set; }
        }

    }
}
