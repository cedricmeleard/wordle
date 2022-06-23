namespace Wordle
{
    public class Letter
    {
        public string Value { get; private set; }
        public int Validity { get; private set; }

        public Letter(string letter, int validity)
        {
            this.Value = letter;
            this.Validity = validity;
        }
    }
}