using Neo4j.Driver;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RDFPhilosophyApp
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private IDriver _driver;

        public List<Triple> TriplesList { get; private set; }
        
        public MainWindowViewModel(string uri)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic("neo4j", "vanyavanya"));
            GetData();
        }

        public void GetData()
        {
            using var session = _driver.Session();
            var data = session.ExecuteRead(
                tx =>
                {
                    var result = tx.Run(
                        "MATCH (s)-[p]->(o)" +
                        "RETURN s.uri AS subject, type(p) AS predicate, o.uri AS object");
                    var triples = new List<Triple>();
                    foreach (var record in result)
                    {
                        triples.Add(new Triple
                        {
                            Subject = record["subject"].As<string>().Split("#").Last(),
                            Predicate = record["predicate"].As<string>()[(record["predicate"].As<string>().IndexOf("_")+2)..],
                            Object = record["object"].As<string>().Split("#").Last()
                        });
                    }
                    return triples;
                });
            TriplesList = data;
        }

        public void GetPhilosophersOnlyData()
        {
            using var session = _driver.Session();
            var data = session.ExecuteRead(
                tx =>
                {
                    var result = tx.Run(
                        "MATCH (s:ns0__Philosopher)" +
                        "RETURN s.uri AS subject");
                    var triples = new List<Triple>();
                    foreach (var record in result)
                    {
                        triples.Add(new Triple
                        {
                            Subject = record["subject"].As<string>().Split("#").Last(),
                            Predicate = "null",
                            Object = "null"
                        });
                    }
                    return triples;
                });
            TriplesList = data;
        }

        public void Dispose()
        {
            _driver.Dispose();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
