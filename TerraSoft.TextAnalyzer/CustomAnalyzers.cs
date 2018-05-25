using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TerraSoft.Contracts;

namespace TerraSoft.TextAnalyzer
{
    public class Spaces : ITextAnalyzer
    {
        public string MetricName => "SpaceCounter";
        public string MetricDescription => "How many spaces present in a text";

        public object Perform(string text)
            => Regex.Matches(text, "\\s").Count;
    }

    public class Commas : ITextAnalyzer
    {
        public string MetricName => "CommaCounter";
        public string MetricDescription => "How many commas present in a text";

        public object Perform(string text)
            => Regex.Matches(text, ",").Count;
    }

    public class FirstLongestSentence : ITextAnalyzer
    {
        public string MetricName => "LongestSentence";
        public string MetricDescription => "Return first longest sentence";

        public object Perform(string text)
            => text
                .Split(new []{'.', '!', '?'})
                .Select(s => new {s.Length, Sentence = s})
                .OrderByDescending(s => s.Length)
                .FirstOrDefault()?.Sentence;
    }
}
