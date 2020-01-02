#region Usings
using System.Collections.Generic;
#endregion

namespace Shield.Framework.Validation
{
    public interface IValidationRule<TType> : IValidate<TType>, IValidateThrow
    {
        #region Properties
        IEnumerable<IValidationPredicate<TType>> Predicates { get; }
        IValidationTarget<TType> Target { get; }
        #endregion

        #region Methods
        IValidationRule<TType> AddPredicate(IValidationPredicate<TType> predicate);
        #endregion
    }
}