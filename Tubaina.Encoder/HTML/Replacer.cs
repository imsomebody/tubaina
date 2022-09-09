using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubaina.Encoder.HTML
{
    public static class Replacer
    {
        public static string Replace((string, string)[] tokens, string value)
        {
            var replaceableValue = value;

            foreach(var token in tokens)
            {
                replaceableValue = replaceableValue.Replace($"{{{{ {token.Item1} }}}}", token.Item2);
            }

            return replaceableValue;
        }
    }
}
