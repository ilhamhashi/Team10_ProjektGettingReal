using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATApp.MVVM.Model.Classes;

namespace DATApp.Core
{
    public static class Session
    {
        public static User CurrentUser { get; set; }
    }
}
