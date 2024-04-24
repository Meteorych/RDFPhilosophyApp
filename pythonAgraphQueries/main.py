from franz.openrdf.repository import Repository
from franz.openrdf.sail import AllegroGraphServer

import re
import os

import settings

server = AllegroGraphServer(
    host=settings.HOST,
    port=settings.PORT,
    user=settings.USER,
    password=settings.PASSWORD,
)

catalog = server.openCatalog(settings.CATALOG_NAME)

repository = catalog.getRepository(settings.REPOSITORY_NAME, Repository.ACCESS)
print(repository.getDatabaseName())

def get_philosophers_by_branch(branch: str):
    pass


def get_methods_by_branch(branch: str):
    pass


def change_philosophers_year_of_birth(philosopher: str, new_year: int):
    pass


def insert_new_philosopher(name: str, year_of_birth: int):
    pass


def get_objects(object: str):
    query = "SELECT distinct ?s ?r ?o WHERE {?s ?r ?o . ?s a %s}" % (object)
    result_list = []

    with repository.getConnection() as connection:
        result = connection.executeTupleQuery(query=query)

    with result:  # type: ignore
        for bindung_set in result:  # type: ignore
            result_list.append(
                {
                    "subject": bindung_set.getValue("s").__str__(),
                    "relation": bindung_set.getValue("r").__str__(),
                    "object": bindung_set.getValue("o").__str__(),
                }
            )

    return result_list
