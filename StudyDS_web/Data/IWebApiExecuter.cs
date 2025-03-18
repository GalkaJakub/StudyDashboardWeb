
namespace StudyDS_web.Data
{
    public interface IWebApiExecuter
    {
        Task InvokeDelete<T>(string relativeUrl);
        Task<T?> InvokeGet<T>(string relativeUrl);
        Task InvokeLogin(string? login, string? password);
        Task<T?> InvokePost<T>(string relativeUrl, T obj);
        Task<T?> InvokePostRegister<T>(string relativeUrl, T obj);
        Task InvokePut<T>(string relativeUrl, T obj);
        Task InvokePutPassword<T>(string relativeUrl, T obj);
    }
}