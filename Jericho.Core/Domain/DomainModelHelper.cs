using System;

namespace Jericho.Core.Domain
{
    public class DomainModelHelper
    {
        public static string GetAssociationEntityNameAsPlural<T>() where T : Entity
        {
            return String.Format("{0}s", ReplaceFirstCharacterToLowerVariant(typeof(T).Name));
        }

        private static string  ReplaceFirstCharacterToLowerVariant(string name)
        {
            return Char.ToLowerInvariant(name[0]) + name.Substring(1);

        }
    }
}