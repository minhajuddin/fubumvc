using System;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Core.Diagnostics
{
    public class RecordingFubuRequest : FubuRequest
    {
        private readonly IDebugReport _report;

        public RecordingFubuRequest(IDebugReport report, IRequestData data, IObjectResolver resolver)
            : base(data, resolver)
        {
            _report = report;
        }

        public override void Set<T>(T target)
        {
            _report.AddDetails(new SetValueReport
            {
                Type = typeof (T),
                Value = target
            });

            base.Set(target);
        }

        public override void SetObject(object input)
        {
            if (input == null) throw new ArgumentNullException("input");

            _report.AddDetails(new SetValueReport
            {
                Type = input.GetType(),
                Value = input
            });

            base.SetObject(input);
        }
    }
}