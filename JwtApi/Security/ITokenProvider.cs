
namespace JwtApi.Security
{
    public interface IJwtProvider
    {
        /// <summary>
        /// Create a jwt(json-web-token) and return token 
        /// </summary>
        /// <returns>jwt Token</returns>
        string CreateJwt();

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        bool ValidateJwt(string jwt);
    }
}