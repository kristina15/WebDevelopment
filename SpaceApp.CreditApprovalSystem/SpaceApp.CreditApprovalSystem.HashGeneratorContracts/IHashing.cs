namespace SpaceApp.CreditApprovalSystem.HashGeneratorContracts;

public interface IHashing
{
    byte[] GetHash(string message);
    bool CompareHash(byte[] sentHashValue, string messageString);
}
