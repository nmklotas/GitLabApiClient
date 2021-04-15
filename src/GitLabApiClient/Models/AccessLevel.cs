namespace GitLabApiClient.Models
{
    public enum AccessLevel
    {
        Guest = 10,
        Reporter = 20,
        Developer = 30,
        Maintainer = 40,
        Owner = 50 // Only valid for groups
    }
}
