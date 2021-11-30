using System;

namespace DDDBlocks.Domain.ValueObjects
{
    /// <summary>
    /// Shows, that current property or field will be ignored in ValueObject comparison.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IgnoreMemberAttribute : Attribute
    {
    }
}