// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.DevCommQuestionsTracker
{
    public class QuestionData
    {
        public string messageId { get; set; }
        public string Title { get; set; }
        public string PostedDate { get; set; }
        public string QuestionType { get; set; }
        public string QuestionSubType { get; set; }
        public string Forum { get; set; }
        public string Status { get; set; }
        public string Module { get; set; }
        public string AssignedTo { get; set; }
        public string Comments { get; set; }
    }
}
