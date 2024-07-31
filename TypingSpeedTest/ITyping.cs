using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingSpeedTest
{
    internal interface ITyping
    {
        bool CheckString(string s1, string s2, int ptr);
        string CheckAccuracy(int totalCount, int wrongCount);
        string CheckTypingSpeed(int totalCount, int wrongCount, int currentTime);


    }
}
