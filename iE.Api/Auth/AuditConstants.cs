namespace iE.Api.Auth;

public static class AuditActions
{
    public const string AuthorizationDenied = "AuthorizationDenied";
    public const string ReportCreated = "ReportCreated";
    public const string ReportUpdated = "ReportUpdated";
    public const string ReportDeleted = "ReportDeleted";
    public const string ReportExported = "ReportExported";
}

public static class AuditResourceTypes
{
    public const string Report = "Report";
    public const string PhotoMarkup = "PhotoMarkup";
}

public static class AuditResults
{
    public const string Success = "Success";
    public const string Denied = "Denied";
}

public static class AuditMetadataKeys
{
    public const string MetadataTruncated = "metadataTruncated";
    public static readonly HashSet<string> Sensitive =
    [
        "token", "accessToken", "refreshToken", "idToken", "authorization", "authHeader", "password", "secret", "apiKey", "connectionString", "rawPayload", "requestBody", "responseBody", "markupJson", "stackTrace"
    ];
}
