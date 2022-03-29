using System;

namespace Distribution
{
    /// <summary>
    /// Сервис для набора остатков округления до определённой величины
    /// </summary>
    internal class RoundingAccumulator
    {
        private const double Delta = 0.0001;

        /// <summary>
        /// Количество знаков после запятой
        /// </summary>
        private readonly int _signs;

        /// <summary>
        /// Единица, которую требуется накопить
        /// </summary>
        private readonly double _unit;

        /// <summary>
        /// Накопленные остатки округления
        /// </summary>
        private double _accumulated;

        /// <summary>
        /// Набрана ли требуемая величина по модулю
        /// </summary>
        private bool IsUnitAccumulated => Math.Abs(Math.Abs(_accumulated) - _unit) < Delta;

        public RoundingAccumulator(int signs)
        {
            _signs = signs;

            _unit = Math.Pow(0.1, signs);
        }

        /// <summary>
        /// Округляет значение, с учётом накопленных остатков от предыдущих округлений
        /// </summary>
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

        /// <summary>
        /// Возвращает <see cref="_unit"/> со знаком накопленного округления и вычитает его из <see cref="_accumulated"/>
        /// </summary>
        /// <returns></returns>
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