using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TerraSoft.Contracts;

namespace TerraSoft.Test
{
    public class TextMetricsManager
    {
        private readonly Dictionary<string, ITextAnalyzer> _analyzers = new Dictionary<string, ITextAnalyzer>();

        public TextMetricsManager()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (var dll in Directory.GetFiles(path, "*.TextAnalyzer.dll"))
            {
                var assembly = Assembly.LoadFile(dll);
                var types = assembly.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(ITextAnalyzer)));
                foreach (var type in types)
                {
                    if (Activator.CreateInstance(type) is ITextAnalyzer @interface)
                    {
                        Register(@interface.MetricName, @interface);
                    }
                }
            }
        }

        public void Register(string metricName, ITextAnalyzer analyzer)
            => _analyzers.Add(metricName, analyzer);

        public object Analyze(string metricName, string text)
        {
            if (!_analyzers.ContainsKey(metricName))
            {
                return $"Metric - {metricName} does not existed";
            }

            return _analyzers[metricName].Perform(text);
        }

        public IEnumerable<(string, string)> ListOfMetrics()
            => _analyzers.Values.Select(v => (v.MetricName, v.MetricDescription));
    }
}