using Installment.Domain;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Installment.States
{
    public static class Extensions
    {
        private static readonly string path = @"..\..\..\DataBase";

        public static bool IsValidCitizen(string PassportId)
        {
            if(!File.Exists(path + "\\CitizenData.json")) return false;

            List<CitizenModel> list = JsonConvert.DeserializeObject<List<CitizenModel>>
                (File.ReadAllText(path + "\\CitizenData.json")) ?? new List<CitizenModel> { };
                                                                                            
            return list.Find(x => x.PassportId == PassportId) != null;
        }

        private static FederalInfo? GetFederalInfo(string PassportId)
        {
            if (!IsValidCitizen(PassportId)) return null;

            List<FederalInfo>? list = JsonConvert.DeserializeObject<List<FederalInfo>> (File.ReadAllText(path + "\\FederalInfo.json"));

            return list?.Find(x => x.PassportId == PassportId);
        }

        private static Taxpayer? GetTaxpayer(string PassportId)
        {
            if (!IsValidCitizen(PassportId)) return null;

            List<Taxpayer>? list = JsonConvert.DeserializeObject<List<Taxpayer>>(File.ReadAllText(path + "\\Taxpayer.json"));

            return list?.Find(x => x.PassportId == PassportId);
        }

        private static int TaxpayerScoring(string PassportId)
        {
            var taxpayer = GetTaxpayer(PassportId);

            int taxpayerScore = 0;

            taxpayerScore += taxpayer?.SocialStatus != SocialStatus.UnEmployed ? 35 : 0;
            
            taxpayerScore += taxpayer?.Debt == 0 ? 35 : taxpayer?.Debt < 2 * 
            taxpayer?.Income ? 25 : taxpayer?.Debt < 4 * taxpayer?.Income ? 15 : 0;

            return taxpayerScore;
        }

        private static int FederalScoring(string PassportId)
        {
            var federal = GetFederalInfo(PassportId);

            int federalScore = 10;

            federalScore += federal.IsDivorced! ? 10 : 0;
            federalScore += federal.IsConvicted! ? 10 : 0;
            
            return federalScore;
        }

        public static Task TotalScore(string PassportId)
        {
            int totalScore = 0;

            var task1 = new Task<int>(() => totalScore += FederalScoring(PassportId));
            var task2 = new Task<int>(() => totalScore += TaxpayerScoring(PassportId));

            task1.Start();
            task2.Start();

            Task.WaitAll(task1, task2);

            return new Task(() =>
            {
                if (totalScore >= 50)
                {
                    var taxpayer = GetTaxpayer(PassportId);
                    Console.WriteLine($"You can take {taxpayer.Income * 20 * totalScore / 100} sum of money");
                }
                else
                    Console.WriteLine("Sorry you cannot use our system!!!");
            });
        }

        public static void WriteData(CitizenModel citizen, Taxpayer taxpayer, FederalInfo federal)
        {
            List<CitizenModel> list1 = JsonConvert.DeserializeObject<List<CitizenModel>>
                (File.ReadAllText(path + "\\CitizenData.json")) ?? new List<CitizenModel> { };

            List<FederalInfo> list2 = JsonConvert.DeserializeObject<List<FederalInfo>>
                (File.ReadAllText(path + "\\FederalInfo.json")) ?? new List<FederalInfo> { };

            List<Taxpayer> list3 = JsonConvert.DeserializeObject<List<Taxpayer>>
                (File.ReadAllText(path + "\\Taxpayer.json")) ?? new List<Taxpayer> { };

            list1.Add(citizen);
            list2.Add(federal);
            list3.Add(taxpayer);

            File.WriteAllText(path+"\\CitizenData.json",JsonConvert.SerializeObject(list1, Formatting.Indented));
            File.WriteAllText(path+"\\FederalInfo.json",JsonConvert.SerializeObject(list2, Formatting.Indented));
            File.WriteAllText(path+"\\Taxpayer.json",JsonConvert.SerializeObject(list3, Formatting.Indented));
        }

    }
}
