using Buildflow.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Buildflow.Infrastructure.Entities;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Report
{
    [JsonPropertyName("reportid")]
    public int ReportId { get; set; }

    [JsonPropertyName("reportcode")]
    public string? ReportCode { get; set; }

    [JsonPropertyName("reporttype")]
    public string ReportType { get; set; } = null!;

    [JsonPropertyName("projectid")]
    public string ProjectId { get; set; } = null!;

    [JsonPropertyName("reportdate")]
    public DateTime ReportDate { get; set; } = DateTime.Now;

    [JsonPropertyName("reportedby")]
    public string ReportedBy { get; set; } = null!;
    [JsonIgnore]
    public string ReportData { get; set; } = null!;

    private ReportData? _reportDataJson;
    [NotMapped] // Tells EF Core to ignore this property
    [JsonPropertyName("reportdata")]
    public ReportData ReportDataJson
    {
        get
        {
            if (_reportDataJson == null && !string.IsNullOrWhiteSpace(ReportData))
            {
                try
                {
                    _reportDataJson = JsonSerializer.Deserialize<ReportData>(ReportData);
                }
                catch (JsonException ex)
                {
                    // Handle deserialization error
                    Console.WriteLine($"Deserialization failed: {ex.Message}");
                    _reportDataJson = new ReportData(); // Or set to null or throw again
                }
            }
            return _reportDataJson ?? new ReportData();
        }
        set
        {
            _reportDataJson = value;
            try
            {
                ReportData = JsonSerializer.Serialize(value);
            }
            catch (JsonException ex)
            {
                // Handle serialization error
                Console.WriteLine($"Serialization failed: {ex.Message}");
                ReportData = "{}"; // Or set to null or throw again
            }
        }
    }

    [JsonIgnore]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    [JsonIgnore]
    public List<Attachment1> Attachment1s { get; set; } = new();
}


