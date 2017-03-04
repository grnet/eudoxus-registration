using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Threading;
using Imis.Domain;
using System.Data.Metadata.Edm;

namespace Eudoxus.BusinessModel
{
    public partial class HelpDeskEntities : IUnitOfWork
    {
        partial void OnContextCreated()
        {
            SavingChanges += new EventHandler(HelpDeskEntities_SavingChanges);
        }

        private void HelpDeskEntities_SavingChanges(object sender, EventArgs e)
        {
            IEnumerable<ObjectStateEntry> addedStateEntries = ObjectStateManager.GetObjectStateEntries(EntityState.Added).Where(se => !se.IsRelationship);
            foreach (ObjectStateEntry stateEntry in addedStateEntries)
            {
                if (stateEntry.Entity is IUserChangeTracking)
                {
                    IUserChangeTracking entity = (IUserChangeTracking)stateEntry.Entity;

                    if (string.IsNullOrEmpty(entity.CreatedBy))
                        entity.CreatedBy = Thread.CurrentPrincipal.Identity.Name;

                    entity.CreatedAt = DateTime.Now;
                }
            }

            IEnumerable<ObjectStateEntry> modifiedStateEntries = ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Where(se => !se.IsRelationship);
            foreach (ObjectStateEntry stateEntry in modifiedStateEntries)
            {
                if (stateEntry.Entity is IUserChangeTracking)
                {
                    IUserChangeTracking entity = (IUserChangeTracking)stateEntry.Entity;

                    if (!string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
                    {
                        entity.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                        entity.UpdatedAt = DateTime.Now;
                    }
                }
            }
        }

        #region IUnitOfWork Members

        void IUnitOfWork.Commit()
        {
            SaveChanges();
        }

        public void MarkAsDeleted(object entity)
        {
            DeleteObject(entity);
        }

        public void MarkAsNew(object entity)
        {
            Type entityType = entity.GetType();

            // Cannot handle Generic Entities
            MetadataWorkspace.LoadFromAssembly(GetType().Assembly);

            EntityType edmType = MetadataWorkspace.GetItems<EntityType>(DataSpace.CSpace).FirstOrDefault(et => et.Name == entityType.Name);

            while (edmType.BaseType != null)
            {
                edmType = (EntityType)edmType.BaseType;
            }

            EntitySetBase entitySetBase = MetadataWorkspace.GetEntityContainer(DefaultContainerName, DataSpace.CSpace)
                                                           .BaseEntitySets
                                                           .FirstOrDefault(bes => bes.ElementType.Name == edmType.Name);
            if (entitySetBase == null)
                throw new NullReferenceException(string.Format("No EntitySet was found that contains the given EntityType '{0}'", edmType.FullName));

            AddObject(entitySetBase.Name, entity);
        }

        #endregion
    }
}