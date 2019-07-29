using System;
using System.Text;

namespace WishList.Shared.Exception
{
    public static class ExceptionExtension
    {
        public static string GetExceptionMessages(this System.Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("ex");

            StringBuilder sb = new StringBuilder();

            while (exception != null)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    sb.AppendLine(exception.Message);
                }

                exception = exception.InnerException;
            }

            return sb.ToString();
        }
    }
}
