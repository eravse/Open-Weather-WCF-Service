using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OpenWeatherWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOpenWeatherService
    {

        [OperationContract]
        string GetWeather(WeatherParameter Param);

        [OperationContract]
        WeatherObject GetWeatherobject(WeatherParameter Param);

        void SaveObject(WeatherObject wObject);

        void GetWeatherWithSave(WeatherParameter Param);

        // TODO: Add your service operations here
    }
    public class WeatherParameter
    {
        public RequestType RequestType { get; set; }
        public DataFormat Format { get; set; }
        public UnitType UnitType { get; set; }
        public string q { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }

    public enum RequestType
    {

        ByCityName = 0,
        ByCoordinates = 1,
        ByCityId = 2
    }


    public enum DataFormat
    {
        jSon = 0,
        Xml = 1,
        Html = 2,
        WeatherObject = 3,

    }

    public enum UnitType
    {
        Imperial = 0,
        Metrics = 1
    }


    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Sys
    {
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Rain
    {
        public int __invalid_name__3h { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class WeatherObject
    {
        public Coord coord { get; set; }
        public Sys sys { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
