using System;

namespace Complejo.Application.Utils
{
    public static class TurnCodeGenerator
    {
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("+", string.Empty).Substring(0, 7);
        }
    }
}
