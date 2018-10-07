using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge.Importers
{
    public interface IImporter
    {
        IEnumerable<Horse> Import();
    }
}
