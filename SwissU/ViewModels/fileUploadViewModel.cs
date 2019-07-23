using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using SwissU.Configuration;
using SwissU.Extention_Code;
using SwissU.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Threading;

namespace SwissU.ViewModels
{
    public class fileUploadViewModel
    {
        // Creating a RESTSharp Client
        public static RestClient Rclient = new RestClient();
        GlobalConfig gConfig = new GlobalConfig();

        public string _ticket { get; set; }

        public int SuccessCount { get; set; } = 0;

        public int ErrorCount { get; set; } = 0;

        public fileUploadViewModel()
        {
           
        }// EOC


        public fileUploadViewModel(string ticket)
        {
            _ticket = ticket;
        }// EOC


        // BULK UPLOAD
        public List<int> BulkUpload(string excelFile, List<string> resultList)
        {

            //this.Dispatcher.Invoke
            var collection = ExcelReader.getExcelFile(excelFile);

            List<int> resultsCounter = new List<int>();
        
        
            // Need to find a way to catch the row that doesnt upload so we can save it in a list and do a retry
            foreach (var item in collection)
            {
                try
                {
                    resultList.Add(UploadFile(search(LoginViewModel.ticket, Config.endpoint, item.EmpID), LoginViewModel.ticket, Config.endpoint, item.Document));

                }
                catch (Exception ex)
                {
                }
            }

            foreach (var item in resultList)
            {
                if (item == "OK")
                    ++SuccessCount;
                else
                    ++ErrorCount;
            }

            resultsCounter.Add(SuccessCount);
            resultsCounter.Add(ErrorCount);

            return resultsCounter;
        }// EOM


        // SINGLE FILE UPLOAD
        public string SingleUpload(string searchValue, string docPath)
        {
            string result = string.Empty;

            UploadFile(search(LoginViewModel.ticket, Config.endpoint, searchValue), LoginViewModel.ticket, Config.endpoint, docPath);

            return result;
        }// EOM


        #region UPLOAD
        private string UploadFile(int id, string ticket, string endPoint, string docPath)
        {
            string CompleteOutput = string.Empty;

            Rclient.BaseUrl = new Uri(string.Format("{0}/api/v2/nodes", endPoint));

            #region FILE UPLOAD SECTION
            string path = docPath;
            string fileName = Path.GetFileName(path);
            byte[] bytes;

            using (var fs = File.OpenRead(path))
            {
                using (var ms = new MemoryStream())
                {
                    ms.SetLength(fs.Length);
                    fs.CopyTo(ms);
                    bytes = ms.ToArray();
                }
            }
            #endregion

            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true,
                Files = { FileParameter.Create("file", bytes, fileName) }
            };

            request.AddHeader("otcsticket", ticket);

            request.AddParameter("type", 144);
            request.AddParameter("parent_id", id);
            request.AddParameter("name", fileName);

            var response = Rclient.Execute(request);

            // This will deserialize the received JSON in a Dictionary - key value pair
            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            string result = response.StatusCode.ToString();

            //return CompleteOutput = output["results"];
            return result;
        }// EOM

        #endregion


        #region SEARCH
        // This search methiod will find the folder specified and return its ID
        public static int search(string ticket, string endPoint, string Searchvalue)
        {
            int id = 0;

            Rclient.BaseUrl = new Uri(string.Format("{0}/api/v2/search", endPoint));

            // Setting the request type and adding the respective headers and parameters
            var request = new RestRequest(Method.POST).
                AddParameter("where", Searchvalue);
            request.AddHeader("otcsticket", ticket);

            var response = Rclient.Execute<Poco>(request);
           
            //var response = Rclient.Execute(request);

            //Poco item = JsonConvert.DeserializeObject<Poco>(response);

            foreach (var obj in response.Data.Results)
            {
                id = obj.Data.properties.Id;
            }

            return id;
        }// EOM
        #endregion

    }// EOCLASS
}
