namespace FernandoApexa.Application.Interfaces
{
    public interface ICache<TKey, TValue>
    {
        TValue Get(TKey key);

        void Put(TKey key, TValue value);

        void Delete(TKey key);
    }
}