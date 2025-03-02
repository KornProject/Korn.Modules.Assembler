using System;

namespace Korn.Utils.Assembler
{
    public unsafe struct Assembler
    {
        #region Methods
        #region xor
        public Assembler* XorRbpRbp() => write(0x48, 0x31, 0xED);
        #endregion
        #region call
        public Assembler* CallRel32(IntPtr address) => write(0xE8)->write32((int)((long)address - (long)Pointer - sizeof(int)));
        public Assembler* CallRax() => write(0xFF, 0xD0);
        #endregion
        #region ret
        public Assembler* Ret() => write(0xC3);
        #endregion
        #region je
        public Assembler* JeRel32(Address address) => write(0x0F, 0x84)->write32((int)((long)address - (long)Pointer - sizeof(int)));
        public Assembler* JneRel32(Address address) => write(0x0F, 0x85)->write32((int)((long)address - (long)Pointer - sizeof(int)));
        #endregion
        #region cmp
        public Assembler* CmpR108(sbyte value) => write(0x49, 0x83, 0xFA)->write8(value);
        public Assembler* CmpRdi8(sbyte value) => write(0x48, 0x83, 0xFF)->write8(value);
        public Assembler* CmpRax8(sbyte value) => write(0x48, 0x83, 0xF8)->write8(value);
        #endregion
        #region sub
        public Assembler* SubRsp8(sbyte value) => write(0x48, 0x83, 0xEC)->write8(value);
        public Assembler* SubRsp32(int value) => write(0x48, 0x81, 0xEC)->write32(value);
        public Assembler* SubRbp32(int value) => write(0x48, 0x81, 0xED)->write32(value);
        #endregion
        #region add
        public Assembler* AddRsp8(sbyte value) => write(0x48, 0x83, 0xC4)->write8(value);
        public Assembler* AddRsp32(int value) => write(0x48, 0x81, 0xC4)->write32(value);
        public Assembler* AddRbp32(int value) => write(0x48, 0x81, 0xC5)->write32(value);
        public Assembler* AddRcx32(int value) => write(0x48, 0x81, 0xC1)->write32(value);
        public Assembler* AddRdx32(int value) => write(0x48, 0x81, 0xC2)->write32(value);
        public Assembler* AddR832(int value) => write(0x49, 0x81, 0xC0)->write32(value);
        public Assembler* AddR932(int value) => write(0x49, 0x81, 0xC1)->write32(value);
        #endregion
        #region push
        public Assembler* PushRbx() => write(0x53);
        public Assembler* PushRbp() => write(0x55);
        public Assembler* PushRdi() => write(0x57);
        public Assembler* PushR10() => write(0x41, 0x52);
        public Assembler* PushR11() => write(0x41, 0x53);
        public Assembler* PushR12() => write(0x41, 0x54);
        public Assembler* PushR13() => write(0x41, 0x55);
        public Assembler* PushR14() => write(0x41, 0x56);
        public Assembler* PushR15() => write(0x41, 0x57);
        #endregion
        #region pop
        public Assembler* PopRbx() => write(0x5B);
        public Assembler* PopRbp() => write(0x5D);
        public Assembler* PopRdi() => write(0x5F);
        public Assembler* PopR10() => write(0x41, 0x5A);
        public Assembler* PopR11() => write(0x41, 0x5B);
        public Assembler* PopR12() => write(0x41, 0x5C);
        public Assembler* PopR13() => write(0x41, 0x5D);
        public Assembler* PopR14() => write(0x41, 0x5E);
        public Assembler* PopR15() => write(0x41, 0x5F);
        #endregion
        #region mov
        public Assembler* MovRax64(Address value) => write(0x48, 0xB8)->write64(value);
        public Assembler* MovRaxRcx() => write(0x48, 0x89, 0xC8);
        public Assembler* MovRaxRdx() => write(0x48, 0x89, 0xD0);
        public Assembler* MovRaxR8() => write(0x4C, 0x89, 0xC0);
        public Assembler* MovRaxR9() => write(0x4C, 0x89, 0xC8);
        public Assembler* MovRaxR10() => write(0x4C, 0x89, 0xD0);
        public Assembler* MovRaxR10Ptr() => write(0x49, 0x8B, 0x02);
        public Assembler* MovRaxRspPtrOff32(int offset) => write(0x48, 0x8B, 0x84, 0x24)->write32(offset);
        public Assembler* MovRcxRsp() => write(0x48, 0x89, 0xE1);
        public Assembler* MovRcxPspPtrOff32(int offset) => write(0x48, 0x8B, 0x8C, 0x24)->write32(offset);
        public Assembler* MovRdxRsp() => write(0x48, 0x89, 0xE2);
        public Assembler* MovRdxPspPtrOff32(int offset) => write(0x48, 0x8B, 0x94, 0x24)->write32(offset);
        public Assembler* MovRbpRsp() => write(0x48, 0x89, 0xE5);
        public Assembler* MovRbpRspPtrOff32(int offset) => write(0x48, 0x8B, 0xAC, 0x24)->write32(offset);
        public Assembler* MovRaxRdiPtr() => write(0x48, 0x8B, 0x07);
        public Assembler* MovRspPtrOff32Rbp(int offset) => write(0x48, 0x89, 0xAC, 0x24)->write32(offset);
        public Assembler* MovRspPtrOff32Rcx(int offset) => write(0x48, 0x89, 0x8C, 0x24)->write32(offset);
        public Assembler* MovRspPtrOff32Rdx(int offset) => write(0x48, 0x89, 0x94, 0x24)->write32(offset);
        public Assembler* MovRspPtrOff32R8(int offset) => write(0x4C, 0x89, 0x84, 0x24)->write32(offset);
        public Assembler* MovRspPtrOff32R9(int offset) => write(0x4C, 0x89, 0x8C, 0x24)->write32(offset);
        public Assembler* MovRdi64(Address value) => write(0x48, 0xBF)->write64(value);
        public Assembler* MovRdiRdiPtrOff8(sbyte offset) => write(0x48, 0x8B, 0x7F)->write8(offset);
        public Assembler* MovR8Rsp() => write(0x49, 0x89, 0xE0);
        public Assembler* MovR8PspPtrOff32(int offset) => write(0x4C, 0x8B, 0x84, 0x24)->write32(offset);
        public Assembler* MovR9Rsp() => write(0x49, 0x89, 0xE1);
        public Assembler* MovR9PspPtrOff32(int offset) => write(0x4C, 0x8B, 0x8C, 0x24)->write32(offset);
        public Assembler* MovR10R10PtrOff8(sbyte offset) => write(0x4D, 0x8B, 0x52)->write8(offset);
        public Assembler* MovR1064(Address value) => write(0x49, 0xBA)->write64(value);
        public Assembler* MovR10Rax() => write(0x49, 0x89, 0xC2);
        #endregion
        #region jmp
        public Assembler* JmpRax() => write(0xFF, 0xE0);
        public Assembler* JmpRel32Ptr(Address address) => write(0xFF, 0x25)->write32((int)((long)address - (long)Pointer - sizeof(int)));
        #endregion
        #endregion
        #region Auxiliarities
        public IntPtr GetCurrentAddress() => (IntPtr)Pointer;
        public IntPtr* GetPreviousAddressValueLabel() => (IntPtr*)(Pointer - 8);
        public long* GetPreviousLongValueLabel() => (long*)(Pointer - 8);
        public int* GetPreviousIntValueLabel() => (int*)(Pointer - 4);
        public sbyte* GetPreviousSbyteValueLabel() => (sbyte*)(Pointer - 1);
        public byte* GetPreviousByteValueLabel() => (byte*)(Pointer - 1);
        public Assembler* WriteBytes(params byte[] bytes) => write(bytes);
        public void WriteInIntLabelOffset(int* label, IntPtr address) => *label = (int)((byte*)address - (byte*)label - 4);
        #endregion
        #region Internal
        Assembler* write64(Address value)
        {
            *(Address*)Pointer = value;
            Pointer += sizeof(long);

            return self;
        }

