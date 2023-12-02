using Application.Interfaces;
using System.Text;

namespace Application.Services
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private const string UrlChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTVWXYZ0123456789";
        private const short RandomStringLength = 8;

        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public RandomStringGenerator(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string Generate()
        {
            var maxLenght = UrlChars.Length;
           
            StringBuilder randomString = new(RandomStringLength);

            for (int i = 0; i < RandomStringLength; i++)
            {
                int index = _randomNumberGenerator.Generate(maxLenght);
                var character = UrlChars[index];
                randomString.Append(character);
            }

            return randomString.ToString();
        }
    }
}
