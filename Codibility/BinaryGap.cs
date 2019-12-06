using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;

namespace Codibility
{
    public class BinaryGap
    {

        public static int FindBinaryGap(int number)
        {
            var binary = Convert.ToString(number, 2);
            int i = 0;
            bool firstOneTracker = false;
            int zeroCount = 0;
            List<int> countBag = new List<int>();

            while (i < binary.Length)
            {
                if (binary[i] == '1' && zeroCount > 0)
                {
                    countBag.Add(zeroCount);
                    firstOneTracker = false;
                    zeroCount = 0;
                }
                if (firstOneTracker) zeroCount++;

                if (i != binary.Length - 1)
                {
                    if (binary[i] == '1' && binary[i + 1] != '1')
                        firstOneTracker = true;
                }
                else
                {
                    if(binary[i] == '1' && firstOneTracker && zeroCount > 0) countBag.Add(zeroCount);
                }

                i++;
            }
            return countBag.Count > 0 ? countBag.Max() : 0;
        }

        public static int OddOccurenceInArray(int[] A)
        {
            Array.Sort(A);
            for (int i = 0; i < A.Length; i+=2)
            {
                int appearance = 0;
                for (int j = i; j < A.Length; j++)
                {
                    if (A[i] == A[j])
                        appearance++;
                    if (appearance > 1) break;
                }

                if (appearance < 2) return A[i];
            }
            return 0;
        }

        public static int[] CyclicRotation(int[] A,int K)
        {
            for (int i = 0; i < K; i++)
            {
                var lastItem = A[A.Length - 1];
                for (int j = 0; j < A.Length; j++)
                {
                    A[A.Length - (1 + j)] = j == A.Length - 1 ? lastItem : A[A.Length - (2 + j)];
                }
            }
            return A;
        }

        public static int[] CyclicRotationToLeftSide(int[] A, int K)
        {
            
            for (int i = 0; i < K; i++)
            {
                var firstItem = A[0];
                for (int j = 0; j < A.Length; j++)
                {
                    A[j] = j == A.Length - 1 ? firstItem : A[1 + j];
                }
            }
            return A;
        }

        public static int ReturnMissingElementInArray(int[] A)
        {
            return 0;
        }

        public static int optimalPoint(List<int> magic, List<int> dist)
        {
            var magicSum = magic.Sum();
            var distSum = dist.Sum();
            if (distSum > magicSum)
                return -1;
            var index = 0;
            var magicUnit = magic[0];
            for (int i = 0; i < magic.Count; i++)
            {
                if (magicUnit < dist[i])
                {
                    magicUnit = magicUnit - dist[i];
                    break;
                }

                magicUnit = i == magic.Count - 1 ? magicUnit - dist[i] : magicUnit - dist[i] + magic[i + 1];
            }

            if (magicUnit < 0 && index < magic.Count - 1)
            {
                var newMagic = CyclicRotation(magic);
                var newDist = CyclicRotation(dist);
                index++;
                optimalPoint(newMagic, newDist);
            }

            if (magicUnit > 0)
                return index;

            return -1;
        }

        public static List<int> CyclicRotation(List<int> A)
        {
            for (int i = 0; i < 1; i++)
            {
                var lastItem = A[A.Count - 1];
                for (int j = 0; j < A.Count; j++)
                {
                    A[A.Count - (1 + j)] = j == A.Count - 1 ? lastItem : A[A.Count - (2 + j)];
                }
            }
            return A;
        }

    }

    public class TravelDetails
    {

        public string admitUntil { get; set; }
        public string admitUntilDay { get; set; }
        public string admitUntilMonth { get; set; }
        public string admitUntilYear { get; set; }
        public string arrivalDate { get; set; }
        public string arrivalDay { get; set; }
        public string arrivalMonth { get; set; }
        public string arrivalYear { get; set; }
        public string classOfAdmission { get; set; }
        public string dob { get; set; }
        public string dobDay { get; set; }
        public string dobMonth { get; set; }
        public string dobYear { get; set; }
        public string error { get; set; }
        public string firstName { get; set; }
        public string i94Number { get; set; }
        public string lastName { get; set; }
        public string passportIssueCountry { get; set; }
        public string passportNumber { get; set; }
        public string resultCode { get; set; }
        public List<TravelHistory> travelHistory { get; set; }

        int GetTravelHistoryData(List<TravelHistory> travelHistories)
        {
            return travelHistories.Select(x => Convert.ToDateTime(x.arrivalDate).Year == 2018).ToList()[0] ? travelHistories.Where(x => Convert.ToDateTime(x.arrivalDate).Year == 2018).ToList().Count() : 0;             
        }

         int CalculateDateDifference(string arrivalDate, string departureDate)
        {
            var theArrivalDate = Convert.ToDateTime(arrivalDate);
            var theDepartureDate = Convert.ToDateTime(departureDate);

            return Convert.ToInt32((theDepartureDate - theArrivalDate).TotalDays);
        }

        int GetDaysInYear(List<TravelHistory> travelHistories, int year)
        {
            var residingYear = travelHistories.Select(x => Convert.ToDateTime(x.arrivalDate).Year == 2018).ToList()[0];
            if (!residingYear) return 0;
            var inYear = travelHistories.Where(x => Convert.ToDateTime(x.arrivalDate).Year == year).ToList();
            int sumOfDaysInYear = 0;
            inYear.ForEach(data =>
            {
                sumOfDaysInYear += CalculateDateDifference(data.arrivalDate, data.departureDate);
            });
            return sumOfDaysInYear;
        }

        double CalculateResidencyStatus(int daysIn2018, int daysIn2017, int daysIn2016)
        {
            return ((double)daysIn2018 + 0.33) * ((double)daysIn2017 + 0.17) * daysIn2016; 
        }
        public string ResidencyStatus(List<TravelHistory> travelHistories)
        {

            var travelHistoryData = GetTravelHistoryData(travelHistories);
            if (travelHistoryData <= 0) return "Not a tax resident";

            //Based on the assumption that we are only getting dates from 2018
            var numberOfDaysIn2018 = GetDaysInYear(travelHistories, 2018);
            var numberOfDaysIn2017 = GetDaysInYear(travelHistories, 2017);
            var numberOfDaysIn2016 = GetDaysInYear(travelHistories, 2016);

            var status = CalculateResidencyStatus(numberOfDaysIn2018, numberOfDaysIn2017, numberOfDaysIn2016);

            return Math.Round(status) >= 183 ? "He is a tax resident" : "Not a tax resident";
        }
    }

    public class TravelHistory
    {
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }
        public string eventDate { get; set; }
        public string eventType { get; set; }
        public string location { get; set; }
        public string portOfEntry { get; set; }
        public string portOfExit { get; set; }
    }
}

