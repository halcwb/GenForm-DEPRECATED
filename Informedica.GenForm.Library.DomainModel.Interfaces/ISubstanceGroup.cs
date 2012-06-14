using System;
using System.Collections.Generic;

namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface ISubstanceGroup
    {
        Guid Id { get; }
        String Name { get; }
        ISubstanceGroup MainSubstanceGroup { get; set; }
        IEnumerable<ISubstance> Substances { get; }
        bool ContainsSubstance(ISubstance subst);
        void AddSubstance(ISubstance substance);
        void Remove(ISubstance substance);
        void ClearAllSubstances();
    }
}