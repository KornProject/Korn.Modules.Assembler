using System;

namespace Korn.Utils.Assembler
{
    public unsafe struct Assembler
    {
        public Assembler(IntPtr* pointer) : this((byte**)pointer) { }
        public Assembler(void** pointer) : this((byte**)pointer) { }
        public Assembler(byte** pointer) => Pointer = pointer;

        public byte** Pointer;

        public Assembler WriteBytes(params byte[] bytes)
        {
            Memory.MemoryExtensions.Copy(*(IntPtr*)Pointer, bytes);
            *Pointer += bytes.Length;

            return this;
        }

        public Assembler MovRax64(long value)
        {
            write(0x48, 0xB8);
            write64(value);

            return this;
        }

        void write64(long value)
        {
            *(long*)*Pointer = value;
            *Pointer += 8;
        }

        void write(params byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++)
                (*Pointer)[i] = bytes[i];
            *Pointer += bytes.Length;
        }

        public static implicit operator Assembler(void** pcode) => new Assembler(pcode);
        public static implicit operator void*(Assembler assembler) => assembler.Pointer;
        public static implicit operator IntPtr(Assembler assembler) => (IntPtr)assembler.Pointer;
    }
}