using Korn.Utils;

unsafe
{
    var byteArray = new byte[]
    {
        0xC3, // ret
        0x4C, 0x8B, 0xD1, // mov r10,rcx
        0xB8, 0x05, 0x10, 0x00, 0x00, // mov eax,1005h
    };

    fixed (byte* bytes = byteArray)
    {
        var pointer = bytes;
        var l1 = Disassembler.GetInstructionLength(pointer);
        var l2 = Disassembler.GetInstructionLength(pointer += l1);
        var l3 = Disassembler.GetInstructionLength(pointer += l2);

        _ = 3;
    }
}