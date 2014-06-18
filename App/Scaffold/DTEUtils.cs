using System;
using EnvDTE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public class DTEUtils
    {
        public Project ActiveProject { get; set; }
        public List<CodeClass> AllClasses { get; set; }
        public List<CodeEnum> AllEnums { get; set; }

        public DTEUtils(Project activeProject)
        {
            ActiveProject = activeProject;
            ParseProject(); ;
        }

        // cache usefull TS data
        void ParseProject()
        {
            AllClasses = new List<CodeClass>();
            AllEnums = new List<CodeEnum>();
            var proj = ActiveProject;
            foreach (var item in EnumerateProjectItem(ActiveProject)) 
            {
                var code = item.FileCodeModel;
                if (code == null)
                    continue;
                foreach(var e in EnumerateCodeElement(code))
                {
                    switch (e.Kind)
                    {
                        case vsCMElement.vsCMElementClass:
                            AllClasses.Add((CodeClass)e);
                            break;
                        case vsCMElement.vsCMElementEnum:
                            AllEnums.Add((CodeEnum)e);
                            break;
                    }
                }
            }
        }

        public void test()
        {
            foreach (var cls in AllClasses)
            {
                var b = cls.Bases.Item(0);
            }
        }

        public IEnumerable<CodeClass> ClassesWithFullName(String fullName)
        {
            return AllClasses.Where(c => c.FullName == fullName);
        }

        int Level = 0;

        IEnumerable<ProjectItem> EnumerateProjectItem(ProjectItem p)
        {
            yield return p;
            Level++;
            foreach (var sub in p.ProjectItems.Cast<ProjectItem>())
                foreach (var sub2 in EnumerateProjectItem(sub))
                    yield return sub2;
            Level--;
        }
        IEnumerable<ProjectItem> EnumerateProjectItem(Project p)
        {
            foreach (var sub in p.ProjectItems.Cast<ProjectItem>())
                foreach (var sub2 in EnumerateProjectItem(sub))
                    yield return sub2;
        }
        // enumerate projects and code elements
        IEnumerable<CodeElement> EnumerateCodeElement(CodeElement element)
        {
            yield return element;
            Level++;
            foreach (var sub in element.Children.Cast<CodeElement>()) {
                foreach(var sub2 in EnumerateCodeElement(sub))
                    yield return sub2;
            }
            Level--;
        }
        IEnumerable<CodeElement> EnumerateCodeElement(CodeElements elements)
        {
            Level++;
            foreach (var sub in elements.Cast<CodeElement>()) {
                foreach(var sub2 in EnumerateCodeElement(sub))
                    yield return sub2;
            }
            Level--;
        }
        IEnumerable<CodeElement> EnumerateCodeElement(FileCodeModel code)
        {
            return EnumerateCodeElement(code.CodeElements);
        }
        IEnumerable<CodeElement> EnumerateCodeElement(ProjectItem p)
        {
            return EnumerateCodeElement(p.FileCodeModel.CodeElements);
        }

    }
}
