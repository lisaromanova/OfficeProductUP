using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User_NN_Lib
{
    public class Calculations
    {
        /// <summary>
        /// Расчет свободных временных интервалов в графике сотрудника
        /// </summary>
        /// <param name="startTimes">Начало занятого времени</param>
        /// <param name="durations">Длительность занятого времени</param>
        /// <param name="beginWorkingTime">Начало рабочего дня сотрудника</param>
        /// <param name="endWorkingTime">Конец рабочего дня сотрудника</param>
        /// <param name="consultationTime">Минимальное необходимое время для работы менеджера</param>
        /// <returns>Список свободных временных интервалов</returns>
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            if(startTimes.Length != durations.Length || startTimes.Length == 0
                || durations.Length == 0 || beginWorkingTime == new TimeSpan()
                || endWorkingTime == new TimeSpan() || consultationTime == 0)
            {
                throw new Exception("Ошибка");
            }
            TimeSpan time = beginWorkingTime;
            string[] str = new string[0];
            int j = 0;
            int indexStr = 0;
            while (time < endWorkingTime)
            {
                TimeSpan t1;
                if (j < startTimes.Length)
                {
                    t1 = startTimes[j];
                }
                else
                {
                    t1 = endWorkingTime;
                }
                TimeSpan vych = t1 - time;
                while (vych >= new TimeSpan(0, consultationTime, 0))
                {
                    Array.Resize(ref str, str.Length + 1);
                    TimeSpan t2 = new TimeSpan(time.Hours, time.Minutes, 0);
                    str[indexStr] += t2.ToString().Substring(0, t2.ToString().Length - 3) + "-";
                    time += new TimeSpan(0, consultationTime, 0);
                    t2 = new TimeSpan(time.Hours, time.Minutes, 0);
                    str[indexStr] += t2.ToString().Substring(0, t2.ToString().Length - 3);
                    vych -= new TimeSpan(0, consultationTime, 0);
                    indexStr++;
                }
                if (j < startTimes.Length)
                {
                    time = startTimes[j];
                    time += new TimeSpan(0, durations[j], 0);
                    j++;
                }
                else
                {
                    time = endWorkingTime;
                }
            }
            return str;
        }
    }
}
