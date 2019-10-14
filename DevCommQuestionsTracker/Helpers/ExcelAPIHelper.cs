//Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.

using DevCommQuestionsTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DevCommQuestionsTracker.Helpers
{
    public class ExcelApiHelper
    {
        // private static string restURLBase = "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/drive/items/";
        // private static string fileId = null;

        //public static async Task LoadWorkbook(string accessToken)
        //{
        //    try
        //    {
        //        var fileName = "Backlog-TechnicalQueries.xlsx";
        //        var serviceEndpoint = "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/root/children/";

        //        String absPath = Directory.GetCurrentDirectory() + "Assets/Backlog-TechnicalQueriess.xlsx";
        //        //String absPath = HttpContext.Current.Server.MapPath("Assets/Backlog-TechnicalQueriess.xlsx");
        //        HttpClient client = new HttpClient();
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


        //        var filesResponse = await client.GetAsync(serviceEndpoint + "?$select=name,id");

        //        if (filesResponse.IsSuccessStatusCode)
        //        {
        //            var filesContent = await filesResponse.Content.ReadAsStringAsync();

        //            JObject parsedResult = JObject.Parse(filesContent);

        //            foreach (JObject file in parsedResult["value"])
        //            {

        //                var name = (string)file["name"];
        //                if (name.Contains("Backlog-TechnicalQueries.xlsx"))
        //                {
        //                    fileId = (string)file["id"];
        //                    restURLBase = "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/items/" + fileId + "/workbook/worksheets('Current')/";
        //                    return;
        //                }
        //            }

        //        }



        //    //    // We know that the file doesn't exist, so upload it and create the necessary worksheets, tables, and chart.
        //    //    var excelFile = File.OpenRead(absPath);
        //    //    byte[] contents = new byte[excelFile.Length];
        //    //    excelFile.Read(contents, 0, (int)excelFile.Length);
        //    //    excelFile.Close();
        //    //    var contentStream = new MemoryStream(contents);


        //        //    var contentPostBody = new StreamContent(contentStream);
        //        //    contentPostBody.Headers.Add("Content-Type", "application/octet-stream");


        //        //    // Endpoint for content in an existing file.
        //        //    var fileEndpoint = new Uri(serviceEndpoint + "/" + fileName + "/content");

        //        //    var requestMessage = new HttpRequestMessage(HttpMethod.Put, fileEndpoint)
        //        //    {
        //        //        Content = contentPostBody
        //        //    };

        //        //    HttpResponseMessage response = await client.SendAsync(requestMessage);

        //        //    if (response.IsSuccessStatusCode)
        //        //    {
        //        //        //Get the Id of the new file.
        //        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        //        var parsedResponse = JObject.Parse(responseContent);
        //        //        fileId = (string)parsedResponse["id"];
        //        //        restURLBase = "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/items/" + fileId + "/workbook/worksheets('Backlog-TechnicalQueries')/";

        //        //        //Set up workbook and worksheet endpoints
        //        //        var workbookEndpoint = "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/items/" + fileId + "/workbook";
        //        //        var worksheetsEndpoint = workbookEndpoint + "/worksheets";

        //        //        //Get session id and add it to the HttpClient's default headers. This will make the changes appear more quickly.
        //        //        var sessionJson = "{" +
        //        //            "'saveChanges': true" +
        //        //            "}";
        //        //        var sessionContentPostbody = new StringContent(sessionJson);
        //        //        sessionContentPostbody.Headers.Clear();
        //        //        sessionContentPostbody.Headers.Add("Content-Type", "application/json");
        //        //        var sessionResponseMessage = await client.PostAsync(workbookEndpoint + "/createsession", sessionContentPostbody);
        //        //        var sessionResponseContent = await sessionResponseMessage.Content.ReadAsStringAsync();
        //        //        JObject sessionObject = JObject.Parse(sessionResponseContent);
        //        //        var sessionId = (string)sessionObject["id"];

        //        //        client.DefaultRequestHeaders.Add("Workbook-Session-Id", sessionId);

        //        //        //Add ToDoList worksheet to the workbook
        //        //        await AddWorksheetToWorkbook("Backlog-TechnicalQueries", worksheetsEndpoint, client);

        //        //        ////Add Summary worksheet to the workbook
        //        //        //await AddWorksheetToWorkbook("Summary", worksheetsEndpoint, client);

        //        //        //Add table to ToDoList worksheet
        //        //        await AddTableToWorksheet("Backlog-TechnicalQueries", "A1:I1", worksheetsEndpoint, client);

        //        //        ////Add table too Summary worksheet
        //        //        //await AddTableToWorksheet("Summary", "A1:B1", worksheetsEndpoint, client);

        //        //        var patchMethod = new HttpMethod("PATCH");

        //        //        //Rename Table1 in ToDoList worksheet to "ToDoList"
        //        //        var toDoListTableNameJson = "{" +
        //        //                "'name': 'Backlog-TechnicalQueries'," +
        //        //                "}";

        //        //        var toDoListTableNamePatchBody = new StringContent(toDoListTableNameJson);
        //        //        toDoListTableNamePatchBody.Headers.Clear();
        //        //        toDoListTableNamePatchBody.Headers.Add("Content-Type", "application/json");
        //        //        var toDoListRequestMessage = new HttpRequestMessage(patchMethod, worksheetsEndpoint + "('ToDoList')/tables('Table1')") { Content = toDoListTableNamePatchBody };
        //        //        var toDoListTableNameResponseMessage = await client.SendAsync(toDoListRequestMessage);


        //        //        //Rename ToDoList columns 1-9

        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "Id", "1", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "Question", "2", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "PercentComplete", "3", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "Forum", "4", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "AssignedTo", "5", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "Module", "6", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "PostedDate", "7", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "ResolvedDate", "8", worksheetsEndpoint, client);
        //        //        await RenameColumn("Backlog-TechnicalQueries", "Backlog-TechnicalQueries", "Comments", "9", worksheetsEndpoint, client);

        //        //        var dateRangeJSON = "{" +
        //        //            "'numberFormat': '@'" +
        //        //            "}";
        //        //        var datePatchBody = new StringContent(dateRangeJSON);
        //        //        datePatchBody.Headers.Clear();
        //        //        datePatchBody.Headers.Add("Content-Type", "application/json");
        //        //        var dateRequestMessage = new HttpRequestMessage(patchMethod, worksheetsEndpoint + "('Backlog-TechnicalQueries')/range(address='$F1:$G1000')") { Content = datePatchBody };
        //        //        var dateResponseMessage = await client.SendAsync(dateRequestMessage);

        //        //        var closeSessionJson = "{}";
        //        //        var closeSessionBody = new StringContent(closeSessionJson);
        //        //        sessionContentPostbody.Headers.Clear();
        //        //        sessionContentPostbody.Headers.Add("Content-Type", "application/json");
        //        //        var closeSessionResponseMessage = await client.PostAsync(workbookEndpoint + "/closesession", closeSessionBody);

        //        //    }

        //        //    else
        //        //    {
        //        //        //Handle exception

        //        //    }

        //        //}

        //    catch (Exception e)
        //    {
        //        //Handle exception

        //    }
        //}


        //private static async Task AddWorksheetToWorkbook(string worksheetName, string worksheetsEndpoint, HttpClient client)
        //{
        //    var worksheetJson = "{" +
        //                    "'name': '" + worksheetName + "'," +
        //                    "}";

        //    var worksheetContentPostBody = new StringContent(worksheetJson);
        //    worksheetContentPostBody.Headers.Clear();
        //    worksheetContentPostBody.Headers.Add("Content-Type", "application/json");
        //    var worksheetResponseMessage = await client.PostAsync(worksheetsEndpoint, worksheetContentPostBody);
        //}

        //private static async Task AddTableToWorksheet(string worksheetName, string tableRange, string worksheetsEndpoint, HttpClient client)
        //{
        //    var tableJson = "{" +
        //            "'address': '" + tableRange + "'," +
        //            "'hasHeaders': true" +
        //            "}";

        //    var tableContentPostBody = new StringContent(tableJson);
        //    tableContentPostBody.Headers.Clear();
        //    tableContentPostBody.Headers.Add("Content-Type", "application/json");
        //    var tableResponseMessage = await client.PostAsync(worksheetsEndpoint + "('" + worksheetName + "')/tables/$/add", tableContentPostBody);

        //}

        //private static async Task RenameColumn(string worksheetName, string tableName, string colName, string colNumber, string worksheetsEndpoint, HttpClient client)
        //{
        //    var patchMethod = new HttpMethod("PATCH");
        //    var colNameJson = "{" +
        //            "'values': [['" + colName + "'], [null]] " +
        //            "}";

        //    var colNamePatchBody = new StringContent(colNameJson);
        //    colNamePatchBody.Headers.Clear();
        //    colNamePatchBody.Headers.Add("Content-Type", "application/json");
        //    var colNameRequestMessage = new HttpRequestMessage(patchMethod, worksheetsEndpoint + "('" + worksheetName + "')/tables('" + tableName + "')/Columns('" + colNumber + "')") { Content = colNamePatchBody };
        //    var colNameResponseMessage = await client.SendAsync(colNameRequestMessage);

        //}

        //private static async Task AddRowToTable(string worksheetName, string tableName, string rowName, string worksheetsEndpoint, HttpClient client)
        //{
        //    var summaryTableRowJson = "{" +
        //            "'values': [['" + rowName + "', '=COUNTIF(ToDoList[PercentComplete],[@Module])']]" +
        //        "}";
        //    var summaryTableRowContentPostBody = new StringContent(summaryTableRowJson, System.Text.Encoding.UTF8);
        //    summaryTableRowContentPostBody.Headers.Clear();
        //    summaryTableRowContentPostBody.Headers.Add("Content-Type", "application/json");
        //    var summaryTableRowResponseMessage = await client.PostAsync(worksheetsEndpoint + "('" + worksheetName + "')/tables('" + tableName + "')/rows", summaryTableRowContentPostBody);
        //}

        public static async Task<string> GetFilePath(string accessToken)
        {
            var serviceEndpoint = "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/root/children/";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var filesResponse = await client.GetAsync(serviceEndpoint + "?$select=name,id");

            if (filesResponse.IsSuccessStatusCode)
            {
                var filesContent = await filesResponse.Content.ReadAsStringAsync();

                JObject parsedResult = JObject.Parse(filesContent);

                foreach (JObject file in parsedResult["value"])
                {

                    var name = (string)file["name"];
                    if (name.Contains("Backlog-TechnicalQueries.xlsx"))
                    {
                        var fileId = (string)file["id"];
                        return  "https://graph.microsoft.com/v1.0/sites/3212a3eb-05f8-4d5a-a459-df30d0cca4c3/drive/items/" + fileId + "/workbook/worksheets('Current')/";
                        
                    }
                }

            }
            return null;


        }


        public static async Task<List<Question>> GetQuestions(string accessToken)
        {
            var resourcePath = await GetFilePath(accessToken);
            List<Question> questions = new List<Question>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync(resourcePath+ "tables('Active_Questions')/Rows");
                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();

                    dynamic x = JsonConvert.DeserializeObject(resultString);
                    JArray y = x.value;

                    questions = BuildList(questions, y);
                }
            }

            return questions;
        }

        private static List<Question> BuildList(List<Question> questions, JArray y)
        {
            foreach (var item in y.Children())
            {
                var itemProperties = item.Children<JProperty>();

                //Get element that holds row collection
                var element = itemProperties.FirstOrDefault(xx => xx.Name == "values");
                JProperty index = itemProperties.FirstOrDefault(xxx => xxx.Name == "index");

                //The string array of row values
                JToken values = element.Value;

                //LINQ query to get rows from results
                var stringValues = from stringValue in values select stringValue;
                //rows
                foreach (JToken thing in stringValues)
                {
                    IEnumerable<string> rowValues = thing.Values<string>();

                    //Cast row value collection to string array
                    string[] stringArray = rowValues.Cast<string>().ToArray();


                    try
                    {
                        Question question = new Question()
                        {
                            Id = stringArray[0]
                        };
                        //     stringArray[0],
                        //     stringArray[1],
                        //     stringArray[2],
                        //     stringArray[3],
                        //     stringArray[4],
                        //     stringArray[5],
                        //     stringArray[6],
                        //stringArray[7],
                        //stringArray[8]),
                        //stringArray[9])
                        // );
                        questions.Add(question);
                    }
                    catch (FormatException)
                    {
                        //Handle exception
                    }
                }
            }

            return questions;

        }

        public static async Task<Question> CreateToDoItem(
                                                 string accessToken,
                                                 string question,
                                                 string forum,
                                                 string assignedTo,
                                                 string module,
                                                 string percentComplete,
                                                 string postedDate,
                                                 string endDate,
                                                 string comments)
        {
            Question newTodoItem = new Question();

            string id = Guid.NewGuid().ToString();

            var forumString = "";

            switch (forum)
            {
                case "1":
                    forumString = "Email";
                    break;
                case "2":
                    forumString = "Github";
                    break;
                case "3":
                    forumString = "StackOverflow";
                    break;
                case "4":
                    forumString = "TechCommunity";
                    break;
                case "5":
                    forumString = "MicroStack";
                    break;
                case "6":
                    forumString = "TeamsPost";
                    break;
            }

            var assignedToString = "";
            switch (assignedTo)
            {
                case "1":
                    assignedToString = "Wajeed";
                    break;
                case "2":
                    assignedToString = "Gousia";
                    break;
                case "3":
                    assignedToString = "Trinetra";
                    break;
                case "4":
                    assignedToString = "Abhijit";
                    break;
                case "5":
                    assignedToString = "Subhashish";
                    break;
            }

            var moduleString = "";

            switch (module)
            {
                case "1":
                    moduleString = "Bots";
                    break;
                case "2":
                    moduleString = "Tabs";
                    break;
                case "3":
                    moduleString = "Adaptive card";
                    break;
                case "4":
                    moduleString = "Connector";
                    break;
                case "5":
                    moduleString = "Message Extension";
                    break;
                case "6":
                    moduleString = "Webhook";
                    break;
                case "7":
                    moduleString = "Teams Client";
                    break;
                case "8":
                    moduleString = "Team feature issue";
                    break;
                case "9":
                    moduleString = "Third Party Apps";
                    break;
                case "10":
                    moduleString = "Teams Support";
                    break;
                case "11":
                    moduleString = "Calling and Meeting";
                    break;
                case "12":
                    moduleString = "Trello";
                    break;
                case "13":
                    moduleString = "Teams UI";
                    break;
                case "14":
                    moduleString = "Teams Framework";
                    break;
                case "15":
                    moduleString = "User Present API";
                    break;
                case "16":
                    moduleString = "AUtrhentication";
                    break;
                case "17":
                    moduleString = "Graph API";
                    break;
                case "18":
                    moduleString = "VSTS Apps";
                    break;
                case "19":
                    moduleString = "Actionable Messages";
                    break;
                case "20":
                    moduleString = "Activity Field";
                    break;
                case "21":
                    moduleString = "App Distribution";
                    break;
                case "22":
                    moduleString = "App Store";
                    break;
                case "23":
                    moduleString = "App Studio";
                    break;
                case "24":
                    moduleString = "Cortana Integration";
                    break;
                case "25":
                    moduleString = "Deep Link";
                    break;
                case "26":
                    moduleString = "Doc-Bug";
                    break;
                case "27":
                    moduleString = "Doc-Suggestion";
                    break;
                case "28":
                    moduleString = "In-progress";
                    break;
                case "29":
                    moduleString = "Activity Field";
                    break;
                case "30":
                    moduleString = "Microsoft Apps";
                    break;
                case "31":
                    moduleString = "Microsoft Flow";
                    break;
                case "32":
                    moduleString = "Mobile Client";
                    break;
                case "33":
                    moduleString = "Notification Only Bots";
                    break;
                case "34":
                    moduleString = "Powershell";
                    break;
                case "35":
                    moduleString = "QnA Maker";
                    break;
                case "36":
                    moduleString = "Sharepoint";
                    break;
                case "37":
                    moduleString = "Task Module";
                    break;
                case "38":
                    moduleString = "Sideloading";
                    break;
                case "39":
                    moduleString = "Skype for business(SFB)";
                    break;
                case "40":
                    moduleString = "Teams Bot SDK";
                    break;

            }
            using (var client = new HttpClient())
            {
                var resourcePath = await GetFilePath(accessToken);
                client.BaseAddress = new Uri(resourcePath);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var request = new HttpRequestMessage(HttpMethod.Post, resourcePath))
                {
                    //Create two-dimensional array to hold the row values to be serialized into json
                    object[,] valuesArray = new object[1, 9] { { id, question, percentComplete.ToString(), forumString, assignedToString, moduleString, postedDate, endDate, comments } };

                    //Create a container for the request body to be serialized
                    RequestBodyHelper requestBodyHelper = new RequestBodyHelper();
                    requestBodyHelper.index = null;
                    requestBodyHelper.values = valuesArray;

                    //Serialize the final request body
                    string postPayload = JsonConvert.SerializeObject(requestBodyHelper);

                    //Add the json payload to the POST request
                    request.Content = new StringContent(postPayload, System.Text.Encoding.UTF8);


                    using (HttpResponseMessage response = await client.PostAsync("tables('Active_Questions')/rows", request.Content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string resultString = await response.Content.ReadAsStringAsync();
                            dynamic x = JsonConvert.DeserializeObject(resultString);
                        }
                    }
                }
            }
            return newTodoItem;
        }

    }
    public class RequestBodyHelper
    {
        public object index;
        public object[,] values;
    }
}