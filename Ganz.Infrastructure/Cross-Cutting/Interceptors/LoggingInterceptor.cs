using Castle.DynamicProxy;

namespace Ganz.Infrastructure.Cross_Cutting.Interceptors
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Executing method: {invocation.Method.Name}");
            invocation.Proceed();
            Console.WriteLine($"Method executed: {invocation.Method.Name}");
        }
    }
}
