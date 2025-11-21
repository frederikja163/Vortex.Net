using Vortex.Net;

namespace Vortex.Tests;

public sealed class WriteTests
{
    [Test]
    public void WriteTest()
    {
        string path = Path.GetTempFileName();
        
        using VxSession session = new VxSession();
        Assert.That(session, Is.Not.EqualTo(VxSession.Zero));
        
        int?[] agesData = [25, 30, null, 40, 45];
        using VxArray agesArray = new VxArray(agesData);
        Assert.That(agesArray, Is.Not.EqualTo(VxArray.Zero));
        Assert.That(agesArray.Length(), Is.EqualTo((IntPtr)5));
        
        VxStructFieldsBuilder fieldsBuilder = new VxStructFieldsBuilder();
        VxString agesStr = Vx.NewFromCStr("ages");
        VxDType agesType = Vx.NewPrimitive(VxPType.I32, true);
        fieldsBuilder.AddField(agesStr, agesType);
        VxStructFields fields = fieldsBuilder.Finalize();
        VxError error = new VxError();
        using VxArraySink sink = session.OpenFile("file.vortex", Vx.NewStruct(fields, false), ref error);
        error.Dispose();
        
        error = new VxError();
        sink.Push(agesArray, ref error);
        error.Dispose();
        
        Assert.That(File.Exists(path), Is.True);
    }
}