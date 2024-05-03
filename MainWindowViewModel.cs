using Neo4j.Driver;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RDFPhilosophyApp
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private IDriver _driver;

        private ObservableCollection<Triple> _triplesList = [];
        
        public MainWindowViewModel(string uri)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic("neo4j", "vanyavanya"));
            GetData();
        }

        public ObservableCollection<Triple> TriplesList
        {
            get { return _triplesList; }
            set
            {
                _triplesList = value;
                OnPropertyChanged(nameof(TriplesList));
            }
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
                    var triples = new ObservableCollection<Triple>();
                    foreach (var record in result)
                    {
                        triples.Add(new Triple
                        {
                            Subject = record["subject"].As<string>().Split("#").Last(),
                            Predicate = record["predicate"].As<string>()[(record["predicate"].As<string>().IndexOf("_") + 2)..],
                            Object = record["object"].As<string>().Split("#").Last()
                        });
                    }
                    return triples;
                });
            TriplesList = data;
        }

        /// <summary>
        /// Get data from data store with several properties.
        /// </summary>
        /// <param name="uri">Uri to find subject.</param>
        /// <param name="label1">Subject's label.</param>
        /// <param name="label2">Label for relationship.</param>
        /// <param name="label3">Label for object.</param>
        public void GetData(string uri, string label1 = "", string label2 = "", string label3 = "")
        {
            using var session = _driver.Session();
            var data = session.ExecuteRead(
                tx =>
                {
                    var result = tx.Run(
                        $"MATCH (s{label1})-[p{label2}]->(o{label3})" +
                        $"WHERE s.uri = \"{uri}\"\n" +
                        "RETURN s.uri AS subject, type(p) AS predicate, o.uri AS object");
                    var triples = new ObservableCollection<Triple>();
                    foreach (var record in result)
                    {
                        triples.Add(new Triple
                        {
                            Subject = record["subject"].As<string>().Split("#").Last(),
                            Predicate = record["predicate"].As<string>()[(record["predicate"].As<string>().IndexOf("_") + 2)..],
                            Object = record["object"].As<string>().Split("#").Last()
                        });
                    }
                    return triples;
                });
            TriplesList = data;
        }

        /// <summary>
        /// In connected database you can change year of birth, height or weight of philosopher (currently works only with year of birth).
        /// </summary>
        /// <param name="newData"></param>
        public void ChangeData(string philosopherName, double newData)
        {
            using var session = _driver.Session();
            var data = session.ExecuteWrite(
                tx =>
                {
                    var result = tx.Run(
                        "MATCH (s:ns1__Philosopher)" +
                        $"WHERE s.uri = \"{philosopherName}\"\n" + 
                        $"SET s.ns1__Year_of_birth = {newData}");
                    result.Consume();
                    return 1;
                });
        }

        /// <summary>
        /// Method to get only philosophers with their year of birth
        /// </summary>
        public void GetPhilosophersOnlyData()
        {
            using var session = _driver.Session();
            var data = session.ExecuteRead(
                tx =>
                {
                    var result = tx.Run(
                        "MATCH (s:ns0__Philosopher)" +
                        "RETURN s.uri AS subject, s.ns1__Year_of_birth as year_of_birth");
                    var triples = new ObservableCollection<Triple>();
                    foreach (var record in result)
                    {
                        triples.Add(new Triple
                        {
                            Subject = record["subject"].As<string>().Split("#").Last(),
                            Predicate = "Year_of_birth",
                            Object = record["year_of_birth"].As<int>().ToString()
                        });
                    }
                    return triples;
                });
            TriplesList = data;
        }

        /// <summary>
        /// Create new philosopher.
        /// </summary>
        /// <param name="philosopherUri">URI of philosopher.</param>
        /// <param name="yearOfBirth">Philosopher's year of birth.</param>
        public void CreateNewPhilosopher(string philosopherUri, double yearOfBirth)
        {
            using var session = _driver.Session();
            var data = session.ExecuteWrite(
                tx =>
                {
                    var result = tx.Run(
                        "CREATE (n:Resource:ns0__Philosopher:ns1__Philosopher:owl__NamedIndividual " +
                        $"{{\r\n  ns1__Year_of_birth: {yearOfBirth},\r\n" +
                        $"uri: '{philosopherUri}'}})");
                    result.Consume();
                    return 1;
                });
            GetPhilosophersOnlyData();
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
