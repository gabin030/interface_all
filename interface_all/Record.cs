using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interface_all
{
    internal class Record
    {
        public int answer { get; set; }
        public double time { get; set; }


        public Record(int answer, double time)
        {
            this.answer = answer;
            this.time = time;
        }
        public Record()
        {
            this.answer = -1;
            this.time = -1;
        }
        List<Record> list = new List<Record>();
        public void store(int answer, double time)
        {
            list.Add(new Record(answer, time));
        }
        public double timeavg()
        {
            double avg = 0;
            avg = list.Average(x => x.time);
            return avg;
        }
        public int count()
        {
            return list.Count();
        }
        public int countrightanswer()
        {
            int count = 0;
            foreach (Record r in list)
            {
                if (r.answer == 1)
                {
                    count++;
                }
            }
            return count;
        }
        public void print()
        {
            foreach (Record r in list)
            {
                Debug.WriteLine(r.answer + " " + r.time);
            }
        }
    }
}
