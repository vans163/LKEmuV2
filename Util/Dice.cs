using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2
{
    public static class Dice
    {
        private static Random m_Random = new Random();

        public static int DiceRoll(int numDice, int numSides, int bonus)
        {
            int total = 0;
            for (int i = 0; i < numDice; ++i)
                total += Random(numSides) + 1;
            total += bonus;
            return total;
        }

        public static bool RandomBool()
        {
            return (m_Random.Next(2) == 0);
        }

        public static int RandomMinMax(int min, int max)
        {
            if (min > max)
            {
                int copy = min;
                min = max;
                max = copy;
            }
            else if (min == max)
            {
                return min;
            }

            return min + m_Random.Next((max - min) + 1);
        }

        public static int Random(int from, int count)
        {
            if (count == 0)
            {
                return from;
            }
            else if (count > 0)
            {
                return from + m_Random.Next(count);
            }
            else
            {
                return from - m_Random.Next(-count);
            }
        }

        public static int Random(int count)
        {
            return m_Random.Next(count);
        }

        public static int Roll(string str)
        {
            int start = 0;
            int index = str.IndexOf('d', start);

            if (index < start)
                return 0;

            var m_Count = Util.ToInt32(str.Substring(start, index - start));

            bool negative;

            start = index + 1;
            index = str.IndexOf('+', start);

            if (negative = (index < start))
                index = str.IndexOf('-', start);

            if (index < start)
                index = str.Length;

            var m_Sides = Util.ToInt32(str.Substring(start, index - start));

            if (index == str.Length)
                return 0 ;

            start = index + 1;
            index = str.Length;

           var m_Bonus = Util.ToInt32(str.Substring(start, index - start));

            if (negative)
                m_Bonus *= -1;

            int v = m_Bonus;
            for (int i = 0; i < m_Count; ++i)
                v += Random(1, m_Sides);

            return v;
        }
    }
}
