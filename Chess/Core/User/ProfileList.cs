using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chess.Core.User
{
    public class ProfileCollection : ICollection<Profile>, IEnumerable<Profile>
    {
        public IEnumerator<Profile> GetEnumerator()
        {
            return new ProfileEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ProfileEnumerator(this);
        }

        // The inner collection to store objects. 
        private List<Profile> innerCol;

        // For IsReadOnly 
        private bool isRO = false;

        public ProfileCollection()
        {
            innerCol = new List<Profile>();
        }

        // Adds an index to the collection. 
        public Profile this[int index]
        {
            get { return (Profile)innerCol[index]; }
            set { innerCol[index] = value; }
        }

        public bool Contains(Profile item)
        {
            bool found = false;

            foreach (Profile bx in innerCol)
            {
                if (bx.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }

        public bool Contains(Profile item, EqualityComparer<Profile> comp)
        {
            bool found = false;

            foreach (Profile bx in innerCol)
            {
                if (comp.Equals(bx, item))
                {
                    found = true;
                }
            }

            return found;
        }

        public void Add(Profile item)
        {
            innerCol.Add(item);
        }

        public void Clear()
        {
            innerCol.Clear();
        }

        public void CopyTo(Profile[] array, int arrayIndex)
        {
            for (int i = 0; i < innerCol.Count; i++)
            {
                array[i] = (Profile)innerCol[i];
            }
        }

        public int Count {
            get { return innerCol.Count; }
        }

        public bool IsReadOnly {
            get { return isRO; }
        }

        public bool Remove(Profile item)
        {
            bool result = false;

            return result;
        }
    }


    public class ProfileEnumerator : IEnumerator<Profile>
    {
        private ProfileCollection _collection;
        private int curIndex;
        private Profile curProfile;


        public ProfileEnumerator(ProfileCollection collection)
        {
            _collection = collection;
            curIndex = -1;
            curProfile = default(Profile);

        }

        public bool MoveNext()
        {
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                curProfile = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        void IDisposable.Dispose() { }

        public Profile Current {
            get { return curProfile; }
        }

        object IEnumerator.Current {
            get { return Current; }
        }

    }
}
