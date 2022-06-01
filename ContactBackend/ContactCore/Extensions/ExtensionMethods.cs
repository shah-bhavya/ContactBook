using System.Collections.Generic;
using System.Data;

namespace Contact.Core.Extensions
{
    public static class ExtensionMethods
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        

    }
}
