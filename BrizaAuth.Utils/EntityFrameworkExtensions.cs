using Microsoft.EntityFrameworkCore;

namespace BrizaAuth.Utils
{
  public static class EntityFrameworkExtensions
  {
    public static void SetNamespaceSchema(this ModelBuilder modelBuilder)
    {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        var namespaceParts = entityType.ClrType.Namespace?.Split('.');
        var schema = namespaceParts?.LastOrDefault()?.ToLowerInvariant();

        if (string.IsNullOrEmpty(entityType.GetSchema()) && !string.IsNullOrEmpty(schema))
          entityType.SetSchema(schema);


        var tableName = entityType.GetTableName()?.ToLowerInvariant();
        if (!string.IsNullOrEmpty(schema))
        {
          entityType.SetTableName($"{schema}_{tableName}");
        }
        else
        {
          entityType.SetTableName(tableName);
        }
      }
    }

    public static void RemovePluralisingTableNameConvention(this ModelBuilder modelBuilder)
    {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        entityType.SetTableName(entityType.DisplayName());
      }
    }

  }
}
