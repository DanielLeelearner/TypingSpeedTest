using System;

namespace TypingSpeedTest
{
    public class Record
    {
        private int submission_id;
        private string name;
        private float accuracy;
        private float wpm;
        private int page;
        private string submission_date;

        public Record(int submissionId, string name, float accuracy, float wpm, int page, string submissionDate)
        {
            submission_id = submissionId;
            this.name = name;
            this.accuracy = accuracy;
            this.wpm = wpm;
            this.page = page;
            submission_date = submissionDate;
        }

        public int GetSubmissionId()
        {
            return submission_id;
        }

        public string GetName()
        {
            return name;
        }

        public float GetAccuracy()
        {
            return accuracy;
        }

        public float GetWpm()
        {
            return wpm;
        }

        public int GetPage()
        {
            return page;
        }

        public string GetSubmissionDate()
        {
            return submission_date;
        }
    }
}