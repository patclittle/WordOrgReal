namespace Core.Model
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class HideFromApiAttribute : Attribute
    {
    }
}