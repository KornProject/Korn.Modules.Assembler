namespace Korn.Utils.Assembler
{   
    public unsafe class Disassembler
    {
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