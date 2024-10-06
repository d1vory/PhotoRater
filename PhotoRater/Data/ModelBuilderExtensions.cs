using Microsoft.EntityFrameworkCore;
using PhotoRater.Models.Directory;

namespace Task11.Data;

public static class ModelBuilderExtensions
{
    public static Status[] DefaultStatuses = new[]
    {
        new Status() { Id = 1, Name = "On review" },
        new Status() { Id = 2, Name = "Inactive" },
        new Status() { Id = 3, Name = "Reviewed" },
        new Status() { Id = 4, Name = "Deleted" },
    };

    public static Tag[] DefaultTags = new[]
    {
        new Tag() {Id = 1, Name = "Tired"},
        new Tag() {Id = 2, Name = "Uncomfortable"},
        new Tag() {Id = 3, Name = "Bad Lighting"},
        new Tag() {Id = 4, Name = "Forced smile"},
        new Tag() {Id = 5, Name = "Unnatural"},
        new Tag() {Id = 6, Name = "Bad expression"},
        new Tag() {Id = 7, Name = "Too far away"},
        new Tag() {Id = 8, Name = "Too close up"},
        new Tag() {Id = 9, Name = "Too bright"},
        new Tag() {Id = 10, Name = "Too dark"},
        new Tag() {Id = 11, Name = "Overused filters"},
        new Tag() {Id = 12, Name = "Skin tone"},
        new Tag() {Id = 13, Name = "Too intense"},
        new Tag() {Id = 14, Name = "Too serious"},
        new Tag() {Id = 15, Name = "Cant see face"},
        new Tag() {Id = 16, Name = "Look great!"},
        new Tag() {Id = 17, Name = "Bad outfit"},
        new Tag() {Id = 18, Name = "Great outfit"},
    };
    
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>().HasData(DefaultStatuses);
        modelBuilder.Entity<Tag>().HasData(DefaultTags);
    }
}