        Assembler* write32(int value)
        {
            *(int*)Pointer = value;
            Pointer += sizeof(int);

            return self;
        }

        Assembler* write16(short value)
        {
            *(short*)Pointer = value;
            Pointer += sizeof(short);

            return self;
        }

        Assembler* write8(byte value)
        {
            *Pointer++ = value;

            return self;
        }

        Assembler* write8(sbyte value)
        {
            *(sbyte*)Pointer++ = value;

            return self;
        }

        Assembler* write(params byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++)
                *Pointer++ = bytes[i];

            return self;
        }
        #endregion
        #region Implementations
        public byte* Pointer;

        Assembler* self
        {
            get
            {
                fixed (Assembler* self = &this)
                    return self;
            }
        }

        public static implicit operator void*(Assembler assembler) => assembler.Pointer;
        public static implicit operator IntPtr(Assembler assembler) => (IntPtr)assembler.Pointer;

        public struct Address
        {
            public Address(IntPtr value) => Value = value;

            public IntPtr Value;

            public static implicit operator Address(IntPtr value) => new Address(value);
            public static implicit operator Address(long value) => new Address((IntPtr)value);
            public static implicit operator Address(void* value) => new Address((IntPtr)value);
            public static implicit operator Address(byte* value) => new Address((IntPtr)value);
            public static implicit operator IntPtr(Address self) => self.Value;
            public static implicit operator long(Address self) => (long)self.Value;
            public static implicit operator void*(Address self) => (void*)self.Value;
            public static implicit operator byte*(Address self) => (byte*)self.Value;
        }
        #endregion
    }
}