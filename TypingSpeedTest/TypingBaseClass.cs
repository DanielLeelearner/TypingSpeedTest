using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingSpeedTest
{
    public class TypingBaseClass : ITyping
    { 

        public bool CheckString(string s1, string s2, int ptr)
        {
            return s1[ptr] == s2[ptr];
        }

        public string CheckAccuracy(int totalCount, int wrongCount)
        {
            float temp = ((float)(totalCount - wrongCount) / (float)totalCount) * 100;
            return temp.ToString("0.00");
        }

        public string CheckTypingSpeed(int totalCount, int wrongCount, int currentTime)
        {
            float temp = ((float)(totalCount - wrongCount) / 5) / ((float)currentTime / 60);
            return temp.ToString("0.0");
        }

    }
}
