using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
//using Newtonsoft.Json;

class Program
{
    public class sWaveLog
    {
        public string LOT { get; set; } = "";
        public string Address { get; set; } = "";
        public string Version { get; set; } = "";
        public bool Test1 { get; set; } = false;
        public bool Test2 { get; set; } = false;
        public float Speed { get; set; } = 0.0f;
        public float Direction { get; set; } = 0.0f;
        public float T2 { get; set; } = 0.0f;
        public float TR { get; set; } = 0.0f;
        public float TL { get; set; } = 0.0f;
        public string timestamp { 
            get { return this.DateTimeNow.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        public DateTime DateTimeNow = DateTime.Now;
        public void SetTimeStamp() { DateTimeNow = DateTime.Now; }

        public sWaveLog() {
            SetTimeStamp();
        }
    }

    static void Main(string[] args)
    {
        var url = "https://search-gssensorlog-6qnhm2sakbyedo6a3tuwn5cq54.ap-northeast-2.es.amazonaws.com/data-sensor-wave-live-v1/_doc/";
        // var data = "{" +
        //     "\"LOT\": \"ES002\"," + 
        //     "\"address\": \"8569206B\"," +
        //     "\"Version\": \"0.01.005\"," +
        //     "\"Test1\": true," +
        //     "\"Test2\": true," +
        //     "\"Speed\": 2.6," +
        //     "\"Direction\": 5.12," +
        //     "\"T2\": 143.1," +
        //     "\"TR\": 63," +
        //     "\"TL\": 67.9," +
        //     "\"timestamp\": \"2023-02-28 02:39:10\"" +
        // "}";

        sWaveLog body = new sWaveLog();
        body.LOT = "ES002";
        body.Address = "8569206B";
        body.Version = "0.01.005";
        body.Test1 = true;
        body.Test2 = true;
        body.Speed = 2.0f;
        body.Direction = 5.12f;
        body.T2 = 143.1f;
        body.TR = 63f;
        body.TL = 67.9f;


        var json = JsonSerializer.Serialize(body);
        //var json = JsonConvert.SerializeObject(body);
        //var data = new StringContent(json, Encoding.UTF8, "application/json");

        var request = WebRequest.Create(url); 
        request.Method = "POST";


        request.ContentType = "application/json";
        var requestBody = Encoding.UTF8.GetBytes(json);
        request.ContentLength = requestBody.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(requestBody, 0, requestBody.Length);
        }

        var response = (HttpWebResponse)request.GetResponse();

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            var responseText = reader.ReadToEnd();
            Console.WriteLine($"Response: {responseText}");
        }
    }
}
