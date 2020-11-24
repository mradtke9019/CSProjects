
from fuzzywuzzy import fuzz
from fuzzywuzzy import process
import os


def code():
    names = os.listdir('D:\Videos\Movies')
    # use fuzzy ratio of 90 as the cutoff
    ids = []
    label = []

    for name1 in names:
        for name2 in names:
            if name1 == name2:
                continue
            if fuzz.token_sort_ratio(name1,name2) > 80:
                print("Match")
                print(name1, name2)

def main():
    code()


if __name__ == '__main__':
    main()
