using MilleniumRecruitmentTask.Model.Enums;
using MilleniumRecruitmentTask.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilleniumRecruitmentTask.Model.Rules
{
    public static class AllowedActionsRule
    {
        // Pełna lista reguł dla ACTION1–ACTION13
        public static readonly List<ActionRule> Rules = new()
        {
            new ActionRule(
                "ACTION1",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{ CardStatus.Active }
            ),

            new ActionRule(
                "ACTION2",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{ CardStatus.Inactive }
            ),

            new ActionRule(
                "ACTION3",
                allowedTypes: AllTypes,
                allowedStatuses: AllStatuses
            ),

            new ActionRule(
                "ACTION4",
                allowedTypes: AllTypes,
                allowedStatuses: AllStatuses
            ),

            new ActionRule(
                "ACTION5",
                allowedTypes: new[]
                {
                    CardType.Credit
                },
                allowedStatuses: AllStatuses
            ),

            new ActionRule(
                "ACTION6",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Ordered,
                    CardStatus.Inactive,
                    CardStatus.Active,
                    CardStatus.Blocked
                },
                requiresPinSet: true
            ),

            new ActionRule(
                "ACTION7",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Ordered,
                    CardStatus.Inactive,
                    CardStatus.Active,
                    CardStatus.Blocked
                },
                requiresPinSet: true
            ),


            new ActionRule(
                "ACTION8",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Ordered,
                    CardStatus.Inactive,
                    CardStatus.Active,
                    CardStatus.Blocked
                }
            ),

            new ActionRule(
                "ACTION9",
                allowedTypes: AllTypes,
                allowedStatuses: AllStatuses
            ),

            new ActionRule(
                "ACTION10",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Ordered,
                    CardStatus.Inactive,
                    CardStatus.Active
                }
            ),

            new ActionRule(
                "ACTION11",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Inactive,
                    CardStatus.Active
                }
            ),

            new ActionRule(
                "ACTION12",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Ordered,
                    CardStatus.Inactive,
                    CardStatus.Active
                }
            ),

            new ActionRule(
                "ACTION13",
                allowedTypes: AllTypes,
                allowedStatuses: new[]{
                    CardStatus.Ordered,
                    CardStatus.Inactive,
                    CardStatus.Active,
                }
            ),
        };

        // Zwracanie wszystkich statusów i typów 
        private static CardType[] AllTypes =>
            (CardType[])Enum.GetValues(typeof(CardType));

        private static CardStatus[] AllStatuses =>
            (CardStatus[])Enum.GetValues(typeof(CardStatus));
    }
}
