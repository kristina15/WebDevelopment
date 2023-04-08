namespace SCB.Surkova.CreditApprovalSystem.Hash.Interfaces
{
    public interface IHashing
    {
        byte[] GetHash(string message);
        bool CompareHash(byte[] sentHashValue, string messageString);
    }
}
