namespace PaperRulez
{
    public struct Process
    {
        public Process(ProcessingType type, string[] parameters)
        {
            Type = type;
            Parameters = parameters;
        }

        public ProcessingType Type { get; }
        public string[] Parameters { get; }
    }

    public enum ProcessingType
    {
        LOOKUP = 0,
        OTHER = 1
    }
}