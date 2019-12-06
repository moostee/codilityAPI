using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack;
using Console = System.Console;
using System.Net;
using Newtonsoft.Json;

namespace Codibility
{
    class Program
    {
        class response
        {
            public int page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
            public int total_pages { get; set; }
            public List<Data> data { get; set; }
        }

        class Data
        {
            public string date { get; set; }
            public double open { get; set; }
            public double close { get; set; }

            public string weekday { get; set; }
        }

        public static string GetData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
        static void Main(string[] args)
        {
            //var A = new int[] { -122, -5, 1, 2, 3, 4, 5, 6, 7 }; // 8
            //var B = new int[] { 1, 3, 6, 4, 1, 2 }; // 5
            //var C = new int[] { -1, -3 }; // 1
            //var D = new int[] { -3 }; // 1
            //var E = new int[] { 1 }; // 2
            //var F = new int[] { 1000000 }; // 1

            ////    var x = new int[][] { A, B, C, D, E, F };
            ////    x.ToList().ForEach((arr) =>
            ////    {
            ////        var s = new Solution();
            ////        Console.WriteLine(s.solution(arr));
            ////    });

            ////    Console.ReadLine();
            ////}
            //var s = new Solution();
            //string str = "musty";
            //int num = 5;
            //Console.WriteLine(num + str);
            ////Console.WriteLine(s.solution("27A84Q", "K2Q25J"));
            //Console.ReadLine();

            //int[] A = new[] {1,1001,1000,857};
            //Array.Sort(A);
            ////var s = new Solution().ReturnSmallestInteger(A);
            ////Console.WriteLine(A[3]);
            //DataTable hi = new DataTable();
            //hi.Columns.Add("Names");
            //hi.Rows.Add("John");
            //hi.Rows.Add("James");
            //hi.Rows.Add("Jack");
            //hi.Rows.Add("Jonas");
            //hi.Rows.Add("Jacob");

            //var result = hi.ToString();
            //Console.WriteLine(result);
            //Console.WriteLine(result.ToCsv());

            //// Console.WriteLine(BinaryGap.ReturnMissingElementInArray(A));


            //int carType = Convert.ToInt32(Console.ReadLine().Trim());
            //int carMileage = Convert.ToInt32(Console.ReadLine().Trim());

            //if (carType == 0)
            //{
            //    Car wagonR = new WagonR(carMileage);
            //    wagonR.printCar("WagonR");
            //}

            //if (carType == 1)
            //{
            //    Car hondaCity = new HondaCity(carMileage);
            //    hondaCity.printCar("HondaCity");
            //}

            //if (carType == 2)
            //{
            //    Car innovaCrysta = new InnovaCrysta(carMileage);
            //    innovaCrysta.printCar("InnovaCrysta");
            //}
            //var magic = new List<int>{2,4,5,2};
            //var dist = new List<int> {4,3,1,3};
            //var result = optimalPoint(magic,dist);

            //Uri hackerRankUri = new Uri("");
            var json = File.ReadAllText("C://Users//P7575//Desktop//test.json");
            var data = JsonConvert.DeserializeObject<TravelDetails>(json);
            var result = data.ResidencyStatus(data.travelHistory);

            Console.WriteLine(result);

            JsonObject



            //StreamReader reader = new StreamReader(webResp.GetResponseStream());
            //string str = reader.ReadLine();
            //while (str != null)
            //{
            //    Console.WriteLine(str);
            //    str = reader.ReadLine();
            //}
            //var result = hackerRankUri.Query;

            //var result = JsonConvert.DeserializeObject<response>(GetData("https://jsonmock.hackerrank.com/api/stocks"));
            //var data = new List<Data>();
            //for(var page = result.page; page<= result.total_pages; page++)
            //{
            //    var res = JsonConvert.DeserializeObject<response>(GetData("https://jsonmock.hackerrank.com/api/stocks/?page="+page));
            //    foreach(var item in res.data)
            //    {
            //        var d = Convert.ToDateTime(item.date);
            //        data.Add(new Data { date = item.date ,close = item.close,open = item.open, weekday = d.ToString("dddd")});
            //    }
            //}

            //var finalResult = data.Where(d => Convert.ToDateTime(d.date) >= Convert.ToDateTime("1-January-2000")
            //&& Convert.ToDateTime(d.date) <= Convert.ToDateTime("22-February-2000") && d.weekday == "Monday");

            //foreach(var item in finalResult)
            //{
            //    Console.WriteLine($"{item.date}   {item.open}   {item.close}");
            //}
            Console.ReadLine();

        }
        private static int index = 0;
        public static int optimalPoint(List<int> magic, List<int> dist)
        {
            var magicSum = magic.Sum();
            var distSum = dist.Sum();
            if (distSum > magicSum)
                return -1;
            
            
            var magicUnit = magic[0];
            for (int i = 0; i < magic.Count; i++)
            {
                if (magicUnit < dist[i])
                {
                    magicUnit -= dist[i];
                    break;
                }

                magicUnit = i == magic.Count - 1 ? magicUnit - dist[i] : magicUnit - dist[i] + magic[i + 1];
            }

            if (magicUnit < 0 && index < magic.Count - 1)
            {
                var newMagic = CyclicRotation(magic);
                var newDist = CyclicRotation(dist);
                index++;
                magicUnit = optimalPoint(newMagic, newDist);
            }

            if (magicUnit > 0)
                return index;

            return -1;
        }
        
