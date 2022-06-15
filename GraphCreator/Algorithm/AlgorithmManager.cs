using GraphCreator.Algorithm.SimpleAlgorithms;
using GraphCreator.Helper;
using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace GraphCreator.Algorithm
{
    class AlgorithmManager
    {
        Project project;
        KruskalSpanningTree openedAlgorithm;
        
        public AlgorithmManager(Project project, bool isSync)
        {
            this.project = project;
            openedAlgorithm = new KruskalSpanningTree(project, isSync);

            OpenAlgorithm();
        }
        
        public void OpenAlgorithm()
        {

            GraphHelper.ResetGraphMeta(project);
            GraphHelper.ClearStyle(project);

            Task.Run(async () =>
            {
                await openedAlgorithm.Run();
            });

        }
    }
}
