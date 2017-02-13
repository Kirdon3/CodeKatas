using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    class Noobify
    {
        public static string N00bify(string text)
        {
            var dict = new Dictionary<string, string>()
            {
                {"to", "2"},
                {"too", "2"},
                {"for", "4"},
                {"fore", "4"},
                {"oo", "00"},

                {"be", "b"},
                {"are", "r"},
                {"you", "u"},
                {"please", "plz"},
                {"people", "ppl"},
                {"really", "rly"},
                {"have", "haz"},
                {"know", "no"},
                {"s", "z"},
                {"S", "Z"},

                {".", ""},
                {",", ""},
                {"'", ""},


            };
            foreach (var pair in dict)
            {
                text = text.Replace(pair.Key, pair.Value);
                text = text.Replace(pair.Key.ToUpper(), pair.Value.ToUpper());

            }

            var LolStringVar = "";
            if (text.ToLower().StartsWith("w"))
            {
                LolStringVar = "LOL ";
            }

            
            if (text.Length + LolStringVar.Length >= 32)
            {
                text = LolStringVar + "OMG " + text;
            }

            text = string.Join(" ", text.Split(' ').Select((x,y) => y % 2 != 0 ? x.ToUpper() : x));

            if (text.ToLower().StartsWith("h"))
            {
                text = text.ToUpper();
            }

            if (text.Contains("!"))
            {
                text = text.Replace("!", "");

                for (int i = 1; i <= text.Split(' ').Length; i++)
                {
                    if (i % 2 == 0)
                    {
                    text += "1";

                    }
                    else
                    {
                    text += "!";

                    }

                }
            }

            if (text.Contains("?"))
            {
                text = text.Replace("?", "");

                for (int i = 1; i <= text.Split(' ').Length; i++)
                {
                  
                  text += "?";


                }
            }
            return text;
        }
    }

    [TestFixture]
    public static class CheckNoobism
    {
        [Test]
        public static void HowRU()
        {
            Assert.AreEqual("HI HOW R U 2DAY?????", Noobify.N00bify("Hi, how are you today?"));
        }

        [Test]
        public static void LongSentence()
        {
            Assert.AreEqual("OMG I think IT would B nice IF we COULD all GET along",
               Noobify.N00bify("I think it would be nice if we could all get along."));
        }

        [Test]
        public static void CommasMatter()
        {
            Assert.AreEqual("Letz EAT Grandma!1!",
               Noobify.N00bify("Let's eat, Grandma!"));
        }
    }
}
