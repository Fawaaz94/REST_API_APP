using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SwissU.Configuration;
using SwissU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Controls;

namespace SwissU.ViewModels
{
    class updateCategoriesViewModel
    {
        public static RestClient Rclient = new RestClient();


        public void ExecuteUpdate(string ticket, string attributeID, string changeValue, TextBox txtResponse, string catAttrID, string endpoint, TextBlock countLabel)
        {
            int id = 0;

            // Setting the URL with for the search method with its endpoint
            Rclient.BaseUrl = new Uri(string.Format("{0}/api/v2/search", endpoint));

            // Setting the request type and adding the respective headers and parameters
            var request = new RestRequest(Method.POST).
                AddParameter("where", attributeID);
            request.AddHeader("otcsticket", ticket);

            string response = Rclient.Execute(request).Content;

            Poco item = JsonConvert.DeserializeObject<Poco>(response);

            countLabel.Text = (item.Results.Count).ToString();

            int i = 0;

            foreach (var obj in item.Results)
            {
                // Getting the id from the seach results and setting it to the variable idS
                id = obj.Data.properties.Id;

                // Inserting the id value to be used for changing the category attribute
                InsertAttributeValues(ticket, id, changeValue, txtResponse, catAttrID, ++i, endpoint);
            }

        }// EOM


        /// <summary>
        /// This method will insert the values you want into a particular categories attribute
        /// </summary>
        public void InsertAttributeValues(string ticket, int id, string changeValue, TextBox txtResponse, string attributeID, int i, string endpoint)
        {
            // Setting the endpoint for this method
            Rclient.BaseUrl = new Uri(string.Format("{0}/api/v2/nodes/{1}/categories/{2}", endpoint, id, splitAttributeID(attributeID)));

            // Creating a string for the data that should be sent as payload to the enpoint
            //string send = string.Format("{" + @"""2608_2""" + ":" + @"""{0}""" + "}", changeValue);
            string send = "{" + string.Format(@"""{0}""", attributeID) + ":" + string.Format(@"""{0}""", changeValue) + "}";

            // Creating a request as a PUT method and sending it through with a body parameter
            var request = new RestRequest(Method.PUT).
                AddParameter("body", send);
            request.AddHeader("otcsticket", ticket);

            // Executing the response
            var response = Rclient.Execute(request);
            var statusCode = response.StatusCode;

            txtResponse.Text += string.Format("{0}: ", i);
            txtResponse.Text += statusCode;
            txtResponse.Text += System.Environment.NewLine;

        }// EOM


        /// <summary>
        /// This method will split the cat-attr-ID so that a cat-ID is returned
        /// </summary>
        public string splitAttributeID(string catAttrID)
        {
            string[] final = catAttrID.Split('_');
            return final[0];
        }// EOM


        public void attributeCount(string ticket, string attributeID, string endpoint, TextBlock countLabel)
        {
            // Setting the URL with for the search method with its endpoint
            Rclient.BaseUrl = new Uri(string.Format("{0}/api/v2/search", endpoint));

            // Setting the request type and adding the respective headers and parameters
            var request = new RestRequest(Method.POST).
                AddParameter("where", attributeID);
            request.AddHeader("otcsticket", ticket);

            string response = Rclient.Execute(request).Content;

            Poco item = JsonConvert.DeserializeObject<Poco>(response);

            countLabel.Text = (item.Results.Count).ToString();

        }// EOM


        public List<string> GetAttributeValues(int id)
        {
            List<string> attributeValues = new List<string>();

            Rclient.BaseUrl = new Uri($"{Config.endpoint}/api/v2/nodes/{id}/categories");
            var request = new RestRequest(Method.GET).AddHeader("otcsticket", LoginViewModel.ticket);

            //var response = Rclient.Execute<PocoResult>(request);
            var response = Rclient.Execute<PocoResult>(request);


            var test = Json.Decode(response.Content);

            DynamicJsonArray test2 = test.results;

            
            var list = new List<Object>();
            
            foreach (var index in test2)
            {
                list.Add(index);
            }

            

            Console.WriteLine(test.data.categories);
         
            return attributeValues;
        }



    }
}
