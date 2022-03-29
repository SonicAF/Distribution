using System;

namespace Distribution
{
    internal class RoundingAccumulator
    {
        private const double Delta = 0.0001;

        private readonly int _signs;
        private readonly double _unit;

        private bool IsUnitAccumulated => Math.Abs(Math.Abs(_accumulated) - _unit) < Delta;

        public RoundingAccumulator(int signs)
        {
            _signs = signs;

            _unit = Math.Pow(0.1, signs);
        }

        private double _accumulated;

        public double Round(double number)
        {
            var rounded = Math.Round(number, _signs);

            _accumulated += number - rounded;

            if (IsUnitAccumulated)
            {
                rounded += ExtractAccumulatedUnit();
            }

            return rounded;
        }

        private double ExtractAccumulatedUnit()
        {
            if (_accumulated > 0)
            {
                _accumulated -= _unit;

                return _unit;
            }

            _accumulated += _unit;

            return -_unit;
        }
    }
}