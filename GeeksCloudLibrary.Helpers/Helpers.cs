using System;
using System.Collections.Generic;
using System.IO;
using GeeksCloudLibrary.Abstractions.Enums;
using Newtonsoft.Json;

namespace GeeksCloudLibrary.Helpers
{
    public static class Helpers
    {
        public static string GetParentDirectory()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            return parent?.FullName;
        }

        public static string ConvertDictionaryToJson<T1, T2>(Dictionary<T1, T2> dictionary)
        {
            return JsonConvert.SerializeObject(dictionary);
        }

        public static string GetProviderName(CloudProvider provider)
        {
            var providerName = "";
            switch (provider)
            {
                case CloudProvider.Igs:
                    providerName = "IGS";
                    break;
                default:
                    throw new Exception("Provider not configured");
            }

            return providerName;
        }
    }
}