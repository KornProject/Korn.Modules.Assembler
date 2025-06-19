using System;

namespace Korn
{   
    public unsafe struct Disassembler
    {
        public byte* Pointer;

        public bool IsLengthChangingInstruction => *(uint*)Pointer == 0x66666666;
        public bool IsJmpPtrRel32Instruction => *(short*)Pointer == 0x25FF;
        public bool IsMov10PtrInstruction => (*(uint*)Pointer & 0xFFFFFF) == 0x158B4C;
        public bool IsMovRaxRel32PtrInstruction => (*(uint*)Pointer & 0xFFFFFF) == 0x058B48;
        public bool IsDecPtrRaxInstruction => (*(uint*)Pointer & 0xFFFFFF) == 0x08FF66;
        public bool IsCallRel32Instruction => *Pointer == 0xE8;
        public bool IsJmpRel32Instruction => *Pointer == 0xE9;
        public bool IsJeRel8Instruction => *Pointer == 0x74;

        public int GetJmpRel32Offset() => *(int*)(Pointer + 2);
        public int GetJmpPtrRel32Offset() => *(int*)(Pointer + 2);
        public IntPtr GetJmpRel32Operand() => (IntPtr)(Pointer + 6 + GetJmpRel32Offset());
        public IntPtr GetJmpPtrRel32Operand() => *(IntPtr*)(Pointer + 6 + GetJmpPtrRel32Offset());
        public byte GetJeRel8Offset() => Pointer[1];

        public int CountOfNextLengthChangingInstruction
        {
            get
            {
                int count = 0;
                while (Pointer[count] == 0x66)
                    count++;
                return count;
            }
        }

        public Disassembler* SkipLengthChangingInstruction() 
            => (Disassembler*)(*(byte**)self + CountOfNextLengthChangingInstruction);

        public Disassembler* SkipInstructions(int count)
        {
            for (var i = 0; i < count; i++)
                NextInstruction();

            return self;
        }

        public Disassembler* NextInstruction()
        {
            var length = GetInstructionLength(Pointer);
            Pointer += length;
            return self;
        }
        
        Disassembler* self
        {
            get
            {
                fixed (Disassembler* self = &this)
                    return self;
            }
        }

        public static implicit operator void*(Disassembler assembler) => assembler.Pointer;
        public static implicit operator IntPtr(Disassembler assembler) => (IntPtr)assembler.Pointer;

        public static byte GetInstructionLength(byte* instruction) => GetInstructionLength(InstructionsSizeTable.IndexTable, instruction);

        public static byte GetInstructionLength(byte* sizesTable, byte* instruction)
        {
            byte value = sizesTable[*instruction++];
            return value < 0x10 ? value : GetInstructionLength(InstructionsSizeTable.ShiftedTable + (value * 256), instruction);
        }

        public static int CalculateMinInstructionLength(byte* instructions, int minLength)
        {
            int result = 0;

            do
            {
                var length = GetInstructionLength(instructions + result);

                // this means what in instructions was passed wrond asm code. it causes an infinite loop
                if (length == 0)
                    return -1;

                result += length;
            }
            while (result < minLength);

            return result;
        }
    }
}