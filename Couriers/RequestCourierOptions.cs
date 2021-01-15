using System;

namespace Couriers
{
    public class RequestCourierOptions
    {
        public ActivityConfig ActivityOne { get; set; }
        public ActivityConfig ActivityTwo { get; set; }
    }

    public class ActivityConfig
    {
        public string Name { get; set; }
        public Uri ExecuteUri { get; set; }
    }
}