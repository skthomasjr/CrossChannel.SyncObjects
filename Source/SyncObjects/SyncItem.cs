using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SyncObjects
{
    public class SyncItem<T>
    {
        public SyncItem(IList<SyncItem<T>> syncItems, T item, SyncAction action = SyncAction.None)
        {
            SyncItems = syncItems;
            Item = item;
            Action = action;
        }

        public T Item { get; }

        public SyncAction Action { get; }

        private IList<SyncItem<T>> SyncItems { get; }

        private int Index => SyncItems.IndexOf(this);

        private bool IsFirst => SyncItems.IndexOf(this) == 0;

        private SyncItem<T> PreviousSyncItem => IsFirst ? null : SyncItems[Index - 1];

        public string GetContextualChecksum()
        {
            var currentItemHash = GetItemChecksum();
            if (IsFirst)
            {
                return GetMd5Hash($"{currentItemHash}{currentItemHash}");
            }
            else
            {
                return GetMd5Hash($"{PreviousSyncItem.GetContextualChecksum()}{currentItemHash}");
            }
        }

        public string GetItemChecksum()
        {
            return GetMd5Hash(JsonConvert.SerializeObject(Item));
        }

        private string GetMd5Hash(string data)
        {
            using (var md5Hash = MD5.Create())
            {
                var itemBytes = Encoding.UTF8.GetBytes(data);
                var hashBytes = md5Hash.ComputeHash(itemBytes);
                var stringBuilder = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    stringBuilder.Append(hashByte.ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}