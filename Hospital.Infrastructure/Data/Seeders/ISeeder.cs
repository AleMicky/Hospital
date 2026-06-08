namespace Hospital.Infrastructure.Data.Seeders;

public interface ISeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}