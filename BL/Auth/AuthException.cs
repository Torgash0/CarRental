namespace BL.Auth;

public enum Error
{
    UserNotFound,
    LoginOrPasswordIsIncorrect,
    DiscoveryDocumentError,
    TokenRequestError,
    CreationUserError
}

public class AuthException(string message, Error errorCode) : Exception(message)
{
    public Error ErrorCode { get; } = errorCode;
}

public class UserNotFoundException(string message) : AuthException(message, Error.UserNotFound) { }

public class LoginOrPasswordInIncorrectException(string message) : AuthException(message, Error.LoginOrPasswordIsIncorrect) { }

public class DiscoveryDocumentException(string message) : AuthException(message, Error.DiscoveryDocumentError) { }

public class TokenRequestException(string message) : AuthException(message, Error.TokenRequestError) { }

public class UserCreationException(string message) : AuthException(message, Error.CreationUserError) { }
