using System.Reflection;

namespace Sample.Service
{
    public static class Constants
    {
        public static Assembly ServiceAssembly => typeof (Constants).Assembly;
    }
}