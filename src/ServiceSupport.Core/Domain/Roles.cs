using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Core.Domain
{
    public class Roles
    {
        public static string Admin => "admin";
        public static string Serviceman => "serviceman";
        public static string ServiceCoordinator => "servicecoordinator";

        private static List<string> RolesList = new List<string>
        {
            Admin, Serviceman,ServiceCoordinator
        };

        public static bool Contains(string role)
        {
            return RolesList.Contains(role);
        }
    }
}
