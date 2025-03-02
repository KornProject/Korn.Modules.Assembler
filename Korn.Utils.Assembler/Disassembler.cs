using System;

namespace Korn.Utils.Assembler
{   
    public unsafe struct Disassembler
    {
        public byte* Pointer;

        public bool IsLengthChangingInstruction => *(uint*)Pointer == 0x66666666;
        public bool IsJmpPtrRel32Instruction => *(short*)Pointer == 0x25FF;
        public bool IsMov10PtrInstruction => (*(uint*)Pointer & 0xFFFFFF) == 0x158B4C;
        public bool IsMovRaxPtrInstruction => (*(uint*)Pointer & 0xFFFFFF) == 0x058B48;
        public bool IsDecPtrRaxInstruction => (*(uint*)Pointer & 0xFFFFFF) == 0x08FF66;
        public bool IsCallRel32Instruction => *Pointer == 0xE8;
        public bool IsJmpRel32Instruction => *Pointer == 0xE9;
        public bool IsJeRel8Instruction => *Pointer == 0x74;

        public IntPtr GetJmpRel32Operand() => *(IntPtr*)(Pointer + 6 + *(int*)(Pointer + 2));
        public byte GetJeRel8Operand() => Pointer[1];

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

        public Disassembler* GetNextInstruction()
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

        public static byte GetInstructionLength(byte* instruction) => GetInstructionLength(InstructionsTables.TABLE, instruction);

        public static byte GetInstructionLength(byte[] table, byte* instruction)
        {
            byte value = table[*instruction++];
            return value < 0x10 ? value : GetInstructionLength(InstructionsTables.TABLES[value - 0x10], instruction);
        }

        public static int CalculateMinInstructionLength(byte* instructions, int length)
        {
            int result = 0;

            do result += GetInstructionLength(instructions + result);
            while (result < length);

            return result;
        }
    }
}