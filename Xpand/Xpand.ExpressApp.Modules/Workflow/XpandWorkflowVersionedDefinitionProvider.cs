using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Workflow.CommonServices;
using DevExpress.ExpressApp.Workflow.Versioning;

namespace Xpand.ExpressApp.Workflow {
    public class XpandWorkflowVersionedDefinitionProvider<T, U> : WorkflowVersionedDefinitionProvider<T, U>
        where U : IUserActivityVersion
        where T : IWorkflowDefinition {
        readonly List<Type> _types;

        public XpandWorkflowVersionedDefinitionProvider(List<Type> types) {
            _types = types;
        }

        public XpandWorkflowVersionedDefinitionProvider(IObjectSpaceProvider objectSpaceProvider1, WorkflowVersioningEngine engine, List<Type> types)
            : base(objectSpaceProvider1, engine) {
            _types = types;
        }

        public XpandWorkflowVersionedDefinitionProvider(IObjectSpaceProvider objectSpaceProvider, List<Type> types)
            : base(objectSpaceProvider) {
            _types = types;
        }

        public override IList<IWorkflowDefinition> GetDefinitions() {
            IList<IWorkflowDefinition> result = base.GetDefinitions();
            IObjectSpace objectSpace = ObjectSpaceProvider.CreateObjectSpace();
            foreach (var type in _types) {
                IEnumerable<IXpandWorkflowDefinition> objects = objectSpace.GetObjects(type).OfType<IXpandWorkflowDefinition>();
                foreach (var definition in objects) {
                    result.Add(definition);
                }
            }
            return result;
        }

    }
}