        public static List<int> CyclicRotation(List<int> A)
        {
            for (int i = 0; i < 1; i++)
            {
                var firstItem = A[0];
                for (int j = 0; j < A.Count; j++)
                {
                    A[j] = j == A.Count - 1 ? firstItem : A[1 + j];
                }
            }
            return A;
        }

    }

    public class Res
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }







    abstract class Car
    {
        protected bool isSedan;
        protected string seats;

        public Car() { }

        public Car(bool isSedan, string seats)
        {
            this.isSedan = isSedan;
            this.seats = seats;
        }

        public bool getIsSedan()
        {
            return this.isSedan;
        }

        public string getSeats()
        {
            return this.seats;
        }

        abstract public string getMileage();

        public void printCar(string name)
        {
            Console.WriteLine("A {0} is{1} Sedan, is {2}-seater, and has a mileage of around {3}.",
            name,
            this.getIsSedan() ? "" : " not",
            this.getSeats(),
            this.getMileage());
        }
    }

    class WagonR : Car
    {
        private int _mileage;
        public WagonR(int mileage) : base(isSedan:false,seats:"4")
        {
            _mileage = mileage;
        }

        public override string getMileage()
        {
            return $"{_mileage} kpml";
        }
    }

    class HondaCity : Car
    {
        private int _mileage;
        public HondaCity(int mileage) : base(isSedan: false, seats: "4")
        {
            _mileage = mileage;
        }

        public override string getMileage()
        {
            return $"{_mileage} kpml";
        }
    }

    class InnovaCrysta : Car
    {
        private int _mileage;
        public InnovaCrysta(int mileage) : base(isSedan: false, seats: "4")
        {
            _mileage = mileage;
        }

        public override string getMileage()
        {
            return $"{_mileage} kpml";
        }
    }

    class Solution
    {

        public void Test()
        {
            //string json = File.ReadAllText("C:\\Users\\P7575\\Desktop\\Codibility\\Codibility\\result.json");

            //var json1 = JsonObject.Parse(json);
            //var result = json1.Get<JsonObject>("Results");
            //var output1 = result.Get<JsonObject>("output1");
            //var value = output1.Get<JsonObject>("value");
            //var values = value.Get<JsonObject>("Values");
            //var results = values.Get<string>("[");







            Console.WriteLine("Starting after 3 seconds test");
            //Console.WriteLine(json);
            //Console.WriteLine(results + 1);

            Console.ReadLine();

        }

        public dynamic solution(string A, string B)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            bool IsEqualStringAndNotEmpty = checkIfStringIsEqualLength(A, B);
            if (!IsEqualStringAndNotEmpty) return "String must be the same length and must not be empty";
            if (!checkIfStringContainsGameNumbers(A)) return $"{A} doesnt have the game numbers";
            if (!checkIfStringContainsGameNumbers(B)) return $"{B} doesnt have the game numbers";

            return CalculateAlecWins(A, B);
        }

        //public dynamic solution(int[] ArrayOfNumbers)
        //{
        //    string message = string.Empty;
        //    int arrayLength = ArrayOfNumbers.Length;
        //    int result = 1;
        //    foreach (var number in ArrayOfNumbers)
        //    {
        //        if (number < 1 && number > 100000)
        //        {
        //            message = "Numbers in the array must be between 1 and 100,000 OR between -1,000,000 and 1,000,000";
        //            return message;
        //        }
        //    }
        //    ArrayOfNumbers = ArrayOfNumbers.Where(number => number > 0).Distinct().OrderBy(lowest => lowest).ToArray();
        //    Console.WriteLine(ArrayOfNumbers);
        //    int lastNumberInArray = ArrayOfNumbers.Length - 1,
        //        count = 0;


        //    foreach (var numbers in ArrayOfNumbers)
        //    {

        //        if (numbers > result) return result;
        //        if (count == lastNumberInArray) return ++result;

        //        result++;
        //        count++;
        //    }


        //    return result;
        //}


        public bool checkIfStringIsEqualLength(string A, string B)
        {
            if (A.Length == B.Length && !String.IsNullOrEmpty(A) && !String.IsNullOrEmpty(B))
                return true;

            return false;
        }

        public bool checkIfStringContainsGameNumbers(string word)
        {
            bool IsValidString = false;

            List<string> gameNumbers = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "K", "Q" };
            foreach (var item in word.ToUpper())
            {
                if (item.ToString() == "A")
                {
                    IsValidString = true;
                }
                else if (gameNumbers.Contains(item.ToString()))
                {
                    IsValidString = true;
                }
                else
                {
                    IsValidString = false;
                    break;
                }
            }


            return IsValidString;
        }

        public List<int> ConvertStringToInt(string A)
        {
            //int i = 0;
            List<int> ToConvert = new List<int>();
            //string ToConvert = string.Empty;
            List<string> check = new List<string>() { "A", "T", "J", "K", "Q" };

            foreach (var i in A)
            {
                if (i.ToString() == "A")
                    ToConvert.Add(14);
                if (i.ToString() == "T")
                    ToConvert.Add(10);
                if (i.ToString() == "J")
                    ToConvert.Add(11);
                if (i.ToString() == "K")
                    ToConvert.Add(12);
                if (i.ToString() == "Q")
                    ToConvert.Add(13);
                if (!check.Contains(i.ToString()))
                    ToConvert.Add(Convert.ToInt32(i.ToString()));
            }

            //Console.WriteLine(ToConvert);
            return ToConvert;

        }


        public int CalculateAlecWins(string A, string B)
        {
            List<int> Alec = ConvertStringToInt(A), Bob = ConvertStringToInt(B);
            int numberOfAlecWins = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (Alec[i] > Bob[i])
                    numberOfAlecWins++;
            }

            return numberOfAlecWins;
        }

        public int ReturnSmallestInteger(int[] a)
        {
            //var A = a;
            Array.Sort(a);
            var arrayLength = a.Length;
            var lastItem = a[arrayLength - 1];
            if (lastItem <= 0)
                return 1;


            for (int i = 0; i < arrayLength - 1; i++)
            {
                if (a[i] <= 0) continue;
                var currentIndex = a[i];
                if (i < arrayLength - 1)
                {
                    var nextIndex = a[i + 1];
                    if (nextIndex - currentIndex > 1)
                        return nextIndex - 1;
                }
            }

            return lastItem + 1;
        }




    }
}
