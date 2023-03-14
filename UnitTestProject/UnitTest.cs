using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SF2022User_NN_Lib;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Проверка интервалов на соответствие
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_Equals()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;

            string[] expected = new string[]
            {
                "08:00-08:30",
                "08:30-09:00",
                "09:00-09:30",
                "09:30-10:00",
                "11:30-12:00",
                "12:00-12:30",
                "12:30-13:00",
                "13:00-13:30",
                "13:30-14:00",
                "14:00-14:30",
                "14:30-15:00",
                "15:40-16:10",
                "16:10-16:40",
                "17:30-18:00"
            };
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Проверка интервалов на отсутствие свободных временных интервалов
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_EqualsIsNull()
        {
            TimeSpan[] startTimes = new TimeSpan[]
{
                new TimeSpan(9,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(12,0,0),
                new TimeSpan(13,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
};

            int[] durations = new int[]
            {
                80, 40, 40, 40, 60, 10, 40
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 100;

            string[] expected = new string[] { };
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Метод не возвращает нулевое значение
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsNotNull()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Метод возвращает массив строк
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsTypeStringArray()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.IsInstanceOfType(actual, typeof(string[]));
        }

        /// <summary>
        /// Начало рабочего дня пустое значение 
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsBeginWorkingTimeNull()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan beginWorkingTime = new TimeSpan();
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calc = new Calculations();
            Assert.ThrowsException<System.Exception>(()=>Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        /// <summary>
        /// Массив занятого времени не равен массиву длительности
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsStartTimeLengthNotEqualsDuartionLength()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10
            };

            TimeSpan beginWorkingTime = new TimeSpan(8,0,0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calc = new Calculations();
            Assert.ThrowsException<System.Exception>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        /// <summary>
        /// Конец рабочего дня пустое значение 
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsEndWorkingTimeNull()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan();
            int consultationTime = 30;
            Calculations calc = new Calculations();
            Assert.ThrowsException<System.Exception>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        /// <summary>
        /// Длительность консультации равна нулю
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsConsultationTimeNull()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calc = new Calculations();
            Assert.ThrowsException<System.Exception>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        /// <summary>
        /// Пустой массив начал занятого времени
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsStartTimesNull()
        {
            TimeSpan[] startTimes = new TimeSpan[] { };

            int[] durations = new int[]
            {
                60, 30, 10, 10
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calc = new Calculations();
            Assert.ThrowsException<System.Exception>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        /// <summary>
        /// Пустой массив начал занятого времени
        /// </summary>
        [TestMethod]
        public void AvailablePeriods_IsDurationsNulll()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };

            int[] durations = new int[] { };

            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calc = new Calculations();
            Assert.ThrowsException<System.Exception>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }
    }
}
