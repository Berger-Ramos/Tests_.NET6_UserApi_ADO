namespace UserApi.Service.ServiceInterface
{
    public interface ITokenService
    {
        public string GanerateToken();
        public void AddClaims(IDictionary<string, string> claims);
    }
}
