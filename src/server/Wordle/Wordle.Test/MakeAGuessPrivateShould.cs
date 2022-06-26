using System.Collections;
using System.Reflection;
using Wordle.Domain;
using Wordle.Domain.Ports;
using Wordle.Domain.UseCases;

namespace Wordle.Test
{
    public class MakeAGuessPrivateShould
    {
        private class FakeGameRepository : IProvideGame
        {
            public WordleGame Create(string userId, string word)
            {
                throw new NotImplementedException();
            }

            public bool Delete(string userId)
            {
                throw new NotImplementedException();
            }

            public WordleGame Find(string userId)
            {
                throw new NotImplementedException();
            }
        }
        private class FakeWordRepository : IProvideWord
        {
            public string NewWord()
            {
                throw new NotImplementedException();
            }
        }

        [Theory]
        [ClassData(typeof(CreateLineTestData))]
        public void CreateLine_Check(string candidate, string correctWord, Letter[] expected)
        {

            var sut = new MakeAGuess(
                new FakeGameRepository()
                );

            MethodInfo? methodInfo = typeof(MakeAGuess)
                            .GetMethod("CreateLine", BindingFlags.NonPublic | BindingFlags.Instance);
            
            object[] parameters = { candidate, correctWord };
            Letter[]? result = methodInfo?.Invoke(sut, parameters) as Letter[];

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Value, result[i].Value);
                Assert.Equal(expected[i].Validity, result[i].Validity);
            }
        }

        public class CreateLineTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "eleve", "court", new Letter[] {
                    new Letter('e', 0), new Letter('l', 0),new Letter('e', 0),new Letter('v', 0),new Letter('e', 0)
                } };

                yield return new object[] { "eleve", "eleve", new Letter[] {
                    new Letter('e', 1), new Letter('l', 1),new Letter('e', 1),new Letter('v', 1),new Letter('e', 1)
                } };

                yield return new object[] { "tarie", "avion", new Letter[] {
                    new Letter('t', 0), new Letter('a', 2),new Letter('r', 0),new Letter('i', 2),new Letter('e', 0)
                } };

                yield return new object[] { "eleve", "epees", new Letter[] {
                    new Letter('e', 1), new Letter('l', 0),new Letter('e', 1),new Letter('v', 0),new Letter('e', 2)
                } };

                yield return new object[] { "route", "troue", new Letter[] {
                    new Letter('r', 2), new Letter('o', 2),new Letter('u', 2),new Letter('t', 2),new Letter('e', 1)
                } };
                //edge cases when multiple occurence correctly and incorrectly placed
                yield return new object[] { "eeeee", "aaaae", new Letter[] {
                    new Letter('e', 0), new Letter('e', 0),new Letter('e', 0),new Letter('e', 0),new Letter('e', 1)
                } };
                yield return new object[] { "eebee", "aaeae", new Letter[] {
                    new Letter('e', 2), new Letter('e', 0),new Letter('b', 0),new Letter('e', 0),new Letter('e', 1)
                } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


    }
}
