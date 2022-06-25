namespace Wordle.Domain
{
    public class Letter
    {
        public string Value { get; private set; }
        public int Validity { get; private set; }

        public Letter(string letter, int validity)
        {
            Value = letter;
            Validity = validity;
        }
    }
}