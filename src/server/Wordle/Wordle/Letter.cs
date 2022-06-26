namespace Wordle.Domain
{
    public class Letter
    {
        public char Value { get; private set; }
        public int Validity { get; private set; }

        public Letter(char letter, int validity)
        {
            Value = letter;
            Validity = validity;
        }
    }
}