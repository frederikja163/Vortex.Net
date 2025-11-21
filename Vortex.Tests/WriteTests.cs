namespace Vortex.Tests;

public sealed class WriteTests
{
    [Test]
    public void WriteTest()
    {
        string path = Path.GetTempFileName();

        VxSession session = new VxSession();
        Assert.That(session, Is.Not.EqualTo(VxSession.Zero));

        int?[] agesData = [25, 30, null, 40, 45];
        VxArray agesArray = new VxArray(agesData);
        
        
    }
}