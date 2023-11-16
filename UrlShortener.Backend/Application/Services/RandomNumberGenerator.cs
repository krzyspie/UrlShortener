using Application.Interfaces;

namespace Application.Services
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _randomNumber;

        public RandomNumberGenerator()
        {
            _randomNumber = new Random();
        }

        public int Generate(int max)
        {
            return _randomNumber.Next(0, max);
        }
    }
}
