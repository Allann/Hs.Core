namespace Hs.Core.Data
{
    public interface IPaging
    {
        int Page { get; }
        int Skip { get; }
        int Take { get; }
    }

}
