﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    public class Data
    {
        public TimetableSetting setting { get; set; }

        public List<Lecture> lectures { get; set; }

        public List<Task> tasks { get; set; }

    }

    public class TimetableSetting
    {

    }

    public class Lecture
    {
        // IDは被らないようにする！方法は未定！ 削除しながらやると思うからLastIDとかで保持する方法にするかも！
        public string id { get; set; }

        public string name { get; set; }

        public string professor { get; set; }

        public string syllabus { get; set; }

        public List<Lecture_URLs> otherurl { get; set; }

        public List<Lecture_Date> dates { get; set; }
    }

    public class Lecture_URLs
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Lecture_Date
    {
        public int count { get; set; }
        public string date { get; set; }
    }

    public class Task
    {
        public string id { get; set; }

        public string lecture { get; set; }

        public string name { get; set; }

        public string time { get; set; }
    }
}