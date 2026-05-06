namespace iE.Api.Auth;

public static class AuthCapabilities
{
    public const string ReportsRead = "reports.read";
    public const string ReportsWrite = "reports.write";
    public const string ReportsSubmit = "reports.submit";
    public const string ReportsReview = "reports.review";
    public const string PhotosRead = "photos.read";
    public const string PhotosWrite = "photos.write";
    public const string ExportsRead = "exports.read";
    public const string AdminUsersManage = "admin.users.manage";

    public static readonly string[] All =
    [
        ReportsRead,
        ReportsWrite,
        ReportsSubmit,
        ReportsReview,
        PhotosRead,
        PhotosWrite,
        ExportsRead,
        AdminUsersManage
    ];
}
