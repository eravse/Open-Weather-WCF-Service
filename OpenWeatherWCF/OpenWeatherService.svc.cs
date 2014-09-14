using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace OpenWeatherWCF
{
    
    public class OpenWeatherService : IOpenWeatherService
    {


        /// <summary>
        /// THIS ONLY RETURN WEATHEROBJECT.
        /// IF YOU WANT TO JSON OR XML OR HTML CHANGE RETURN TYPE...
        /// </summary>
        /// <param name="RequestType">Please Select from enum</param>
        /// <param name="Format">Default Json .xml or html.</param>
        /// <param name="UnitType">metric or imperial </param>
        /// <param name="q">ID or CityName</param>
        /// <param name="Lat">empty string</param>
        /// <param name="Long">empty string</param>
        /// <returns></returns>
        public string GetWeather(WeatherParameter Param)
        {

            var wFormat = "";
            var RType = "";
            var UType = "";



            switch (Param.Format)
            {
                case DataFormat.jSon:
                    wFormat = "mode=json";
                    break;
                case DataFormat.Xml:
                    wFormat = "mode=xml";
                    break;
                case DataFormat.Html:
                    wFormat = "mode=html";
                    break;
                default:
                    break;
            }

            switch (Param.RequestType)
            {
                case RequestType.ByCityName:
                    RType = "q=" + Param.q;
                    break;
                case RequestType.ByCoordinates:
                    RType = "lat=" + Param.Lat + "&lon=" + Param.Lon;
                    break;
                case RequestType.ByCityId:  
                    RType = "iq="+Param.q;
                    break;
                default:
                    break;
            }

            switch (Param.UnitType)
	        {
		        case UnitType.Imperial:
                            UType="units=imperial";
                            break;
                case UnitType.Metrics:
                    UType="units=metric";
                 break;
                default:
                break;
	        }

            
            var BaseUrl = "http://api.openweathermap.org/data/2.5/weather?"+wFormat+"&"+RType+"&"+UType;
            var Content = "";
       
            WebRequest request = WebRequest.Create(
             BaseUrl);
          
            request.Credentials = CredentialCache.DefaultCredentials;
         
            WebResponse response = request.GetResponse();
         
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
          
            Stream dataStream = response.GetResponseStream();
           
            StreamReader reader = new StreamReader(dataStream);
       
            string responseFromServer = reader.ReadToEnd();
           
            Content = responseFromServer;





            reader.Close();
            response.Close();

            return Content;
        }




        /// <summary>
        /// IF YOU GET WEATHER OBJECT PLEASE SET WEATHER PARAMETER FORMAT JSON
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public WeatherObject GetWeatherobject(WeatherParameter Param)
        {
            var jsonObject = GetWeather(Param);
            WeatherObject WObject = new JavaScriptSerializer().Deserialize<WeatherObject>(jsonObject);
            return WObject;
        }




        public void SaveObject(WeatherObject wObject)
        {
           
        }


        public void GetWeatherWithSave(WeatherParameter Param)
        {
            var jsonObject = GetWeather(Param);
            WeatherObject WObject = new JavaScriptSerializer().Deserialize<WeatherObject>(jsonObject);


            SaveObject(WObject);

        }
    }
}
