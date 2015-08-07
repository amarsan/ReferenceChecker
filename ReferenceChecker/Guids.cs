// Guids.cs
// MUST match guids.h
using System;

namespace CareEvolution.ReferenceChecker
{
    static class GuidList
    {
        public const string guidReferenceCheckerPkgString = "a24d4dca-b6e5-45f3-b15f-775669cb3421";
        public const string guidReferenceCheckerCmdSetString = "99752166-db94-4236-bcc4-df65bf4b41f7";
        public const string guidToolWindowPersistanceString = "5da14126-a0de-46ec-a522-706818ede33b";

        public static readonly Guid guidReferenceCheckerCmdSet = new Guid(guidReferenceCheckerCmdSetString);
    };
}