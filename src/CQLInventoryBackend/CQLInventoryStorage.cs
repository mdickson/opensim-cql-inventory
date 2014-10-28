﻿/*
    Copyright (c) 2014, InWorldz, LLC
    All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this
      list of conditions and the following disclaimer.

    * Redistributions in binary form must reproduce the above copyright notice,
      this list of conditions and the following disclaimer in the documentation
      and/or other materials provided with the distribution.

    * Neither the name of opensim-cql-inventory nor the names of its
      contributors may be used to endorse or promote products derived from
      this software without specific prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
    FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
    SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
    OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace CQLInventoryBackend
{
    class CQLInventoryStorage : IInventoryStorage
    {
        private Cluster _cluster;
        private ISession _session;

        private const string KEYSPACE_NAME = "OpensimInventory";

        private readonly PreparedStatement SKEL_SELECT_STMT;



        public CQLInventoryStorage(string[] contactPoints)
        {
            _cluster = Cluster.Builder().AddContactPoints(contactPoints).Build();
            _session = _cluster.Connect();

            SKEL_SELECT_STMT = _session.Prepare("SELECT * FROM skeletons WHERE user_id = ?");
            SKEL_SELECT_STMT.SetConsistencyLevel(ConsistencyLevel.Quorum);
        }

        public List<InventorySkeletonEntry> GetInventorySkeleton(Guid userId)
        {
            var statement = SKEL_SELECT_STMT.Bind(userId);
            var rowset = _session.Execute(statement);

            var retList = new List<InventorySkeletonEntry>();
            foreach (var row in rowset)
            {
                retList.Add(new InventorySkeletonEntry
                {
                    UserId = row.GetValue<Guid>("user_id"),
                    FolderId = row.GetValue<Guid>("folder_id"),
                    Name = row.GetValue<string>("folder_name"),
                    ParentId = row.GetValue<Guid>("parent_id"),
                    Type = row.GetValue<byte>("type"),
                    FolderLevel = (InventorySkeletonEntry.Level)row.GetValue<int>("level")
                });
            }

            return retList;
        }

        public InventoryFolder GetFolder(Guid folderId)
        {
            throw new NotImplementedException();
        }

        public InventoryFolder GetFolderAttributes(Guid folderId)
        {
            throw new NotImplementedException();
        }

        public void CreateFolder(InventoryFolder folder)
        {
            throw new NotImplementedException();
        }

        public void SaveFolder(InventoryFolder folder)
        {
            throw new NotImplementedException();
        }

        public void MoveFolder(InventoryFolder folder, Guid parentId)
        {
            throw new NotImplementedException();
        }

        public Guid SendFolderToTrash(InventoryFolder folder, Guid trashFolderHint)
        {
            throw new NotImplementedException();
        }

        public InventoryFolder FindFolderForType(Guid owner, byte assetType)
        {
            throw new NotImplementedException();
        }

        public void PurgeFolderContents(InventoryFolder folder)
        {
            throw new NotImplementedException();
        }

        public void PurgeFolder(InventoryFolder folder)
        {
            throw new NotImplementedException();
        }

        public void PurgeFolders(IEnumerable<InventoryFolder> folders)
        {
            throw new NotImplementedException();
        }

        public InventoryItem GetItem(Guid itemId, Guid parentFolderHint)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(InventoryItem item)
        {
            throw new NotImplementedException();
        }

        public void SaveItem(InventoryItem item)
        {
            throw new NotImplementedException();
        }

        public void MoveItem(InventoryItem item, InventoryFolder parentFolder)
        {
            throw new NotImplementedException();
        }

        public void PurgeItem(InventoryItem item)
        {
            throw new NotImplementedException();
        }

        public void PurgeItems(IEnumerable<InventoryItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
