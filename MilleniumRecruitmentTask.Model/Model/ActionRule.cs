using MilleniumRecruitmentTask.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilleniumRecruitmentTask.Model.Model
{
    public class ActionRule
    {
        public string ActionName { get; }
        public CardType[] AllowedTypes { get; }
        public CardStatus[] AllowedStatuses { get; }
        public bool? RequiresPinSet { get; }

        public ActionRule(
            string actionName,
            CardType[] allowedTypes,
            CardStatus[] allowedStatuses,
            bool? requiresPinSet = null)
        {
            ActionName = actionName;
            AllowedTypes = allowedTypes;
            AllowedStatuses = allowedStatuses;
            RequiresPinSet = requiresPinSet;
        }

        public bool IsAllowed(CardDetails card)
        {
            if (!AllowedTypes.Contains(card.CardType)) return false;
            if (!AllowedStatuses.Contains(card.CardStatus)) return false;
            if (RequiresPinSet.HasValue && RequiresPinSet.Value != card.IsPinSet)
                return false;
            return true;
        }
    }
}
