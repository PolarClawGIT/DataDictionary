using System;
using System.Collections.Generic;
using System.Text;
using Toolbox.Mediator;

namespace DataDictionary.Main.Messages
{

    /// <summary>
    /// The Row spefied, by Key, has changed significantly and needs to be refreshed.
    /// </summary>
    abstract class RefreshRow : MessageEventArgs
    { }

    /// <inheritdoc/>
    class RefreshRow<TKey> : RefreshRow
    {
        public TKey Key { get; }

        public RefreshRow(TKey key)
        { Key = key; }
    }
}
