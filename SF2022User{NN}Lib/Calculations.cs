using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User_NN_Lib
{
    public class Calculations
    {
        public string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
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
                    str[indexStr] += t2.ToString() + "-";
                    time += new TimeSpan(0, consultationTime, 0);
                    t2 = new TimeSpan(time.Hours, time.Minutes, 0);
                    str[indexStr] += t2.ToString();
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
