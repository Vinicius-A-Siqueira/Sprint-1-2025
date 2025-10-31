using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Infrastructure.Configuration
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public CollectionNames Collections { get; set; } = null!;
    }

    public class CollectionNames
    {
        public string Motos { get; set; } = null!;
        public string Patios { get; set; } = null!;
        public string Usuarios { get; set; } = null!;
    }
}
