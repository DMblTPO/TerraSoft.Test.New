namespace TerraSoft.Contracts
{
    public interface ITextAnalyzer
    {
        string MetricName { get; }
        string MetricDescription { get; }
        object Perform(string text);
    }
}