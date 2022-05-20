namespace CScript
{
    [Serializable]
    public class Script
    {
        public static double GetArea(double a, double b)
        {
            return a * b;
        }

        static void Log(object message)
        {
            if (CodeLearn.CodeExecuter.OnExecute != null)
                CodeLearn.CodeExecuter.OnExecute(message);
        }

        static void Main()
        {
            string _header = @"
            namespace CScript
            {
                [Serializable]
                public class " + "ClassName" + @"
                {
            ";
            Console.WriteLine(_header);
        }
    }
}