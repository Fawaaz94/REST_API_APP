using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwissU.ViewModels
{
    static class LoginViewModel
    {
        public static RestClient Rclient = new RestClient();

        public static string ticket { get; set; }
        public static string ticketAsync { get; set; }

        public static string statusCode { get; set; }


        // Authenticates and returns a ticket
        public static void Authentication(string username, string password, string endPoint)
        {
            //Rclient.BaseUrl = new Uri(string.Format("{0}/api/v1/auth", endPoint));
            Rclient.BaseUrl = new Uri($"{endPoint}/api/v1/auth");

            // Sending through a basic authentication to the REST API
            Rclient.Authenticator = new SimpleAuthenticator("username", username, "password", password);

            var request = new RestRequest(Method.POST);

            // Executing the request 
            var response = Rclient.Execute(request);
 
            //Get the status Code
            statusCode = response.StatusCode.ToString();

            // Deserializing the out of the response Json 
            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            // Returns the value of the ticket key into the variable ticket
            ticket = output["ticket"];
           
        }// EOM

    }
}
