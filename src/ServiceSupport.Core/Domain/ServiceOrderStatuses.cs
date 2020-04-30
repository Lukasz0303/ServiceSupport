using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Core.Domain
{
    public class ServiceOrderStatuses
    {
        public static string AutomaticGenerated => "automaticgenerated";
        public static string Started => "started";
        public static string InProgress => "inProgress";
        public static string Finished => "finished";
        public static string AutomaticFinished => "automaticfinished";
    }
}
