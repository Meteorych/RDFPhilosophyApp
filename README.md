# WPF-Philosophy App
## Short description
Actually, this is just a GUI to interacact with ontology of Philosophers, imported from the local RDF-file Philosophy.rdf to the Neo4j database.

## Requirements
To make WPF GUI-application work there should be installed _NEO4J.Driver_ NuGet Package and _Neosemantics_ plugin for the _Neo4j_. Queries in this program were specifically written for provided RDF-files.

## Interface and Exemple of work
| ![Interface image](https://github.com/Meteorych/RDFPhilosophyApp/assets/90402270/7d2724b7-7fb1-4c39-92bc-022178e8be7e) |
|:--:|
| *The beginning interface with buttons for query and ListView for visualisation of elements.* |

In the bottom of image you can see a lot of different queries. Some of them are primitive and just execute certain quert without even ability to provide new info and some of them make visible _Grid_ with _TextBoxes_ for providing information.

| ![image](https://github.com/Meteorych/RDFPhilosophyApp/assets/90402270/a55d05e0-7010-43ea-ad69-9cfdd3cbfbd5) |
|:--:|
| *Grid that shows after clicking button "Get philosopher by year and branch"* |

All queries write new info into the database or vice-versa return some result, using _ListView_ to visualize it.
