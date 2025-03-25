using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Domain.Utilities;

public class SearchResultDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    [JsonIgnore]
    public int RelevanceScore { get; set; }
    
    public static int CalculateRelevanceScore(string searchTerm, params string[] fields)
    {
        // Basic example: count occurrences of the search term in the fields
        return fields.Sum(field => Regex.Matches(field.ToLower(), searchTerm).Count);
    }
}