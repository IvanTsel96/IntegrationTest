using System;
using Web.API.Domain;

namespace Web.API.Integration.Tests.Mock
{
    internal static class RecordMockHelper
    {
        internal static Guid IdToGet => new("c17d884d-10ba-4fa3-997a-7f71fdab4113");
        internal static Guid IdToDelete => new("649b7033-bd35-495b-92f5-1f08de4ec5c4");
        internal static Guid IdToUpdate => new("fb994b65-9f4c-4b4e-9a40-bb65ca5b039d");

        internal static Record GetRecord(Guid id, string name = null, string description = null)
        {
            return new Record
            {
                Id = id,
                Name = name ?? "Name",
                Description = description
            };
        }
    }
}
