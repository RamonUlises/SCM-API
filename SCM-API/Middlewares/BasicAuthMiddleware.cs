using System.Text;

namespace SCM_API.Middlewares
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public BasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Si no hay cabecera de autorización denegar petición y mostrar un 401
            if(!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                context.Response.Headers.Append("WWW-Authenticate", "Basic");
                await context.Response.WriteAsync("No autorizado");
                return;
            }

            // Obtener el valor de la cabecera de autorización
            var authHeader = context.Request.Headers["Authorization"].ToString();

            // Si la cabecera de autorización no comienza con "Basic" denegar petición y mostrar un 401
            if(authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            {
                var encodedUsernamePassword = authHeader["Basic ".Length..].Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var usernamePassword = decodedUsernamePassword.Split(':');

                var username = usernamePassword[0];
                var password = usernamePassword[1];

                var validUsername = _configuration["username"];
                var validPassword = _configuration["password"];

                if(username == validUsername && password == validPassword)
                {
                    await _next(context);
                    return;
                }
            }

            // Si no es válido regresar un 401
            context.Response.StatusCode = 401;
            context.Response.Headers.Append("WWW-Authenticate", "Basic");
            await context.Response.WriteAsync("Credenciales inválidas");

        }
    }
}
