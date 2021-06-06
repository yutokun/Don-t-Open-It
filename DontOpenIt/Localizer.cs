using System;
using System.Globalization;

namespace DontOpenIt
{
    public static class Localizer
    {
        public static string BeforeWorkingTime(int time)
        {
            return CultureInfo.CurrentCulture.IetfLanguageTag switch
            {
                "ja-JP" => $"{time.ToString()}時になっていませんがよろしいですか？",
                _ => $"It's before {time.ToString()}. Is this intentional?"
            };
        }

        public static string AfterWorkingTime(int time)
        {
            return CultureInfo.CurrentCulture.IetfLanguageTag switch
            {
                "ja-JP" => $"{time.ToString()}時を過ぎていますがよろしいですか？",
                _ => $"It's after {time.ToString()}. Is this intentional?"
            };
        }

        public static string DidYouOpenApp(string appName)
        {
            Console.WriteLine(CultureInfo.CurrentCulture.IetfLanguageTag);
            return CultureInfo.CurrentCulture.IetfLanguageTag switch
            {
                "ja-JP" => $"{appName} を開きましたね？",
                _ => $"Did you open {appName}?"
            };
        }
    }
}
