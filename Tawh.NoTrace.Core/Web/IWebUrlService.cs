namespace Tawh.NoTrace.Web
{
    public interface IWebUrlService
    {
        string GetSiteRootAddress(string tenancyName = null);
    }
}
