namespace ElectronicVoting.Validator.Domain;
public class Serialization
{
    public byte[] Serialize()
    {
        using var stream = new MemoryStream();
        ProtoBuf.Serializer.Serialize(stream, this);
        return stream.GetBuffer();
    }
}