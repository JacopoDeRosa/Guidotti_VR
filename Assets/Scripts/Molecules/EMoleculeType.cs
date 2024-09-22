using System;

namespace Molecules
{
    public enum MoleculeType
    {
        None = 0,
        Sodium = 1 << 0,
        Glucose = 1 << 1,
        Blood = 1 << 2
    }
    
    [Flags]
    public enum MoleculeTypeFlags
    {
        None = 0,
        Sodium = 1 << 0,
        Glucose = 1 << 1,
        Blood = 1 << 2
    }
}