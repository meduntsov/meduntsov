using Application;

namespace Infrastructure;

public class DeterministicEmbeddingService : IEmbeddingService
{
    public Task<float[]> BuildEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        var vector = new float[8];
        for (var i = 0; i < text.Length; i++)
        {
            vector[i % vector.Length] += (text[i] % 31) / 31f;
        }

        return Task.FromResult(vector);
    }
}
